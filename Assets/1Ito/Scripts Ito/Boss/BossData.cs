using System;
using UnityEngine;

/// <summary>
/// �{�X�̃f�[�^���i�[���ꂽ�N���X
/// </summary>
[Serializable]
public class BossData
{
    /// <summary>�{�X�̍s��</summary>
    public BossAcitionPattern[] ActionPatterns => actionPatterns;
    /// <summary>�K�E�Z�̔z��</summary>
    public BossAttackAction[] BossSuperAttackActions => bossSuperAttackActons;

    [SerializeField,Header("BossAttackAction��BossAttackAction�͓����ɂ��邱��")]
    private BossAcitionPattern[] actionPatterns = default;

    [SerializeField, Header("�K�E�Z�̍s��(��{�I��2��)")]
    private BossAttackAction[] bossSuperAttackActons = default; 
}

/// <summary>
/// �{�X�̍s�����i�[���ꂽ�N���X
/// </summary>
[Serializable]
public class BossAcitionPattern
{
    /// <summary>�U���̔z��</summary>
    public BossAttackAction[] BossAttackActions => bossAttackActions;
    /// <summary>�ړ��̔z��</summary>
    public BossMoveAction[] BossMoveActions => bossMoveActions;

    [SerializeField, Header("�U���̍s��")]
    private BossAttackAction[] bossAttackActions = default;

    [SerializeField, Header("�ړ��̍s��")]
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
    public abstract void Enter(BossController contlloer);
    /// <summary> ���̍s��Update���� </summary>
    public abstract void ManagedUpdate(BossController contlloer);
    /// <summary> ���̍s������o�鎞�̏��� </summary>
    public abstract void Exit(BossController contlloer);
}

/// <summary>
/// �{�X��"�ړ�"�̊��N���X
/// </summary>
public abstract class BossMoveAction : MonoBehaviour
{
    /// <summary> ���̍s���ɓ����ė������̏��� </summary>
    public abstract void Enter(BossController contlloer);
    /// <summary> ���̍s��Update���� </summary>
    public abstract void ManagedUpdate(BossController contlloer);
    /// <summary> ���̍s������o�鎞�̏��� </summary>
    public abstract void Exit(BossController contlloer);
}