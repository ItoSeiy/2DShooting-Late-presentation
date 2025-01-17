﻿using Cinemachine;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Overdose.Novel;

/// <summary>
/// Playerの基底クラス
/// シングルトンパターンではない
/// アイテムの上限値等を持っている
/// (プレイヤーによって実数が変わる可能性があるため)
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class PlayerBase : MonoBehaviour, IPauseable
{
    /// <summary>無敵モードのフラグ</summary>
    [SerializeField] 
    [Header("無敵モード")]
    bool _isGodMode = false;

    [SerializeField]
    [Header("プレイヤーの音")]
    protected AudioData[] _audioData;

    Rigidbody2D _rb;
    SpriteRenderer _sp;
    protected AudioSource _audioSource;
    Animator _anim;
    Vector2 _dir;
    Vector2 _oldVerocity;

    [SerializeField]
    [Header("リスポーンするポジション")]
    Transform _playerRespawn = default;

    [SerializeField]
    [Header("弾を発射するポジション")]
    protected Transform _muzzle = default;

    [SerializeField]
    [Header("残基が無くなってから自分が消えるまでの時間(ミリ秒)")]
    int _onDestroyDelay = 1500;

    [SerializeField]
    [Header("精密操作時の発射する間隔(ミリ秒)")]
    int _superAttackDelay = 200;
    
    [SerializeField]
    [Header("発射する間隔(ミリ秒)")]
    int _attackDelay = 200;
    
    [SerializeField]
    [Header("ボムのクールタイム（ミリ秒）")]
    int _bombCoolTime = default;
    
    [SerializeField]
    [Header("無敵モードのクールタイム(ミリ秒)")]
    int _invincibleTime = 2800;

    [SerializeField]
    [Header("レベル2に上げるために必要なパワーアイテムの数")]
    int _playerPowerRequiredNumberLevel2 = 50;
    
    [SerializeField]
    [Header("レベル3に上げるために必要なパワーアイテムの数")]
    int _playerPowerRequiredNumberLevel3 = 100;

    [SerializeField]
    [Header("Enemyのタグ")]
    string _enemyTag = "Enemy";
    
    [SerializeField]
    [Header("EnemyのBulletのタグ")]
    string _enemyBulletTag = "EnemyBullet";
    
    [SerializeField]
    [Header("Powerのタグ")]
    string _powerTag = "Power";
    
    [SerializeField]
    [Header("Pointのタグ")]
    string _pointTag = "Point";
    
    [SerializeField]
    [Header("1upのタグ")]
    string _1upTag = "1UP";
    
    [SerializeField]
    [Header("ボムを増やすアイテムのタグ")]
    string _bombItemTag = "BombItem";
    
    [SerializeField]
    [Header("Invincibleのタグ")]
    string _invincibleTag = "Invincible";
    
    [SerializeField]
    [Header("アイテムを回収するためのコライダーのタグ")]
    string _playerItemGetLineTag = "ItemGetLine";

    [SerializeField]
    [Header("被弾時に再生するアニメーションのパラメータ名")]
    string _invicibleAnimParamName = "IsInvicible";

    [SerializeField]
    [Header("動くスピード")]
    float _moveSpeed = default;
    
    [SerializeField]
    [Header("精密操作のスピード")]
    float _lateMove = default;

    [SerializeField]
    [Header("Plaerの残機の上限")]
    int _playerResidueLimit = 99;
    
    [SerializeField]
    [Header("Playerのボムの上限")]
    int _playerBombLimit = 99;
    
    [SerializeField]
    [Header("Playerのスコアの上限")]
    int _playerScoreLimit = 999999999;
    
    [SerializeField]
    [Header("この数値以上なら、一定時間無敵モードになる変数")]
    int _invincibleLimit = 150;
    
    [SerializeField]
    [Header("Playerのパワーの上限")]
    int _playerPowerLimit = 150;
    
    [SerializeField]
    [Header("リスポーン中の無敵時間")]
    int _respawnTime = 2800;
    
    [SerializeField]
    [Header("リスポーン後の無敵時間")]
    int _afterRespawnTime = 1400;

    [SerializeField]
    [Header("音量を調節する変数")]
    protected float _musicVolume = default;

    [SerializeField]
    [Header("揺らすカメラ")]
    CinemachineVirtualCamera _cmvcam1 = default;

    [SerializeField]
    [Header("EffectをまとめてあるGameObject")]
    GameObject _effects;

    [SerializeField]
    [Header("チャージショットのパーティカルシステム（溜め）")]
    ParticleSystem _chargeps = default;

    [SerializeField]
    [Header("精密操作時の演出R")]
    ParticleSystem _parsR;
    
    [SerializeField]
    [Header("精密操作時の演出B")]
    ParticleSystem _parsB;
    
    [SerializeField]
    [Header("精密操作時の演出G")]
    ParticleSystem _parsG;

    [SerializeField]
    [Header("精密操作時のPlayerの色をゲーミングにする変数")]
    int _gameingPlayerColorCoolTime = default;

    [SerializeField]
    [Header("パワーアイテムの数がカンストしたとき（レベルマックスのとき）の演出")]
    ParticleSystem _fullPowerModeEffect = default;
    
    [SerializeField]
    [Header("Invincibleモードのときの演出")]
    ParticleSystem _invincibleModeEffect = default;
    
    [SerializeField]
    [Header("残機がゼロの時の演出")]
    ParticleSystem _playerDeathEffect = default;

    [SerializeField]
    [Header("パワーアイテムのデスペナルティ")]
    int _powerDeathPenalty = -50;

    [SerializeField]
    [Header("通常弾の音")]
    protected string _playerBulletAudio = "Bullet";
    
    [SerializeField]
    [Header("チャージ中の音")]
    protected string _playerChargeBulletAudio = "Charge";
    
    [SerializeField]
    [Header("チャージショットの音")]
    protected string _playerChargeShotBulletAudio = "ChargeShot";
    
    [SerializeField]
    [Header("ボムの音")]
    protected string _playerBombShotAudio = "Bomb";
    
    [SerializeField]
    [Header("ボムの着弾時の音")]
    string _bombOnEnemyAudio = "OnBomb";
    
    [SerializeField]
    [Header("プレイヤーの被弾時に流れる音")]
    string _playerDestroyAudio = "PlayerDestroy";
    
    [SerializeField]
    [Header("アイテムを回収したときの音")]
    string _itemGetAudio = "ItemGet";
    
    [SerializeField]
    [Header("１UPの音")]
    string _1UPAudio = "1UP";
    
    [SerializeField]
    [Header("ボムアイテム獲得時の音")]
    string _getBombAudio = "BombGet";
    
    [SerializeField]
    [Header("レベルアップ時の音")]
    public readonly string _levelUpAudio = "LevelUp";
    
    [SerializeField]
    [Header("Invincibleモードの時の音")]
    string _invincibleModeAudio = "Invincible";
    
    [SerializeField]
    [Header("残機がゼロになった時の音")]
    string _playerGameOverAudio = "Death";

    [SerializeField]
    [Header("精密操作時の当たり判定のSprite")]
    GameObject _playerCollider = default;

    protected const int _level1 = 1;
    protected const int _level2 = 2;
    protected const int _level3 = 3;

    public int PlayerPowerRequiredNumberLevel2 => _playerPowerRequiredNumberLevel2;
    public int PlayerPowerRequiredNumberLevel3 => _playerPowerRequiredNumberLevel3;

    public int PlayerResidueLimit => _playerResidueLimit;
    public int PlayerBombLimit => _playerBombLimit;
    public int PlayerScoreLimit => _playerScoreLimit;
    public int PlayerPowerLimit => _playerPowerLimit;
    /// <summary>無敵モードになるために必要なInvicibleアイテムの数が入ったプロパティ</summary>
    public int InvicibleLimit => _invincibleLimit;
    /// <summary>移動の可不可が入ったプロパティ</summary>
    public bool CanMove { get => _canMove; set => _canMove = value; }

    public bool IsGodMode { get { return _isGodMode; } set { _isGodMode = value; } }

    /// <summary>連続で弾を撃てないようにするフラグ</summary>
    bool _isBulletStop = default;
    /// <summary>精密操作時のフラグ</summary>
    bool _isLateMode = default;
    /// <summary>ボムの使用時に立つフラグ</summary>
    protected bool _isBomb = default;
    /// <summary>移動の可不可を制御するフラグ</summary>
    bool _canMove = true;
    /// <summary>チャージしているかどうか判定するフラグ</summary>
    bool _isCharge = default;
    /// <summary>アタックしているかどうか判定するフラグ</summary>
    bool _isAttackMode = default;
    /// <summary>パワーアイテムがマックスかどうか判定するフラグ</summary>
    bool _isPowerMax = false;
    /// <summary>1UPアイテムがマックスかどうか判定するフラグ</summary>
    bool _is1upMax = false;
    /// <summary>ボムアイテムがマックスかどうか判定するフラグ</summary>
    bool _isBombMax = false;


    /// <summary>カウントアップする定数</summary>
    const int DEFAULTCOUNTUP = 1;
    /// <summary>カウントダウンする定数</summary>
    const int DEFAULTCOUNTDOWN = -1;
    /// <summary>0の入った定数(デフォルト)</summary>
    const int DEFAULT = 0;
    /// <summary>InvincibleObjectを初期化する定数</summary>
    const int INVENCIBLEDEFAULT = -150;


    private void Start()
    {
        GameManager.Instance.OnStageClear += () =>
        {
            AllFalse();
            OnItemGetLine();
        };

        _rb = GetComponent<Rigidbody2D>();
        _sp = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
        _anim = GetComponent<Animator>();

        _cmvcam1.Priority = -1;

        transform.position = _playerRespawn.position;//リスポーン地点に移動

        GamingFalse();
    }

    private void Update()
    {
        if (!_canMove)
        {
            _rb.velocity = Vector2.zero;
            return;
        }
        switch (_isLateMode)//移動時に精密操作モードかどうか判定する
        {
            case false:
                _rb.velocity = _dir * _moveSpeed;
                break;
            case true:
                _rb.velocity = _dir * _lateMove;
                break;
        }
        switch (_isAttackMode)
        {
            case false:
                break;
            case true:
                if (PhaseNovelManager.Instance)
                {
                    if(PhaseNovelManager.Instance.NovelePhaesState != NovelPhase.None) return;
                }
                switch (_isLateMode)
                {
                    case false:
                        Shot(PlayerAttack, _attackDelay);
                        break;
                    case true:
                        Shot(PlayerSuperAttack, _superAttackDelay);
                        break;
                }
                break;
        }
    }

    async void Shot(Action action, int delay)
    {
        if (_isBulletStop) return;
        action();
        await Task.Delay(delay);
        _isBulletStop = false;
    }

    private void OnEnable()
    {
        PauseManager.Instance.SetEvent(this);
    }

    private void OnDisable()
    {
        PauseManager.Instance.RemoveEvent(this);
    }

    public void OnMove(InputAction.CallbackContext context)//通常の移動
    {
        if (!_canMove) return;
        Vector2 inputMoveMent = context.ReadValue<Vector2>();
        _dir = new Vector2(inputMoveMent.x, inputMoveMent.y);
        Inversion();
    }

    public void OnLateMove(InputAction.CallbackContext context)//精密操作時の移動
    {
        if (!_canMove) return;
        if (context.started)//LeftShiftKeyが押された瞬間の処理
        {
            _isLateMode = true;
            _playerCollider.SetActive(true);
            PlayerGamingEffect();
        }
        if (context.performed || context.canceled)//LeftShiftKeyが離された瞬間の処理
        {
            _isLateMode = false;
            _playerCollider.SetActive(false);
            GamingFalse();
        }
    }


    public async void OnJump(InputAction.CallbackContext context)//SpaceKeyが押された瞬間の処理
    {
        if (context.started && DEFAULT < GameManager.Instance.PlayerBombCount && !_isBomb && _canMove && !_isCharge && !_isAttackMode)
        {
            Bomb();
            GameManager.Instance.PlayerBombCountChange(DEFAULTCOUNTDOWN);
            await Task.Delay(_bombCoolTime);
            _isBomb = false;
        }
    }

    public void OnInputChargeShotButton(InputAction.CallbackContext context)//ChargeShotの処理
    {
        if (!_canMove) return;
        if (context.started && !_isCharge && !_isAttackMode)
        {
            _cmvcam1.Priority = 10;
            _audioSource.Stop();
            Play(_playerChargeBulletAudio);
            _isCharge = true;
            _chargeps.gameObject.SetActive(true);
        }
        if(context.performed && _isCharge)
        {
            _isCharge = false;
            if (!_canMove) return;
            PlayerChargeAttack();
            _cmvcam1.Priority = -1;
            _chargeps.gameObject.SetActive(false);
        }
        if(context.canceled)
        {
            _isCharge = false;
            if (!_canMove) return;
            _audioSource.Stop();
            _cmvcam1.Priority = -1;
            _chargeps.gameObject.SetActive(false);
        }
    }

    public void OnFire(InputAction.CallbackContext context)//Mouceの左クリックまたは、GamePadのZRボタンで弾を出す
    {
        if (context.started)//攻撃時の処理
        {
            if (_isCharge) return;
            _isAttackMode = true;
        }
        if(context.performed || context.canceled)
        {
            _isAttackMode = false;
        }
    }

    /// <summary>通常の攻撃処理</summary>
    public virtual void PlayerAttack()
    {
        _isBulletStop = true;
    }

    /// <summary>精密操作時の攻撃処理</summary>
    public virtual void PlayerSuperAttack()
    {
        _isBulletStop = true;
    }

    /// <summary>チャージショット時の攻撃処理</summary>
    public virtual void PlayerChargeAttack()
    {
        Debug.LogError("チャージアタックが実装されていません");
    }

    /// <summary>ボム使用時の処理</summary>
    public virtual void Bomb()
    {
        _isBomb = true;
        _isBombMax = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //EnemyまたはEnemyBuletに当たった際行う残機を減らす処理　無敵モードであれば残機は減らない
        if (!_isGodMode && collision.tag == _enemyTag || collision.tag == _enemyBulletTag)
        {
            if (_isGodMode) return;
            _cmvcam1.Priority = -1;
            _isCharge = false;
            _isAttackMode = false;
            _effects.SetActive(false);
            GamingFalse();
            GameManager.Instance.ResidueChange(DEFAULTCOUNTDOWN);

            if (GameManager.Instance.PlayerResidueCount >= DEFAULTCOUNTUP)//残機が残っている場合はリスポーンを行う
            {
                Play(_playerDestroyAudio);
                Respawn();
            }
            else//残機が0であればゲームオーバー処理を呼び出す
            {
                Play(_playerGameOverAudio);
                _playerDeathEffect.gameObject.SetActive(true);
                AllFalse();
                GameManager.Instance.GameOver();
            }
        }

        if (collision.tag == _1upTag)//残機を増やす処理
        {
            CollisionItem(collision.gameObject, _1UPAudio, GameManager.Instance.ResidueChange);
            if(GameManager.Instance.PlayerResidueCount >= _playerResidueLimit)
            {
                _is1upMax = true;
            }
        }

        if (collision.tag == _bombItemTag)//ボムの所持数を増やす処理
        {
            CollisionItem(collision.gameObject, _getBombAudio, GameManager.Instance.PlayerBombCountChange);
            if(GameManager.Instance.PlayerBombCount >= _playerBombLimit)
            {
                _isBombMax = true;
            }
        }

        if (collision.tag == _pointTag)//スコアを増やす処理
        {
            CollisionItem(collision.gameObject, _itemGetAudio, GameManager.Instance.PlayerScoreChange);
        }

        if (collision.tag == _powerTag)//パワーを増やす処理
        {
            var power = GameManager.Instance.PlayerPowerItemCount;
            CollisionItem(collision.gameObject, _itemGetAudio,
                GameManager.Instance.PlayerPowerItemCountChange);
            if(power == _playerPowerRequiredNumberLevel2 || power == _playerPowerRequiredNumberLevel3 || power == _playerPowerLimit)
            {
                Play(_levelUpAudio);//LevelUP時に音を鳴らす
            }
            if (power >= _playerPowerLimit)
            {
                _fullPowerModeEffect.gameObject.SetActive(true);
                _isPowerMax = true;
            }
        }

        if (collision.tag == _invincibleTag)//一定数取得すると無敵になるアイテムの所持数を増やす処理
        {
            CollisionItem(collision.gameObject, _itemGetAudio, GameManager.Instance.PlayerInvicibleObjectValueChange);
            if(GameManager.Instance.PlayerInvincibleObjectCount >= _invincibleLimit)
            {
                InvincibleMode();
            }
        }

        if(collision.tag == _playerItemGetLineTag)
        {
            OnItemGetLine();
        }
    }

    void CollisionItem(GameObject go, string audioName, Action<int> action)
    {
        var item = go.GetComponent<ItemBase>();
        Play(audioName);
        if (item.IsTaking) return;
        action(item.ItemCount);
        item.IsTaking = true;
    }

    void OnItemGetLine()
    {
            string[] itemTags = new string[] {_1upTag, _bombItemTag, _pointTag, _powerTag, _invincibleTag};
            foreach(var itemTag in itemTags)
            {
                AllItemGet(itemTag);
            }
    }

    void AllItemGet(string itemTag)
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag(itemTag);
        foreach(var item in items)
        {
            var itemBase = item.GetComponent<ItemBase>();
            itemBase?.ItemGet();
        }
    }

    public async void Respawn()//リスポーンの処理
    {
        _isGodMode = true;
        _canMove = false;
        _is1upMax = false;
        _anim.SetBool(_invicibleAnimParamName, true);
        _chargeps.gameObject.SetActive(false);
        _fullPowerModeEffect.gameObject.SetActive(false);
        GamingFalse();
        DeathPenalty();
        await Task.Delay(_respawnTime);
        _dir = Vector2.zero;
        transform.position = _playerRespawn.position;//ここでリスポーン地点に移動
        _effects.SetActive(true);
        _canMove = true;
        await Task.Delay(_afterRespawnTime);
        _isGodMode = false;
        _anim.SetBool(_invicibleAnimParamName, false);
    }

    public virtual async void InvincibleMode()//無敵モード
    {
        if (_isGodMode) return;
        _isGodMode = true;
        _anim.SetBool(_invicibleAnimParamName, true);
        _invincibleModeEffect.gameObject.SetActive(true);
        Play(_invincibleModeAudio);
        GameManager.Instance.PlayerInvicibleObjectValueChange(INVENCIBLEDEFAULT);
        await Task.Delay(_invincibleTime);
        _isGodMode = false;
        _anim.SetBool(_invicibleAnimParamName, false);
        _invincibleModeEffect.gameObject.SetActive(false);
    }

    void Inversion()
    {
        if (_dir.x > DEFAULT)
        {
            _sp.flipX = false;
        }
        else if (_dir.x < DEFAULT)
        {
            _sp.flipX = true;
        }
    }

    public void Play(string key)
    {
        var data = Array.Find(_audioData, e => e._key == key);

        if (data != null)
        {
            _audioSource.PlayOneShot(data._audio);
        }
        else
        {
            Debug.Log("AudioClipがありません");
        }
    }
    async void PlayerGamingEffect()
    {
        if (!_isLateMode) return;
        _parsR.gameObject.SetActive(true);
        await Task.Delay(_gameingPlayerColorCoolTime);
        if (!_isLateMode) return;
        _parsB.gameObject.SetActive(true);
        await Task.Delay(_gameingPlayerColorCoolTime);
        if (!_isLateMode) return;
        _parsG.gameObject.SetActive(true);
    }

    void GamingFalse()
    {
        _parsR.gameObject.SetActive(false);
        _parsB.gameObject.SetActive(false);
        _parsG.gameObject.SetActive(false);
    }

    //private void OnParticleCollision(GameObject other)
    //{
    //    if (_isLateMode || _isGodMode) return;
    //    Respawn();
    //}

    void AllFalse()
    {
        _canMove = false;
        _sp.enabled = false;
        GamingFalse();
        _effects.SetActive(false);
    }

    void DeathPenalty()
    {
        if(GameManager.Instance.PlayerPowerItemCount + _powerDeathPenalty < 0)
        {
            GameManager.Instance.PlayerPowerItemCountChange(0 - GameManager.Instance.PlayerPowerItemCount);
        }
        else
        {
            GameManager.Instance.PlayerPowerItemCountChange(_powerDeathPenalty);
        }
        _isPowerMax = false;
    }
    void IPauseable.PauseResume(bool isPause)
    {
        if(isPause)
        {
            _anim.speed = 0;
            _oldVerocity = _rb.velocity;
            _rb.velocity = Vector2.zero;
            _canMove = false;
        }
        else
        {
            _anim.speed = 1;
            _rb.velocity = _oldVerocity;
            _canMove = true;
        }
    }
}
