using System;
using UnityEngine;

[Serializable]
public class BossData
{
    /// <summary>�s���p�^�[��</summary>
    public Actions[] ActionPattern => _actionPattern;

    [SerializeField]
    private Actions[] _actionPattern = default;

}

[Serializable]
public class Actions
{
    /// <summary>�U���̔z��</summary>
    public BossAttackAction[] BossActions => _bossActions;

    [SerializeField]
    private BossAttackAction[] _bossActions = default;
}

/// <summary>
/// �{�X��"�U��"�̊��N���X
/// </summary>
public abstract class BossAttackAction : MonoBehaviour
{
    /// <summary></summary>
    public abstract Action ActinoEnd { get; set; }
    /// <summary> ���̍s���ɓ����ė������̏��� </summary>
    public abstract void Enter(BossEnemyController contlloer);
    /// <summary> ���̍s��Update���� </summary>
    public abstract void ManagedUpdate(BossEnemyController contlloer);
    /// <summary> ���̍s������o�鎞�̏��� </summary>
    public abstract void Exit(BossEnemyController contlloer);
}

/// <summary>
/// �{�X��"�ړ�"�̊��N���X
/// </summary>
public abstract class BossMoveAction : MonoBehaviour
{
    public abstract Action ActinoEnd { get; set; }
    /// <summary> ���̍s���ɓ����ė������̏��� </summary>
    public abstract void Enter(BossEnemyController contlloer);
    /// <summary> ���̍s��Update���� </summary>
    public abstract void ManagedUpdate(BossEnemyController contlloer);
    /// <summary> ���̍s������o�鎞�̏��� </summary>
    public abstract void Exit(BossEnemyController contlloer);
}