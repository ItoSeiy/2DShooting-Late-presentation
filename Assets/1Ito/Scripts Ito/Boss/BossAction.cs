using System;
using UnityEngine;

/// <summary>
/// �{�X�̍s�����N���X
/// </summary>
public abstract class BossAction : MonoBehaviour
{
    public abstract Action ActinoEnd { get; set; }
    /// <summary> ���̍s���ɓ����ė������̏��� </summary>
    public abstract void Enter(BossEnemyController contlloer);
    /// <summary> ���̍s��Update���� </summary>
    public abstract void ManagedUpdate(BossEnemyController contlloer);
    /// <summary> ���̍s������o�鎞�̏��� </summary>
    public abstract void Exit(BossEnemyController contlloer);
}