﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : PlayerBase
{
    public override void Bomb()
    {
        //ここにボムを使う処理を書く
        ObjectPool.Instance.UseObject(_muzzle.position, PoolObjectType.Player02Bomb01);
        Debug.Log("ボムドーン！！");
        base.Bomb();
    }

    public override void InvincibleMode()
    {
        base.InvincibleMode();
    }

    public override void PlayerAttack()
    {
        switch (GameManager.Instance.PlayerLevel)
        {
            case _level1:
                ObjectPool.Instance.UseObject(_muzzle.position, PoolObjectType.Player02Power1);
                break;
            case _level2:
                ObjectPool.Instance.UseObject(_muzzle.position, PoolObjectType.Player02Power2);
                break;
            case _level3:
                ObjectPool.Instance.UseObject(_muzzle.position, PoolObjectType.Player02Power3);
                break;
        }
        Play(_playerBulletAudio);
        base.PlayerAttack();
    }

    public override void PlayerSuperAttack()
    {
        switch (GameManager.Instance.PlayerLevel)
        {
            case _level1:
                ObjectPool.Instance.UseObject(_muzzle.position, PoolObjectType.Player02SuperPower1);
                break;
            case _level2:
                ObjectPool.Instance.UseObject(_muzzle.position, PoolObjectType.Player02SuperPower2);
                break;
            case _level3:
                ObjectPool.Instance.UseObject(_muzzle.position, PoolObjectType.Player02SuperPower3);
                break;
        }
        Play(_playerBulletAudio);
        base.PlayerSuperAttack();
    }

    public override void PlayerChargeAttack()
    {
        switch (GameManager.Instance.PlayerLevel)
        {
            case _level1:
                ObjectPool.Instance.UseObject(_muzzle.position, PoolObjectType.Player02ChargePower1);
                break;
            case _level2:
                ObjectPool.Instance.UseObject(_muzzle.position, PoolObjectType.Player02ChargePower2);
                break;
            case _level3:
                ObjectPool.Instance.UseObject(_muzzle.position, PoolObjectType.Player02ChargePower3);
                break;
        }
        _audioSource.Stop();
        Play(_playerChargeShotBulletAudio);
    }
}
