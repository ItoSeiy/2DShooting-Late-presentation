using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


/// <summary>
/// �{�X�G���Ǘ�����X�e�[�g�p�^�[���̃X�N���v�g
/// 
/// �{�X�ɂ�"�A�N�V����(Action)"�Ƃ����s��(�U����ړ�)
/// ��������Ă���
/// 
/// ������܂Ƃ߂�"�p�^�[��(Pattern)"�Ƃ��Ă�����̂�����
/// </summary>
public class BossController : EnemyBase
{
    /// <summary>�{�X�̃f�[�^</summary>
    public BossData Data => _data;
    /// <summary>�{�X�̃f�[�^</summary>
    [SerializeField]
    BossData _data = null;

    public float CastTime => _castTime;

    [SerializeField]
    float _castTime;

    /// <summary>�L���X�g�̔���</summary>
    public bool IsCast { get; private set; } = default;

    /// <summary> �A�j���[�^�[ </summary>
    public Animator Animator { get; private set; } = default;

    /// <summary>���݂̍U���s��</summary>
    private BossAttackAction _currentAttackAction = default;
    /// <summary>���݂̈ړ��s��</summary>
    private BossMoveAction _currentMoveAction = default;

    /// <summary>�s��"�p�^�[��"�̃C���f�b�N�X</summary>
    private int _patternIndex = -1;
    /// <summary>"�A�N�V����"�̃C���f�b�N�X</summary>
    private int _actionIndex = 0;
    protected override void Awake()
    {
        base.Awake();
        foreach (var pattern in _data.ActionPattern)
        {
            foreach (var attackAction in pattern.BossAttackActions)
            {
                attackAction.ActinoEnd = () =>
                {
                    Debug.Log("�s���p�^�[�������Ɉڂ�܂�");
                    _actionIndex++;

                    if(_actionIndex >= _data.ActionPattern[_patternIndex].BossAttackActions.Length)
                    {
                        //�s���p�^�[�������s����������
                        RandomPatternChange();//�s���p�^�[����ς���
                    }
                    else//�s���p�^�[�������s�������Ă��Ȃ�������
                    {
                        //�A�N�V���������̂��̂ɐ؂�ւ���
                        ChangeAction(_data.ActionPattern[_patternIndex].BossAttackActions[_actionIndex],
                                     _data.ActionPattern[_patternIndex].BossMoveActions[_actionIndex]);
                    }
                };
            }
        }
    }

    protected override void Update()
    {
        if(IsCast)
        {
            Debug.Log($"{_currentAttackAction.gameObject.name},��{_currentMoveAction.gameObject.name}\nUpdate�̎��s�̓L���X�g���̂��ߍs���Ă��܂���");
            return;
        }
        _currentAttackAction?.ManagedUpdate(this);
        _currentMoveAction?.ManagedUpdate(this);
    }

    /// <summary>
    /// �s���p�^�[���������_���ɕς���
    /// </summary>
    private void RandomPatternChange()
    {
        //�s���p�^�[���̃C���b�f�N�X�����߂�
        _patternIndex = Random.Range(0, _data.ActionPattern.Length);
        Debug.Log($"�p�^�[��{_patternIndex}�����s����");

        //�A�N�V������ύX�����s����
        ChangeAction(_data.ActionPattern[_patternIndex].BossAttackActions[_actionIndex],
                     _data.ActionPattern[_patternIndex].BossMoveActions[_actionIndex]);
    }

    /// <summary>
    /// �A�N�V������ύX�����s����
    /// </summary>
    /// <param name="bossAttackAction"></param>
    /// <param name="bossMoveAction"></param>
    private void ChangeAction(BossAttackAction bossAttackAction, BossMoveAction bossMoveAction)
    {
        //���݂̃A�N�V�����̍Ō�ɍs���֐����Ă�
        _currentAttackAction?.Exit(this);
        _currentMoveAction?.Exit(this);

        //�A�N�V�����̒��g��؂�ւ���
        _currentAttackAction = bossAttackAction;
        _currentMoveAction = bossMoveAction;

        //�؂�ւ�����̃A�N�V���������s����
        _currentAttackAction.Enter(this);
        _currentAttackAction.Enter(this);
    }

    /// <summary>
    /// �҂��Ƃ����s����֐�
    /// �A�b�v�f�[�g�̎��s��҂�
    /// </summary>
    /// <param name="castTime">�҂���</param>
    public void Cast(float castTime)
    {
        StopAllCoroutines();
        StartCoroutine(Casting(castTime));
    }

    private IEnumerator Casting(float castTime)
    {
        IsCast = true;

        float timer = 0;
        while(timer <= castTime)
        {
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        IsCast = false;
    }

    protected override void OnGetDamage()
    {
    }
}