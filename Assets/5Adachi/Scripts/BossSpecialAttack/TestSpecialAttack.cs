using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpecialAttack : EnemyBese
{
    /// <summary>�o���b�g�̃v���n�u</summary>
    [SerializeField] List<GameObject> _enemyBulletPrefab = new List<GameObject>();
    /// <summary>�o���b�g�𔭎˂���ꏊ</summary>
    [SerializeField] List<Transform> m_muzzles =new List<Transform>();
    void Start()
    {
        
    }
    protected override void Update()
    {
        base.Update();
    }

   
    protected override void Attack()
    {
        throw new System.NotImplementedException();
    }


}
