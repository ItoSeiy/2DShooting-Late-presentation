﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Enemyの派生クラス
/// </summary>
public class EnemyController : EnemyBese 
{
    [SerializeField,Header("マズルの位置")] Transform[] _muzzle = null;
    [SerializeField, Header("倒された時の音")] GameObject _Audio = default;
    [SerializeField] GameObject _Bulet; 
    [SerializeField, Header("移動の向きの変えるY軸")] float _ybottomposition = 0;
    [SerializeField, Header("移動の向きの変えるx軸")] float _xbottomposition = 0;
    [SerializeField, Header("出た時の移動方向")] Vector2 _beforeDir;
    [SerializeField, Header("移動変わった後の移動方向")] Vector2 _afterDir;
    [SerializeField,Header("モブ敵を止める時の方向の切り替え")] MoveMode _moveMode;
    [SerializeField, Header("")] MoveCurve _MoveCurve;
    [SerializeField] int _a = 2;
    bool _isBttomposition = false;
    [SerializeField, Header("何秒とどまるか")] float _stopcount = 0.0f;
    

    //[SerializeField] float _testX = 0;
    //[SerializeField] float _testY = 0;
    //[SerializeField] float _testX2 = default;
    //[SerializeField] float _testY2 = default;

    //float _test =  default;
    //Vector2 _dir;

    private void OnEnable()
    {
        Rb.velocity = _beforeDir;
    }

    //public float geoLength(float x1, float y1,float x2, float y2)
    //{
    //    float ret = (float)System.Math.Sqrt(System.Math.Pow(x2 - x1, 2) +
    //    System.Math.Pow(y2 - y1, 2));
    //    return ret;
    //}

    protected override void Update()
     {
        base.Update();
        if (_isBttomposition) return;
        //if (_MoveCurve == MoveCurve.curce)
        //{
        //    var Test = geoLength(_testX, _testY, _testX2, _testY2);
        //    Rb.velocity = _dir * Test;
        //}

        switch (_moveMode)
        {
            case MoveMode.right:
                if (this.transform.position.x <= _xbottomposition)
                {
                    EnemyMove();
                }
                break;
            case MoveMode.left:
                if (this.transform.position.x >= _xbottomposition)
                {
                    EnemyMove();
                }
                break;
            case MoveMode.up:
                if (this.transform.position.y <= _ybottomposition)
                {
                    EnemyMove();
                }
                break;
            case MoveMode.down:
                if (this.transform.position.y >= _ybottomposition)
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
        /// <summary>
        /// また動き出す時の処理
        /// </summary>
        if (_stopcount <= 0)
        {
            Rb.velocity = _afterDir;
            _isBttomposition = true; 
        }
    }
   


protected override void Attack()
    {
        for (int i = 0; i < _muzzle.Length; i++)
        {
            Instantiate(_Bulet, _muzzle[i]);
        }
    }
    

    protected override void OnGetDamage()
    {
        if (EnemyHp == 0) 
        {
            Instantiate(_Audio);
        }
    }
    enum MoveMode
    {
        /// <summary>
        ///右
        /// </summary>
        right,
        /// <summary>
        /// 左
        /// </summary>
        left,
        /// <summary>
        ///上 
        /// </summary>
        up,
         /// <summary>
         /// 下
         /// </summary>
        down
    }
    enum MoveCurve
    {
        strate,
        curce
    }
}
