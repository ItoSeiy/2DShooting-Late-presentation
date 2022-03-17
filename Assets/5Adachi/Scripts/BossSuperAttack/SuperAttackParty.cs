using Overdose.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SuperAttackParty : BossAttackAction
{
    /// <summary>�E���͈̔�</summary>
    bool _rightRange;
    /// <summary>�����͈̔�</summary>
    bool _leftRange;
    /// <summary>�㑤�͈̔�</summary>
    bool _upperRange;
    /// <summary>�����͈̔�</summary>
    bool _downRange;
    /// <summary>�^�C�}�[</summary>
    float _timer = 0f;
    /// <summary>������</summary>
    float _horizontalDir = 0f;
    /// <summary>�c����</summary>
    float _verticalDir = 0f;
    /// <summary>�ʏ펞�̔�_���[�W�̊�����ۑ�����</summary>
    float _saveDamageTakenRation = 1f;
    /// <summary>�e�̌����ڂ̎��</summary>
    int _pattern = 0;
    /// <summary>�U����</summary>
    int _attackCount = 0;
    /// <summary>�K�E�O�Ɉړ�����|�W�V����</summary>
    [SerializeField, Header("�K�E�O�Ɉړ�����|�W�V����")] Vector2 _superAttackPosition = new Vector2(0f, 4f);
    /// <summary>�o���b�g�𔭎˂���|�W�V����</summary>
    [SerializeField, Header("Bullet�𔭎˂���|�W�V����")] Transform _muzzle = null;
    /// <summary>�K�E�O�Ɉړ�����Ƃ��̃X�s�[�h</summary>
    [SerializeField, Header("�K�E�O�Ɉړ�����Ƃ��̃X�s�[�h")] float _speed = 4f;
    /// <summary>�K�E�Z�ҋ@����</summary>
    [SerializeField, Header("�K�E�Z�ҋ@����")] float _waitTime = 5f;
    /// <summary>�K�E�Z��������</summary>
    [SerializeField, Header("�K�E�Z��������")] float _activationTime = 30f;
    /// <summary>�}�Y���̊p�x�Ԋu</summary>
    [SerializeField, Header("�}�Y���̊p�x�Ԋu")] float _angleInterval = 4f;
    /// <summary>���˂���e��ݒ�ł���</summary>
    [SerializeField, Header("���˂���e�̐ݒ�(���o�E���h)")] PoolObjectType[] _bullet;
    /// <summary>��_���[�W�̊���</summary>
    [SerializeField, Header("��_���[�W�̊���"), Range(0, 1)] float _damageTakenRationRange = 0.5f;
    /// <summary>�{�X�̕K�E�Z�̃^�C�����C��</summary>
    [SerializeField, Header("�{�X�̕K�E�Z�̃^�C�����C��")] PlayableDirector _Introduction = null;
    /// <summary>�U�����̉�</summary>
    [SerializeField, Header("�U�����̉�")] SoundType _superAttack;
    /// <summary>����炷�^�C�~���O</summary>
    [SerializeField, Header("����炷�^�C�~���O")] int _maxAttackCount = 5;
    /// <summary>�^�C�����C������������</summary>
    [SerializeField, Header("�^�C�����C������������")] float _introductionStopTime = 3f;
    /// <summary>�C���l</summary>
    const float PLAYER_POS_OFFSET = 0.5f;
    /// <summary>����񐔂̐���</summary>
    const float JUDGMENT_TIME = 1 / 60f;
    /// <summary>���Z�b�g�^�C�}�[</summary>
    const float RESET_TIME = 0f;
    /// <summary>�ŏ��̉�]�l</summary>
    const float MINIMUM_ROTATION_RANGE = 0f;
    /// <summary>�ő�̉�]�l</summary>
    const float MAXIMUM_ROTATION_RANGE = 360f;

    public override System.Action ActinoEnd { get; set; }

    public override void Enter(BossController contlloer)
    {
        contlloer.ItemDrop();
        //�ʏ펞�̔�_���[�W�̊�����ۑ�����
        _saveDamageTakenRation = contlloer.DamageTakenRation;
        //��_���[�W�̊�����ύX����
        contlloer.DamageTakenRation = _damageTakenRationRange;
        StartCoroutine(Party(contlloer)); //�R���[�`���𔭓�
    }

    public override void ManagedUpdate(BossController contlloer)
    {
        _timer += Time.deltaTime;//�^�C�}�[

        if (_timer >= _activationTime)
        {
            ActinoEnd?.Invoke();
        }
    }

    public override void Exit(BossController contlloer)
    {
        //��_���[�W�̊������������ɖ߂�
        contlloer.DamageTakenRation = _saveDamageTakenRation;
        StopAllCoroutines();
    }

    /// <summary>�����_���ȕ����ɒ��˕Ԃ�e�𔭎˂���K�E�Z</summary>
    IEnumerator Party(BossController controller)
    {


        _timer = RESET_TIME;//�^�C�����Z�b�g

        //�K�E����Ƃ���BOSS�͕��O�ɂ���0�A�x��2���̈ʒu(��)�ɁA�ړ�����
        while (true)
        {
            yield return new WaitForSeconds(JUDGMENT_TIME);//����񐔂̐���
            //������
            _horizontalDir = _superAttackPosition.x - controller.transform.position.x;
            //�c����
            _verticalDir = _superAttackPosition.y - controller.transform.position.y;
            //���͈̔͂̏�����      
            _rightRange = controller.transform.position.x < _superAttackPosition.x + PLAYER_POS_OFFSET;
            _leftRange = controller.transform.position.x > _superAttackPosition.x - PLAYER_POS_OFFSET;
            //�c�͈̔͂̏�����
            _upperRange = controller.transform.position.y < _superAttackPosition.y + PLAYER_POS_OFFSET;
            _downRange = controller.transform.position.y > _superAttackPosition.y - PLAYER_POS_OFFSET;
            //�s�������|�W�V�����Ɉړ�����
            //�߂�������
            if (_rightRange && _leftRange && _upperRange && _downRange)
            {
                Debug.Log("���ʂ�" + _rightRange + _leftRange + _upperRange + _downRange);
                //�X���[�Y�Ɉړ�
                controller.Rb.velocity = new Vector2(_horizontalDir, _verticalDir) * _speed;
            }
            //����������
            else
            {
                Debug.Log("���ʂ�" + _rightRange + _leftRange + _upperRange + _downRange);
                //���肵�Ĉړ�
                controller.Rb.velocity = new Vector2(_horizontalDir, _verticalDir).normalized * _speed;
            }

            //���b�o������
            if (_timer >= _waitTime)
            {
                Debug.Log("stop");
                controller.Rb.velocity = Vector2.zero;//��~
                controller.transform.position = _superAttackPosition;//�{�X�̈ʒu���C��
                break;//�I���
            }
        }

        _timer = 0f;//�^�C�����Z�b�g

        if (_Introduction)
        {
            _Introduction.gameObject.SetActive(true);
        }

        //�K�E�Z����
        while (true)
        {
            if (_timer >= _introductionStopTime)
            {
                _Introduction.gameObject.SetActive(false);
            }
            if (_attackCount >= _maxAttackCount)
            {
                //�U�����̉�
                SoundManager.Instance.UseSound(_superAttack);
                _attackCount = 0;
            }

            //�e�̌����ڂ�ς���
            _pattern = Random.Range(0, _bullet.Length);
            ///�}�Y������]����///
            Vector3 localAngle = _muzzle.localEulerAngles;// ���[�J�����W����Ɏ擾
            //�����_���Ȋp�x��ݒ�i�i�@0�x�@�`�@360�x/�}�Y���̊p�x�Ԋu�@�j* �}�Y���̊p�x�Ԋu�@�j�͈̔�
            localAngle.z = Random.Range(MINIMUM_ROTATION_RANGE, MAXIMUM_ROTATION_RANGE / _angleInterval) * _angleInterval;
            _muzzle.localEulerAngles = localAngle;//��]����

            //�e���}�Y���̌����ɍ��킹�Ēe�𔭎�
            ObjectPool.Instance.UseObject(_muzzle.position, _bullet[_pattern]).transform.rotation = _muzzle.rotation;

            _attackCount++;

            yield return new WaitForSeconds(JUDGMENT_TIME);//����񐔂̒���

            //���b�o������
            if (_timer >= _activationTime)
            {
                break;//�I��
            }
        }
        yield break;//�I��
    }
}
