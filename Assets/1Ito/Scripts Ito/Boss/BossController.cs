using System.Collections;
using System.Linq;
using UnityEngine;

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
    //[SerializeField, Header("���S���̃p�[�e�B�N��")]
    //ParticleSystem _
    /// <summary>�{�X�̃f�[�^</summary>
    public BossData Data => _data;
    /// <summary>�{�X�̃f�[�^</summary>
    [SerializeField]
    BossData _data = null;

    [SerializeField, Header("�ݒ肵�����l�ȉ��ɂȂ�ƕK�E�Z�𔭓�����(��{�I��2��)")]
    float[] _superAttackTimingHp = default;
    private int _timingIndex = 0;

    /// <summary>�L���X�g�̔���</summary>
    public bool IsCast { get; private set; } = default;

    /// <summary> �A�j���[�^�[ </summary>
    public Animator Animator { get; private set; } = default;

    /// <summary>���݂̍U���s��</summary>
    private BossAttackAction _currentAttackAction = default;
    /// <summary>���݂̈ړ��s��</summary>
    private BossMoveAction _currentMoveAction = default;
    /// <summary>���݂�</summary>
    private BossAttackAction _currentSuperAttackAction = default;

    /// <summary>�s��"�p�^�[��"�̃C���f�b�N�X</summary>
    private int _patternIndex = -1;
    /// <summary>"�A�N�V����"�̃C���f�b�N�X</summary>
    private int _actionIndex = 0;
    protected override void Awake()
    {
        base.Awake();
        //�A�N�V�����I�����̏�����o�^����
        _data.ActionPatterns.ForEach(x => x.BossAttackActions.ForEach(x => x.ActinoEnd = JudgeAction));

        //�K�E�Z�I�����̍s����o�^����
        _data.BossSuperAttackActions.ForEach(x => x.ActinoEnd = () =>
        {
            //�I�����ɌĂяo�����֐����Ă�
            _currentSuperAttackAction.Exit(this);
            //���g����ɂ���(Update�̓��e�����s����Ȃ��悤�ɂ��邽��)
            _currentSuperAttackAction = null;
            //���i�̍s���p�^�[���ɖ߂�
            RandomPatternChange();
        });

        //�K�E�Z�𔭓�����HP�̃^�C�~���O�̐ݒ�~�X��h�����߂ɍ~��(�傫����)�ɕ��בւ���
        _superAttackTimingHp = _superAttackTimingHp.OrderByDescending(x => x).ToArray();
    }

    void Start()
    {
        RandomPatternChange();    
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
        _currentSuperAttackAction?.ManagedUpdate(this);
    }

    protected override void OnGetDamage()
    {
        if(_timingIndex >= _data.BossSuperAttackActions.Count)
        {
            //�K�E�Z��K�v�ȏ�ɔ��������Ȃ����߂ɔz��̗v�f���ȏ�̎��ɃA�N�Z�X������Return����
            return;
        }

        //�K�E�킴�𔭓�����Hp�ɓ��B������K�E�Z�����s����
        if(_superAttackTimingHp[_timingIndex] >= EnemyHp) SuperAttack();
    }
    
    /// <summary>
    /// �K�E�Z�̏���
    /// </summary>
    private void SuperAttack()
    {
        Debug.Log($"�{�X��HP��{EnemyHp}\n{_timingIndex}�Ԗڂ̕K�E�Z�����s���܂�");

        /// �ʏ펞�̃A�N�V�����ƕK�E�Z�̍Ō�ɍs���֐����Ă�
        _currentAttackAction?.Exit(this);
        _currentMoveAction?.Exit(this);
        _currentSuperAttackAction?.Exit(this);

        /// �ʏ펞�̃A�N�V�����ƕK�E�Z����ɂ���
        _currentAttackAction = null;
        _currentMoveAction = null;
        _currentSuperAttackAction = null;

        /// �K�E�Z�̃A�N�V������ǉ�����
        _currentSuperAttackAction = _data.BossSuperAttackActions[_timingIndex];
        //�K�E�Z�̏���̓��������s����
        _currentSuperAttackAction.Enter(this);

        _timingIndex++;
    }

    /// <summary>
    /// ���ݍs���Ă���A�N�V�����𔻒肷��
    /// ����ɉ����ăp�^�[�����̓A�N�V������؂�ւ���
    /// </summary>
    private void JudgeAction()
    {
        Debug.Log("�A�N�V�����𔻒肵�܂�");
        _actionIndex++;

        if(_actionIndex >= _data.ActionPatterns[_patternIndex].BossAttackActions.Count)
        {
            //�s���p�^�[�������s����������
            RandomPatternChange();//�s���p�^�[����ς���
        }
        else//�s���p�^�[�������s�������Ă��Ȃ�������
        {
            //�A�N�V���������̂��̂ɐ؂�ւ���
            ActionChange(_data.ActionPatterns[_patternIndex].BossAttackActions[_actionIndex],
                            _data.ActionPatterns[_patternIndex].BossMoveActions[_actionIndex]);
        }
    }

    /// <summary>
    /// �s���p�^�[���������_���ɕς���
    /// </summary>
    private void RandomPatternChange()
    {
        //�s���p�^�[���̃C���b�f�N�X�����߂�
        _patternIndex = Random.Range(0, _data.ActionPatterns.Count);
        Debug.Log($"�p�^�[��{_patternIndex}�����s����");

        //�A�N�V�����̓��Z�b�g����邽��Index��0�ɂ���
        _actionIndex = 0;

        //�A�N�V������ύX�����s����
        ActionChange(_data.ActionPatterns[_patternIndex].BossAttackActions.FirstOrDefault(),
                     _data.ActionPatterns[_patternIndex].BossMoveActions.FirstOrDefault());
    }

    /// <summary>
    /// �A�N�V������ύX�����s����
    /// </summary>
    /// <param name="bossAttackAction"></param>
    /// <param name="bossMoveAction"></param>
    private void ActionChange(BossAttackAction bossAttackAction, BossMoveAction bossMoveAction)
    {
        //���݂̃A�N�V�����̍Ō�ɍs���֐����s����
        _currentAttackAction?.Exit(this);
        _currentMoveAction?.Exit(this);

        //�A�N�V�����̒��g��؂�ւ���
        _currentAttackAction = bossAttackAction;
        _currentMoveAction = bossMoveAction;

        Debug.Log($"{_currentAttackAction.gameObject.name},��{_currentMoveAction.gameObject.name}�ɃA�N�V������؂�ւ��܂�");

        //�؂�ւ�����̃A�N�V�����̍ŏ��ɍs���֐������s����
        _currentAttackAction?.Enter(this);
        _currentMoveAction?.Enter(this);
    }

    protected override void OnGameZoneTag(Collider2D collision)
    {
        //�{�X�̓Q�[���]�[���ɐG��Ă��j�����Ăق����Ȃ����ߋL�q�����Ȃ�
    }

    protected override void OnKilledByPlayer()
    {

        GameManager.Instance.StageClear();
        base.OnKilledByPlayer();
    }

    /// <summary>
    /// �҂��Ƃ����s����֐�
    /// �A�b�v�f�[�g�̎��s��҂�
    /// </summary>
    /// <param name="castTime">�҂���</param>
    public void Cast(float castTime)
    {
        StopCoroutine(Casting(castTime));
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

}
