using System;
using UnityEngine;

[Serializable]
public class BossData
{
    /// <summary>�s���p�^�[��</summary>
    public Actions[] ActionPattern => actionPattern;

    [SerializeField]
    private Actions[] actionPattern = default;

}

[Serializable]
public class Actions
{
    /// <summary>�U���̔z��</summary>
    public BossAttackAction[] BossActions => bossActions;
    /// <summary>�ړ��̔z��</summary>
    public BossMoveAction[] BossMoveActions => bossMoveActions;

    [SerializeField]
    private BossAttackAction[] bossActions = default;

    [SerializeField]
    private BossMoveAction[] bossMoveActions = default;
}

/// <summary>
/// �{�X��"�U��"�̊��N���X
/// </summary>
public abstract class BossAttackAction : MonoBehaviour
{
    /// <summary>
    /// ��肽���s���̏I������ActonEnd?.Invoke();�ƋL�ڂ���
    /// �������邱�ƂŎ��̓����Ɉڍs����
    /// </summary>
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
    /// <summary>
    /// ��肽���ړ��̏I������ActonEnd?.Invoke();�ƋL�ڂ���
    /// �������邱�ƂŎ��̓����Ɉڍs����
    /// </summary>
    public abstract Action ActinoEnd { get; set; }
    /// <summary> ���̍s���ɓ����ė������̏��� </summary>
    public abstract void Enter(BossEnemyController contlloer);
    /// <summary> ���̍s��Update���� </summary>
    public abstract void ManagedUpdate(BossEnemyController contlloer);
    /// <summary> ���̍s������o�鎞�̏��� </summary>
    public abstract void Exit(BossEnemyController contlloer);
}