using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperAttackWindmill : BossAttackAction
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
    int _firstPattern = 0;
    /// <summary>�e�̌����ڂ̎��</summary>
    int _secondPattern = 0;
    /// <summary>�K�E�O�Ɉړ�����|�W�V����</summary>
    [SerializeField, Header("�K�E�O�Ɉړ�����|�W�V����")] Vector2 _superAttackPosition = new Vector2(0f, 4f);
    /// <summary>�o���b�g�𔭎˂���|�W�V����</summary>
    [SerializeField, Header("Bullet�𔭎˂���|�W�V����")] Transform[] _muzzles = null;
    /// <summary>�K�E�O�Ɉړ�����Ƃ��̃X�s�[�h</summary>
    [SerializeField, Header("�K�E�O�Ɉړ�����Ƃ��̃X�s�[�h")] float _speed = 4f;
    /// <summary>�K�E�Z�ҋ@����</summary>
    [SerializeField, Header("�K�E�Z�ҋ@����")] float _waitTime = 5f;
    /// <summary>�K�E�Z��������</summary>
    [SerializeField, Header("�K�E�Z��������")] float _activationTime = 30f;
    /// <summary>�U���p�x</summary>
    [SerializeField, Header("�U���p�x(�b)")] float _attackInterval = 0.2f;
    /// <summary>�}�Y���̊p�x�Ԋu</summary>
    [SerializeField, Header("�}�Y���̊p�x�Ԋu")] float _angleInterval = 10f;
    /// <summary>���˂���e��ݒ�ł���</summary>
    [SerializeField, Header("���˂���e�̐ݒ�")] PoolObjectType[] _bullet;
    /// <summary>�C���l</summary>
    const float PLAYER_POS_OFFSET = 0.5f;
    /// <summary>����񐔂̐���</summary>
    const float JUDGMENT_TIME = 1 / 60f;
    /// <summary>���Z�b�g�^�C�}�[</summary>
    const float RESET_TIME = 0f;

    public override System.Action ActinoEnd { get; set; }

    public override void Enter(BossController contlloer)
    {
        contlloer.ItemDrop();
        StartCoroutine(Windmill(contlloer)); //�R���[�`���𔭓� 
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
        StopAllCoroutines();
    }

    /// <summary>���Ԃ̂悤�ȋO���A�����v���ɔ���</summary>
    IEnumerator Windmill(BossController controller)
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
            //�����߂�������
            if (_rightRange && _leftRange && _upperRange && _downRange)
            {
                Debug.Log("���ʂ�" + _rightRange + _leftRange + _upperRange + _downRange);
                //�X���[�Y�ɓ���
                controller.Rb.velocity = new Vector2(_horizontalDir, _verticalDir) * _speed;
            }
            //����������
            else
            {
                Debug.Log("���ʂ�" + _rightRange + _leftRange + _upperRange + _downRange);
                //���肵�����x�ňړ�
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

        _timer = RESET_TIME;//�^�C�����Z�b�g

        //�K�E�Z����
        while (true)
        {
            //�e�̌����ڂ�ς���
            _firstPattern = Random.Range(0, _bullet.Length);
            _secondPattern = Random.Range(0, _bullet.Length);
            //�e�I�u�W�F�N�g

            //�}�Y������]����
            Vector3 firstLocalAngle = _muzzles[0].localEulerAngles;// ���[�J�����W����Ɏ擾
            firstLocalAngle.z += _angleInterval;// �p�x��ݒ�
            _muzzles[0].localEulerAngles = firstLocalAngle;//��]����

            //�e���}�Y��0�̌����ɍ��킹�Ēe�𔭎�
            ObjectPool.Instance.UseObject(_muzzles[0].position, _bullet[_firstPattern]).transform.rotation = _muzzles[0].rotation;

            //�q�I�u�W�F�N�g

            //�e���}�Y��1�̌����ɍ��킹�Ēe�𔭎�
            ObjectPool.Instance.UseObject(_muzzles[1].position, _bullet[_secondPattern]).transform.rotation = _muzzles[1].rotation;

            //�e���}�Y��2�̌����ɍ��킹�Ēe�𔭎�
            ObjectPool.Instance.UseObject(_muzzles[2].position, _bullet[_firstPattern]).transform.rotation = _muzzles[2].rotation;

            //�e���}�Y��3�̌����ɍ��킹�Ēe�𔭎�
            ObjectPool.Instance.UseObject(_muzzles[3].position, _bullet[_secondPattern]).transform.rotation = _muzzles[3].rotation;

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
