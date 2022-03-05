using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperAttackDelayFollow : BossAttackAction
{
    /// <summary>�^�C�}�[</summary>
    float _timer = 0f;
    /// <summary>�E���͈̔�</summary>
    bool _rightRange;
    /// <summary>�����͈̔�</summary>
    bool _leftRange;
    /// <summary>�㑤�͈̔�</summary>
    bool _upperRange;
    /// <summary>�����͈̔�</summary>
    bool _downRange;
    /// <summary>������</summary>
    float _horizontalDir = 0f;
    /// <summary>�c����</summary>
    float _verticalDir = 0f;
    /// <summary>�e�̌����ڂ̎��</summary>
    int _pattern = 0;
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
    [SerializeField, Header("�}�Y���̊p�x�Ԋu")] float _angleInterval = 20f;
    /// <summary>�U���p�x</summary>
    [SerializeField, Header("�U���p�x(�b)")] float _attackInterval = 0.3f;
    /// <summary>���˂���e��ݒ�ł���</summary>
    [SerializeField, Header("���˂���e�̐ݒ�(�f�B���C�t�H��-)")]  PoolObjectType[] _bullet;
    /// <summary>�C���l</summary>
    const float PLAYER_POS_OFFSET = 0.5f;
    /// <summary>����񐔂̐���</summary>
    const float JUDGMENT_TIME = 1 / 60f;
    /// <summary>���Z�b�g�^�C�}�[</summary>
    const float RESET_TIME = 0f;
    /// <summary>�ŏ��̉�]�l</summary>
    const float FIRST_ROTATION = 95f;
    /// <summary>�Ō�̉�]�l</summary>
    const float LAST_ROTATION = 265f;
    /// <summary>�^�񒆂͈̔�</summary>
    const float MIDDLE_RANGE = 135f;
    /// <summary>�^�񒆂͈̔͂̏C���l</summary>
    const float MIDDLE_RANGE_OFFSET = 70f;

    public override System.Action ActinoEnd { get; set; }

    public override void Enter(BossController contlloer)
    {
        contlloer.ItemDrop();
        StartCoroutine(DelayFollow(contlloer)); //�R���[�`���𔭓�
    }

    public override void ManagedUpdate(BossController contlloer)
    {
        _timer += Time.deltaTime;//�^�C�}�[

        if(_timer >= _activationTime)
        {
            ActinoEnd?.Invoke();
        }
    }

    public override void Exit(BossController contlloer)
    {
        StopAllCoroutines();
    }
    
    /// <summary>���̕����ɒe�𔭎˂�����A�v���C���[�̕��ɒe�����ł����K�E�Z</summary>
    IEnumerator DelayFollow(BossController controller)
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
                //Debug.Log("���ʂ�" + _rightRange + _leftRange + _upperRange + _downRange);
                //�X���[�Y�Ɉړ�
                controller.Rb.velocity = new Vector2(_horizontalDir, _verticalDir) * _speed;
            }
            //����������
            else
            {
                //Debug.Log("���ʂ�" + _rightRange + _leftRange + _upperRange + _downRange);
                //���肵�Ĉړ�
                controller.Rb.velocity = new Vector2(_horizontalDir, _verticalDir).normalized * _speed;
            }

            //���b�o������
            if (_timer >= _waitTime)
            {
                //Debug.Log("stop");
                controller.Rb.velocity = Vector2.zero;//��~
                controller.transform.position = _superAttackPosition;//�{�X�̈ʒu���C��
                break;//�I���
            }
        }
        
        _timer = 0f;//�^�C�����Z�b�g

        //�K�E�Z����
        while (true)
        {
            _pattern = Random.Range(0, _bullet.Length);
            //���S�̂ɔ���
            for (float angle = FIRST_ROTATION; angle <= LAST_ROTATION; angle += _angleInterval)//95�x�`135�x�A225�x�`265�x�͈̔�
            {
                Vector3 localAngle = _muzzle.localEulerAngles;// ���[�J�����W����Ɏ擾
                localAngle.z = angle;// �p�x��ݒ�
                _muzzle.localEulerAngles = localAngle;//��]����
                //�e���}�Y���̌����ɍ��킹�Ēe�𔭎ˁi���Ԃ��o�ƃv���C���[�Ɉ�u�����Ǐ]����Bullet���g���܂��j
                ObjectPool.Instance.UseObject(_muzzle.position, _bullet[_pattern]).transform.rotation = _muzzle.rotation;
                //�^�񒆂�����͔��˂��Ȃ�
                if (angle == MIDDLE_RANGE)
                {
                    angle = MIDDLE_RANGE + MIDDLE_RANGE_OFFSET;//�^�񒆂�����ɂ͔��˂��Ȃ��̂ŃX�L�b�v����
                }
            }
            yield return new WaitForSeconds(_attackInterval);//�U���p�x(�b)
            //���b�o������
            if (_timer >= _activationTime)
            {
                break;//�I��
            }
        }
        yield break;//�I��
    }

}
