using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperAttackEightShape : BossAttackAction
{
    /// <summary>���̃X�N���v�g�Ŏg���^�C�}�[</summary>
    float _defaultTimer = 0f;
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
    /// <summary>�e�̌����ڂ�ς���Ԋu(�b)�̏C���l</summary>
    float _switchIntervalOffset = 0f;
    /// <summary>�e�̌����ڂ̎��</summary>
    int _firstPattern = 0;
    /// <summary>�e�̌����ڂ̎��</summary>
    int _secondPattern = 0;
    /// <summary>�o���b�g�𔭎˂���|�W�V����</summary>
    [SerializeField, Header("Bullet�𔭎˂���|�W�V����")] Transform[] _muzzles = null;
    /// <summary>�K�E�O�Ɉړ�����|�W�V����</summary>
    [SerializeField, Header("�K�E�O�Ɉړ�����|�W�V����")] Vector2 _superAttackPosition = new Vector2(0f, 4f);
    /// <summary>�K�E�O�Ɉړ�����Ƃ��̃X�s�[�h</summary>
    [SerializeField, Header("�K�E�O�Ɉړ�����Ƃ��̃X�s�[�h")] float _speed = 4f;    
    /// <summary>�K�E�Z�ҋ@����</summary>
    [SerializeField, Header("�K�E�Z�ҋ@����")] float _waitTime = 5f;
    /// <summary>�K�E�Z��������</summary>
    [SerializeField, Header("�K�E�Z��������")] float _activationTime = 30f;
    /// <summary>�U���p�x</summary>
    [SerializeField, Header("�U���p�x(�b)")] private float _attackInterval = 0.6f;
    /// <summary>�}�Y���̊p�x�Ԋu</summary>
    [SerializeField, Header("�}�Y���̊p�x�Ԋu")] float _rotationInterval = 10f;
    /// <summary>���˂���e��ݒ�ł���</summary>
    [SerializeField, Header("���˂���e�̐ݒ�")] PoolObjectType[] _bullet;
    /// <summary>�e�̌����ڂ�ς���Ԋu(�b)</summary>
    [SerializeField,Header("�e�̌����ڂ�ς���Ԋu(�b)")] float _switchInterval = 2f;
    /// <summary>���̍s������o�鎞��</summary>
    [SerializeField, Header("���̍s������o�鎞��")] float _endingTime = 30f;
    /// <summary>�C���l</summary>
    const float PLAYER_POS_OFFSET = 0.5f;
    /// <summary>����񐔂̐���</summary>
    const float JUDGMENT_TIME = 1 / 60f;
    /// <summary>���Z�b�g�^�C�}�[</summary>
    const float RESET_TIME = 0f;

    public override System.Action ActinoEnd { get; set; }

    
    public override void Enter(BossController contlloer)
    {
        StartCoroutine(EightShape(contlloer)); //�R���[�`���𔭓�
    }

    public override void ManagedUpdate(BossController contlloer)
    {
        _defaultTimer += Time.deltaTime;//�^�C�}�[
        _timer += Time.deltaTime;

        if(_timer >= _endingTime)
        {
            ActinoEnd?.Invoke();
        }
    }

    public override void Exit(BossController contlloer)
    {
        StopAllCoroutines();
    }

    /// <summary>8�̎��̂悤�ȋO��</summary>
    IEnumerator EightShape(BossController controller)
    {
        _defaultTimer = RESET_TIME;//�^�C�����Z�b�g

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
            if (_defaultTimer >= _waitTime)
            {
                Debug.Log("stop");
                controller.Rb.velocity = Vector2.zero;//��~
                controller.transform.position = _superAttackPosition;//�{�X�̈ʒu���C��
                break;//�I���
            }
        }

        _defaultTimer = RESET_TIME;//�^�C�����Z�b�g

        //�K�E�Z����
        while (true)
        {
            //���b�o���Ƃɒe�̌����ڂ�ς���
            if (_defaultTimer >= _switchInterval + _switchIntervalOffset)
            {
                //�e�̌����ڂ�ς���
                _firstPattern = Random.Range(0, _bullet.Length);
                _secondPattern = Random.Range(0, _bullet.Length);
                //�o�߂����b����ǉ�
                _switchIntervalOffset += _switchInterval;
            }

                //�}�Y��0�i�e�I�u�W�F�N�g�j�𔽎��v���i+�j�ɉ�]����
                Vector3 upperLocalAngle = _muzzles[0].localEulerAngles;// ���[�J�����W����Ɏ擾
                upperLocalAngle.z += _rotationInterval;// �p�x��ݒ�
                _muzzles[0].localEulerAngles = upperLocalAngle;//��]����

                //�e���}�Y��0�i�e�I�u�W�F�N�g�j�̌����ɍ��킹�Ēe�𔭎�
                ObjectPool.Instance.UseObject(_muzzles[0].position, _bullet[_firstPattern]).transform.rotation = _muzzles[0].rotation;

                //�e���}�Y��1�i�q�I�u�W�F�N�g�j�̌����ɍ��킹�Ēe�𔭎�
                ObjectPool.Instance.UseObject(_muzzles[1].position, _bullet[_secondPattern]).transform.rotation = _muzzles[1].rotation;

                //�}�Y��2�i�e�I�u�W�F�N�g�j�����v���i-�j�ɉ�]����
                Vector3 rightLocalAngle = _muzzles[2].localEulerAngles;// ���[�J�����W����Ɏ擾
                rightLocalAngle.z -= _rotationInterval;// �p�x��ݒ�
                _muzzles[2].localEulerAngles = rightLocalAngle;//��]����

                //�e���}�Y��2�i�e�I�u�W�F�N�g�j�̌����ɍ��킹�Ēe�𔭎�
                ObjectPool.Instance.UseObject(_muzzles[2].position, _bullet[_firstPattern]).transform.rotation = _muzzles[2].rotation;
                
                //�e���}�Y��3�i�q�I�u�W�F�N�g�j�̌����ɍ��킹�Ēe�𔭎�
                ObjectPool.Instance.UseObject(_muzzles[3].position, _bullet[_secondPattern]).transform.rotation = _muzzles[3].rotation;

            yield return new WaitForSeconds(_attackInterval);//�U���p�x(�b)
            //���b�o������
            if (_defaultTimer >= _activationTime)
            {
                break;//�I��
            }
        }
        yield break;//�I��
    }

}
