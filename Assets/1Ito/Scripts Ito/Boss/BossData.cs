using System;
using UnityEngine;

/// <summary>
/// �{�X�̃f�[�^���i�[���ꂽ�N���X
/// </summary>
[Serializable]
public class BossData
{
    /// <summary>�{�X�̍s��</summary>
    public Actions[] ActionPattern => actionPattern;

    [SerializeField,Header("BossAttackAction��BossAttackAction�͓����ɂ��邱��")]
    private Actions[] actionPattern = default;

}

/// <summary>
/// �{�X�̍s�����i�[���ꂽ�N���X
/// </summary>
[Serializable]
public class Actions
{
    /// <summary>�U���̔z��</summary>
    public BossAttackAction[] BossAttackActions => bossAttackActions;
    /// <summary>�ړ��̔z��</summary>
    public BossMoveAction[] BossMoveActions => bossMoveActions;

    [SerializeField]
    private BossAttackAction[] bossAttackActions = default;

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
    /// <summary> ���̍s���ɓ����ė������̏��� </summary>
    public abstract void Enter(BossEnemyController contlloer);
    /// <summary> ���̍s��Update���� </summary>
    public abstract void ManagedUpdate(BossEnemyController contlloer);
    /// <summary> ���̍s������o�鎞�̏��� </summary>
    public abstract void Exit(BossEnemyController contlloer);
}