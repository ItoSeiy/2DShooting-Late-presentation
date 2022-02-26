using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyController : EnemyBase
{
    /// <summary>�{�X�̃f�[�^</summary>
    public BossData Data => _data;
    /// <summary> �A�j���[�^�[ </summary>
    public Animator Animator { get; private set; } = default;
  
    /// <summary>�{�X�̃f�[�^</summary>
    [SerializeField]
    BossData _data = null;

    /// <summary>���݂̍U���s��</summary>
    private BossAttackAction _currentAttackAction = default;
    /// <summary>���݂̈ړ��s��</summary>
    private BossMoveAction _currentMoveAction = default;
    /// <summary>�s���p�^�[���C���f�b�N�X </summary>
    private int _patternIndex = -1;
    /// <summary>�{�X�̃I�u�W�F�N�g</summary>
    public GameObject Model { get => gameObject; }

    protected override void Awake()
    {
        base.Awake();
        foreach (var pattern in _data.ActionPattern)
        {
            foreach (var attackAction in pattern.BossAttackActions)
            {

            }
            foreach(var moveAction in pattern.BossMoveActions)
            {

            }
        }
    }

    protected override void Update()
    {
        _currentAttackAction?.ManagedUpdate(this);
        _currentMoveAction?.ManagedUpdate(this);
    }

    protected override void Attack()
    {
        //ObjectPool.Instance.UseObject(pos, objType);
    }

    protected override void OnGetDamage()
    {
    }
}
