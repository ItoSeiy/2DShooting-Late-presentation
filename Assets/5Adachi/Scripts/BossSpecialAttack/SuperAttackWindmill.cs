using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperAttackWindmill : MonoBehaviour
{
    /// <summary>�`���傫���̊T�O������������</summary>
    Rigidbody2D _rb;
    /// <summary>�K�E�O�Ɉړ�����|�W�V����</summary>
    [SerializeField, Header("�K�E�O�Ɉړ�����|�W�V����")] Transform _superAttackPos = null;
    /// <summary>�o���b�g�𔭎˂���|�W�V����</summary>
    [SerializeField, Header("Bullet�𔭎˂���|�W�V����")] Transform[] _muzzles = null;
    /// <summary>���x</summary>
    [SerializeField, Header("�X�s�[�h")] float _speed = 4f;
    /// <summary>�����̍U������</summary>
    float _initialDamageRatio;
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
    /// <summary>�K�E�Z�ҋ@����</summary>
    [SerializeField, Header("�K�E�Z�ҋ@����")] float _waitTime = 5f;
    /// <summary>�K�E�Z��������</summary>
    [SerializeField, Header("�K�E�Z��������")] float _activationTime = 30f;
    /// <summary>�e���΂��Ԋu</summary>
    [SerializeField, Header("�e���΂��Ԋu")] float _superAttackInterval = 0.2f;
    /// <summary>�C���l</summary>
    const float PLAYER_POS_OFFSET = 0.5f;
    /// <summary>����񐔂̐���</summary>
    const float JUDGMENT_TIME = 1 / 60f;
    /// <summary>0�x�̊p�x</summary>
    const float ZERO_DEGREE_ANGLE = 0f;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();//�s�X�^�[�g�t�ŃQ�b�g�R���|�[�l���g
        StartCoroutine(Super()); //�R���[�`���𔭓�    
    }

    void Update()
    {
        _timer += Time.deltaTime;
    }


    IEnumerator Super()
    {
        _timer = 0f;//�^�C�����Z�b�g

        //�K�E����Ƃ���BOSS�͕��O�ɂ���0�A�x��2���̈ʒu(��)�ɁA�ړ�����
        while (true)
        {
            yield return new WaitForSeconds(JUDGMENT_TIME);//����񐔂̐���
                                                           //������
            _horizontalDir = _superAttackPos.position.x - transform.position.x;
            //�c����
            _verticalDir = _superAttackPos.position.y - transform.position.y;
            //���͈̔͂̏�����      
            _rightRange = transform.position.x < _superAttackPos.position.x + PLAYER_POS_OFFSET;
            _leftRange = transform.position.x > _superAttackPos.position.x - PLAYER_POS_OFFSET;
            //�c�͈̔͂̏�����
            _upperRange = transform.position.y < _superAttackPos.position.y + PLAYER_POS_OFFSET;
            _downRange = transform.position.y > _superAttackPos.position.y - PLAYER_POS_OFFSET;
            //�s�������|�W�V�����Ɉړ�����
            //�߂�������
            if (_rightRange && _leftRange && _upperRange && _downRange)
            {
                Debug.Log("���ʂ�" + _rightRange + _leftRange + _upperRange + _downRange);
                //�X���[�Y�Ɉړ�
                _rb.velocity = new Vector2(_horizontalDir, _verticalDir) * _speed;
            }
            //����������
            else
            {
                Debug.Log("���ʂ�" + _rightRange + _leftRange + _upperRange + _downRange);
                //���肵�Ĉړ�
                _rb.velocity = new Vector2(_horizontalDir, _verticalDir).normalized * _speed;
            }

            //���b�o������
            if (_timer >= _waitTime)
            {
                Debug.Log("stop");
                _rb.velocity = Vector2.zero;//��~
                transform.position = _superAttackPos.position;//�{�X�̈ʒu���C��
                break;//�I���
            }
        }
        //_initialDamageRatio = AddDamageRatio;//�����l��ݒ�
        //AddDamageRatio = 0.5f;//�K�E���͍U��������ύX
        _timer = 0f;//�^�C�����Z�b�g
        while (true)
        {
            //�e�I�u�W�F�N�g

            //1�ڂ̃}�Y��
            Vector3 firstLocalAngle = _muzzles[0].localEulerAngles;// ���[�J�����W����Ɏ擾
            firstLocalAngle.z += 10f;// �p�x��ݒ�
            _muzzles[0].localEulerAngles = firstLocalAngle;//��]����
            //�e�𔭎ˁi����Bomb�ɂ��Ă܂��j
            var firstBossEnemyBullet = ObjectPool.Instance.UseBullet(_muzzles[0].position, PoolObjectType.Player01BombChild);
            //�e���}�Y���̌����ɍ��킹��
            firstBossEnemyBullet.transform.rotation = _muzzles[0].rotation;

            //�q�I�u�W�F�N�g

            //2�ڂ̃}�Y��
            //�e�𔭎ˁi����Bomb�ɂ��Ă܂��j
            var secondBossEnemyBullet = ObjectPool.Instance.UseBullet(_muzzles[1].position, PoolObjectType.Player01BombChild);
            //�e���}�Y���̌����ɍ��킹��
            secondBossEnemyBullet.transform.rotation = _muzzles[1].rotation;

            //3�ڂ̃}�Y��
            //�e�𔭎ˁi����Bomb�ɂ��Ă܂��j
            var thirdBossEnemyBullet = ObjectPool.Instance.UseBullet(_muzzles[2].position, PoolObjectType.Player01BombChild);
            //�e���}�Y���̌����ɍ��킹��
            thirdBossEnemyBullet.transform.rotation = _muzzles[2].rotation;

            //4�ڂ̃}�Y��
            //�e�𔭎ˁi����Bomb�ɂ��Ă܂��j
            var forceBossEnemyBullet = ObjectPool.Instance.UseBullet(_muzzles[3].position, PoolObjectType.Player01BombChild);
            //�e���}�Y���̌����ɍ��킹��
            forceBossEnemyBullet.transform.rotation = _muzzles[3].rotation;

            yield return new WaitForSeconds(_superAttackInterval);//����񐔂̒���

            //���b�o������
            if (_timer >= _activationTime)
            {
                firstLocalAngle.z = ZERO_DEGREE_ANGLE;// �p�x��0�x�ɐݒ�
                _muzzles[0].localEulerAngles = firstLocalAngle;//��~
                break;//�I��
            }
        }

        //AddDamageRatio = _initialDamageRatio;//�U�����������ɖ߂�
        yield break;//�I��
    }
}
