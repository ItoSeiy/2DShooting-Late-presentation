using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperAttackRandom : MonoBehaviour
{
    /// <summary>�`���傫���̊T�O������������</summary>
    Rigidbody2D _rb;    
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
    /// <summary>�K�E�O�Ɉړ�����|�W�V����</summary>
    [SerializeField, Header("�K�E�O�Ɉړ�����|�W�V����")] Vector2 _superAttackPosition = new Vector2(0f, 4f);
    /// <summary>�K�E�O�Ɉړ�����Ƃ��̃X�s�[�h</summary>
    [SerializeField, Header("�K�E�O�Ɉړ�����Ƃ��̃X�s�[�h")] float _speed = 4f;
    /// <summary>�o���b�g�𔭎˂���|�W�V����</summary>
    [SerializeField, Header("Bullet�𔭎˂���|�W�V����")] Transform[] _muzzles = null;    
    /// <summary>�K�E�Z�ҋ@����</summary>
    [SerializeField, Header("�K�E�Z�ҋ@����")] float _waitTime = 5f;
    /// <summary>�K�E�Z��������</summary>
    [SerializeField, Header("�K�E�Z��������")] float _activationTime = 30f;
    /// <summary>�}�Y���̊p�x�Ԋu</summary>
    [SerializeField, Header("�}�Y���̊p�x�Ԋu")] float _rotationInterval = 2f;
    /// <summary>���˂���e��ݒ�ł���</summary>
    [SerializeField, Header("���˂���e�̐ݒ�")] PoolObjectType _bullet;
    /// <summary>1��̏����Œe�𔭎˂����</summary>
    [SerializeField,Header("1��̏����Œe�𔭎˂����")] int _maximumCount = 2;
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
    /// <summary>1��̏����Œe�𔭎˂���񐔂̏����l</summary>
    const int INITIAL_COUNT = 0;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();//�s�X�^�[�g�t�ŃQ�b�g�R���|�[�l���g
        StartCoroutine(Spiral()); //�R���[�`���𔭓�    
    }
    void Update()
    {
        _timer += Time.deltaTime;//�^�C�}�[
    }

    /// <summary>�Q���̂悤�ȋO���A�����v���ɔ���</summary>
    IEnumerator Spiral()
    {
        _timer = RESET_TIME;//�^�C�����Z�b�g

        //�K�E����Ƃ���BOSS�͕��O�ɂ���0�A�x��2���̈ʒu(��)�ɁA�ړ�����
        while (true)
        {
            yield return new WaitForSeconds(JUDGMENT_TIME);//����񐔂̐���
            //������
            _horizontalDir = _superAttackPosition.x - transform.position.x;
            //�c����
            _verticalDir = _superAttackPosition.y - transform.position.y;
            //���͈̔͂̏�����      
            _rightRange = transform.position.x < _superAttackPosition.x + PLAYER_POS_OFFSET;
            _leftRange = transform.position.x > _superAttackPosition.x - PLAYER_POS_OFFSET;
            //�c�͈̔͂̏�����
            _upperRange = transform.position.y < _superAttackPosition.y + PLAYER_POS_OFFSET;
            _downRange = transform.position.y > _superAttackPosition.y - PLAYER_POS_OFFSET;
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
                transform.position = _superAttackPosition;//�{�X�̈ʒu���C��
                break;//�I���
            }
        }
 
        _timer = 0f;//�^�C�����Z�b�g

        //�K�E�Z����
        while (true)
        {
            //���������𐔉�(_maximumCount)�J��Ԃ�
            for (int count = INITIAL_COUNT; count < _maximumCount; count++)
            {
                ///�}�Y������]����///
                Vector3 localAngle = _muzzles[0].localEulerAngles;// ���[�J�����W����Ɏ擾
                // �����_���Ȋp�x��ݒ�i�i�@0�x�@�`�@360�x/�}�Y���̊p�x�Ԋu�@�j* �}�Y���̊p�x�Ԋu�@)
                localAngle.z = Random.Range(MINIMUM_ROTATION_RANGE,MAXIMUM_ROTATION_RANGE / _rotationInterval) * _rotationInterval;
                _muzzles[0].localEulerAngles = localAngle;//��]����
                //�e���}�Y���̌����ɍ��킹�Ēe�𔭎�
                ObjectPool.Instance.UseObject(_muzzles[0].position, _bullet).transform.rotation = _muzzles[0].rotation;
            }

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
