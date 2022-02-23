using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNormalAttack03 : MonoBehaviour
{
    /// <summary>����</summary>
    Vector3 _dir;
    /// <summary>�v���C���[�̃I�u�W�F�N�g</summary>
    private GameObject _player;
    /// <summary>�}�Y���̊p�x�Ԋu</summary>
    float _rotationInterval = 360f;
    /// <summary>���˂�����</summary>
    int _attackCount = 0;
    /// <summary>���˂���ő��</summary>
    [SerializeField,Header("���˂���ő��")] int _maxAttackCount = 7;
    /// <summary>�v���C���[�̃^�O</summary>
    [SerializeField, Header("player��tag")] string _playerTag = null;
    /// <summary>�o���b�g�𔭎˂���|�W�V����</summary>
    [SerializeField, Header("Bullet�𔭎˂���|�W�V����")] Transform[] _muzzles = null;
    /// <summary>�U���p�x</summary>
    [SerializeField, Header("�U���p�x(�b)")] private float _attackInterval = 0.64f;
    /// <summary>���˂���e��ݒ�ł���</summary>
    [SerializeField, Header("���˂���e�̐ݒ�")] PoolObjectType _bullet;
    /// <summary>�ő�̒e��</summary>
    [SerializeField,Header("�ő�̒e�̐�")] int _maximumBulletRange = 11;
    /// <summary>�ŏ��̉�]�l</summary>
    const float MINIMUM_ROTATION_RANGE = 0f;
    /// <summary>�ő�̉�]�l</summary>
    const float MAX_ROTATION_RANGE = 360f;
    /// <summary>�ŏ��̒e��</summary>
    const int MINIMUM_BULLET_RANGE = 3;
    /// <summary>�ő�̒e���̏C���l</summary>
    const float MAX_BULLET_RANGE_OFFSET = 1;
    /// <summary>���ˉ񐔂����Z�b�g</summary>
    const int ATTACK_COUNT_RESET = 0;
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag(_playerTag);//�v���C���[�̃^�O���Ƃ��Ă���
        StartCoroutine(Attack());
    }

    //Attack�֐��ɓ����ʏ�U��
    IEnumerator Attack()
    {
        while (true)
        {
            //���̉񐔔��˂�����
            if(_attackCount >= _maxAttackCount)
            {   //1��̍U���Œe���΂���(3�`?)
                _rotationInterval = (MAX_ROTATION_RANGE / Random.Range(MINIMUM_BULLET_RANGE, _maximumBulletRange + MAX_BULLET_RANGE_OFFSET));
                _attackCount = ATTACK_COUNT_RESET;//���ˉ񐔂����Z�b�g
            }
            //�^�[�Q�b�g�i�v���C���[�j�̕������v�Z
            _dir = (_player.transform.position - _muzzles[0].transform.position);
            //�^�[�Q�b�g�i�v���C���[�j�̕����ɉ�]
            _muzzles[0].transform.rotation = Quaternion.FromToRotation(Vector3.up, _dir);

            //�e���}�Y��0�̌����ɍ��킹�Ēe�𔭎�
            ObjectPool.Instance.UseObject(_muzzles[0].position, _bullet).transform.rotation = _muzzles[0].rotation;

            Vector3 firstLocalAngle = _muzzles[0].localEulerAngles;// ���[�J�����W����Ɏ擾

            //���������𐔉�(_maximumCount)�J��Ԃ�
            for (float rotation = MINIMUM_ROTATION_RANGE + firstLocalAngle.z; rotation <= MAX_ROTATION_RANGE + firstLocalAngle.z; rotation += _rotationInterval)
            {
                Vector3 secondLocalAngle = _muzzles[1].localEulerAngles;// ���[�J�����W����Ɏ擾
                secondLocalAngle.z = rotation;// �p�x��ݒ�
                _muzzles[1].localEulerAngles = secondLocalAngle;//��]����
                //�e���}�Y���̌����ɍ��킹�Ēe�𔭎ˁi����Bomb�ɂ��Ă܂��j
                ObjectPool.Instance.UseObject(_muzzles[1].position, _bullet).transform.rotation = _muzzles[1].rotation;
            }
            _attackCount++;//���ˉ񐔂��P����
            yield return new WaitForSeconds(_attackInterval);
        }
    }
}
