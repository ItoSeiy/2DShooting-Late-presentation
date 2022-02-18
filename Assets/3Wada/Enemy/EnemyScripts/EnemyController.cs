﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
/// <summary>
/// Enemyの派生クラス
/// </summary>
public class EnemyController : EnemyBase 
{
    [SerializeField, Header("球の出る位置")] Transform[] _muzzle = null;
    [SerializeField,Header("Muzzleを回しながら球が出る位置")] Transform[] _rotateMuzzles = null;
    [SerializeField,Header("Muzzleの切り替え")] MuzzleTransform _muzzleTransform;

    [SerializeField,Header("弾幕")] GameObject _bullet;
    
    [SerializeField,Header("モブ敵の出現位置")] GeneratePos _generatePos;
    [SerializeField, Header("移動方向が変わるXまたはYの座標")] float _limitPos = 0;
    [SerializeField,Header("Muzzleが一周するまでの秒数")] float _rotateSecond = 2f;
    [SerializeField, Header("出た時の移動方向")] Vector2 _beforeDir;
    [SerializeField, Header("移動変わった後の移動方向")] Vector2 _afterDir;
    bool _isBttomposition = false;
    [SerializeField, Header("何秒とどまるか")] float _stopcount = 0.0f;

    [SerializeField, Header("倒された時の音")] GameObject _deathSFX = default;

    [SerializeField, Header("モブの攻撃するときを変える")] AttackMode _attackMode;
    bool _isMove = false;
    



    private void OnEnable()
    {
        Rb.velocity = _beforeDir * Speed;
        _isMove = true;
    }

    protected override void Update()
     {
        switch(_attackMode)
        {
            case AttackMode.MoveAtrack:
                base.Update();
                break;
            case AttackMode.StopAttack:
            if (!_isMove)
            {
                base.Update();
            }
                break;
        }
      
        switch (_generatePos)
        {
            case GeneratePos.Right:
                if (this.transform.position.x <= _limitPos)
                {
                    EnemyMove();
                }
                break;
            case GeneratePos.Left:
                if (this.transform.position.x >= _limitPos)
                {
                    EnemyMove();
                }
                break;
            case GeneratePos.Up:
                if (this.transform.position.y <= _limitPos)
                {
                    EnemyMove();
                }
                break;
            case GeneratePos.Down:
                if (this.transform.position.y >= _limitPos)
                {
                    EnemyMove();
                }
                break;
               
        }
        
     }



    void EnemyMove()
    {   
        //途中で止まる時の処理
        Rb.velocity = Vector2.zero;
        _stopcount -= Time.deltaTime;
        _isMove = false;
        /// <summary>
        /// また動き出す時の処理
        /// </summary>
        if (_stopcount <= 0)
        {
            Rb.velocity = _afterDir * Speed;
            _isBttomposition = true;
            _isMove = true;
        }
    }



    protected override void Attack()
    {
        switch (_muzzleTransform)
        {
            case MuzzleTransform.Normal:
                for (int i = 0; i < _muzzle.Length; i++)
                {
                    var bullet = Instantiate(_bullet);
                    bullet.transform.position = _muzzle[i].position;
                    bullet.transform.rotation = _muzzle[i].rotation;
                    
                }
                
                break;
            case MuzzleTransform.Rotate:
                Rotate();
                for (int i = 0; i < _rotateMuzzles.Length; i++)
                {
                    var bullet = Instantiate(_bullet);
                    bullet.transform.position = _rotateMuzzles[i].position;
                    bullet.transform.rotation = _rotateMuzzles[i].rotation;
                }

                break;
        }
    }

    void Rotate()
    {   
        foreach(var rotateMuzzle in _rotateMuzzles)
        {

            rotateMuzzle.DORotate(new Vector3(0,0, 360f), _rotateSecond, RotateMode.WorldAxisAdd)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);

        }
    }
  
    protected override void OnGetDamage()
    {
        if (EnemyHp <= 0)
        {
            Instantiate(_deathSFX);
        }
    }

    enum GeneratePos
    {
        /// <summary>
        ///右
        /// </summary>
        Right,
        /// <summary>
        /// 左
        /// </summary>
        Left,
        /// <summary>
        ///上 
        /// </summary>
        Up,
         /// <summary>
         /// 下
         /// </summary>
        Down
    }

    enum MuzzleTransform
    {
        Normal,
        Rotate
    }
    enum AttackMode
    {
        StopAttack,
        MoveAtrack
    }
}