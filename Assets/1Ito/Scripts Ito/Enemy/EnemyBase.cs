﻿using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemyの基底クラス
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public abstract class EnemyBase : MonoBehaviour, IDamage, IPauseable
{
    public float Speed => _speed;
    public float EnemyHp => _enemyHp;
    public float DamageTakenRation { get => _damageTakenRatio; set => _damageTakenRatio = value; }
    public Rigidbody2D Rb { get => _rb; set => _rb = value; }
    public float AttackInterval => _attackInterval;
    public string PlayerBulletTag => _playerBulletTag;
    public string PlayerTag => _playerTag;
    public string GameZoneTag => _gameZoneTag;
    public SpriteRenderer Sprite => _sprite;

    [SerializeField, Header("動きのスピード")]
    private float _speed = 5f;

    [SerializeField, Header("体力")] 
    private float _enemyHp = 10f;

    [SerializeField, Header("攻撃頻度(秒)")]
    private float _attackInterval = 1f;

    /// <summary>攻撃力の割合/// </summary>
    [SerializeField, Header("被ダメージを何割にするか"), Range(0f, 1f)]
    float _damageTakenRatio = 1f;

    [SerializeField, Header("プレイヤーのBulletのタグ")]
    string _playerBulletTag = "PlayerBullet";

    [SerializeField, Header("プレイヤーのタグ")]
    string _playerTag = "Player";

    [SerializeField, Header("壁のタグ")] 
    string _gameZoneTag = "Finish";

    [SerializeField]
    SpriteRenderer _sprite = default;

    [SerializeField, Header("死亡時に落とすアイテム")] 
    DropItems _dropItems;

    [SerializeField] 
    float _itemDropRangeX = 2f;
    [SerializeField] 
    float _itemDropRangeY = 2f;

    private float _attackTimer = default;
    Rigidbody2D _rb = null;
　　protected bool _destroyAble = false;

    Vector2 _oldVelocity;
    float _oldTimer;

    private bool _isKilledFirstTime = true; 

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        if (PauseManager.Instance.PauseFlg == true) return;

        _attackTimer += Time.deltaTime;

        if(_attackTimer > _attackInterval)
        {
            Attack();
            _attackTimer = 0;
        }
    }

    void OnEnable()
    {
        PauseManager.Instance.SetEvent(this);
    }

    void OnDisable()
    {
        PauseManager.Instance.RemoveEvent(this);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(EnemyHp <= 0)
        {
            OnKilledByPlayer();
        }
        OnGameZoneTag(collision);
    }

    /// <summary>
    /// プレイヤーに殺された際の処理
    /// </summary>
    protected virtual void OnKilledByPlayer()
    {
        if(_isKilledFirstTime)
        {
            ItemDrop();
            _isKilledFirstTime = false;
        }
        Destroy(gameObject);
    }

    /// <summary>
    /// ゲームゾーンに触れた際の処理
    /// 2回触れたら破棄する仕様
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void OnGameZoneTag(Collider2D collision)
    {
        if(collision.tag == _gameZoneTag)
        {
            if(!_destroyAble)
            {
                _destroyAble = true;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
    /// <summary>
    /// ダメージを受けた際に行う処理を書いてください
    /// 例)ダメージを受けた際のアニメーションなど
    /// </summary>
    protected abstract void OnGetDamage();

    /// <summary>
    /// 攻撃の処理を書いてください
    /// 例)Bulletプレハブを生成するなど
    /// </summary>
    protected virtual void Attack()
    {
        Debug.LogError($"{gameObject.name}の攻撃が実装されていません\n実装してください");
    }

    /// <summary>
    /// アイテムを落とす処理
    /// </summary>
    public void ItemDrop()
    {
        for(int itemIndex = 0; itemIndex < _dropItems.Items.Count; itemIndex++)
        {
            for(int i = 0; i < _dropItems.Items[itemIndex].Count; i++)
            {
                float x = Random.Range(transform.position.x + _itemDropRangeX, transform.position.x - _itemDropRangeX);
                float y = Random.Range(transform.position.y + _itemDropRangeY, transform.position.y - _itemDropRangeY);
                ObjectPool.Instance.UseObject(new Vector2(x, y), _dropItems.Items[itemIndex].ItemType);
            }
        }
    }

    /// <summary>
    /// ダメージを喰らった際にBulletBaseから呼び出される関数
    /// Enemyがダメージを喰らう
    /// 攻撃力のデバフを行う
    /// ダメージを喰らった際の処理もここで呼ばれる
    /// 受けるダメージ量はBulletが指定する
    /// </summary>
    /// <param name="damage">受けるダメージ量</param>
    public void AddDamage(float damage, Collider2D col)
    {
        //攻撃力を設定した分減らす処理
        damage *= _damageTakenRatio;

        _enemyHp -= damage;
        OnGetDamage();
    }

    public void ChangeAttackInterval(float interval)
    {
        _attackInterval = interval;
    }

    public void ChangeAttackIntervalRandom(float min, float max)
    {
        _attackInterval = Random.Range(min, max);
    }

    void IPauseable.PauseResume(bool isPause)
    {
        if (isPause)
        {
            _oldVelocity = _rb.velocity;
            _oldTimer = _attackTimer;
            _attackTimer = 0;
        }
        else
        {
            _rb.velocity = _oldVelocity;
            _attackTimer = _oldTimer;
        }
    }

    protected void OnPauseForSubSlass()
    {
        _rb.velocity = Vector2.zero;
    }

    /// <summary>
    /// 落とすアイテムを格納するクラス
    /// </summary>
    [System.Serializable]
    class DropItems
    {
        public List<DropItem> Items => items;
        [SerializeField]
        private List<DropItem> items = new List<DropItem>();
    }

    /// <summary>
    /// 落とすアイテムひとつひとつを格納するクラス
    /// </summary>
    [System.Serializable]
    class DropItem
    {
        public PoolObjectType ItemType => itemType;
        public int Count => count;
        [SerializeField]
        PoolObjectType itemType;
        [SerializeField]
        private int count = 5;
    }
}

