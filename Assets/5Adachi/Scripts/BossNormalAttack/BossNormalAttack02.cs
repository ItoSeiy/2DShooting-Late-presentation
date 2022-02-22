using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNormalAttack02 : MonoBehaviour
{
    /// <summary>�`���傫���̊T�O������������</summary>
    Rigidbody2D _rb;
    /// <summary>����</summary>
    Vector3 _dir;
    /// <summary>�v���C���[�̃I�u�W�F�N�g</summary>
    private GameObject _player;
    /// <summary>�v���C���[�̃^�O</summary>
    [SerializeField, Header("player��tag")] string _playerTag = null;
    /// <summary>�o���b�g�𔭎˂���|�W�V����</summary>
    [SerializeField, Header("Bullet�𔭎˂���|�W�V����")] Transform[] _muzzles = null;
    /// <summary>�U���p�x</summary>
    [SerializeField, Header("�U���p�x(�b)")] private float _attackInterval = 0.64f;
    /// <summary>���˂���e��ݒ�ł���</summary>
    [SerializeField, Header("���˂���e�̐ݒ�")] PoolObjectType _bullet;
    /// <summary>�}�Y���̊p�x�Ԋu</summary>
    [SerializeField, Header("�}�Y���̊p�x�Ԋu")] float _rotationInterval = 1f;
    /// <summary>1��̏����Œe�𔭎˂����</summary>
    [SerializeField, Header("1��̏����Œe�𔭎˂����")] int _maximumCount = 15;
    /// <summary>�ŏ��̉�]�l</summary>
    const float MINIMUM_ROTATION_RANGE = 0f;
    /// <summary>�ő�̉�]�l</summary>
    const float MAXIMUM_ROTATION_RANGE = 360f;
    /// <summary>1��̏����Œe�𔭎˂���񐔂̏����l</summary>
    const int INITIAL_COUNT = 0;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag(_playerTag);
        StartCoroutine(Attack());
    }

    //Attack�֐��ɓ����ʏ�U��
    IEnumerator Attack()
    {
        while (true)
        {
            //�^�[�Q�b�g�i�v���C���[�j�̕������v�Z
            _dir = (_player.transform.position - _muzzles[0].transform.position);
            //�^�[�Q�b�g�i�v���C���[�j�̕����ɉ�]
            _muzzles[0].transform.rotation = Quaternion.FromToRotation(Vector3.up, _dir);

            //�e�I�u�W�F�N�g�̃}�Y��

            //�e���}�Y��0�̌����ɍ��킹�Ēe�𔭎�
            ObjectPool.Instance.UseBullet(_muzzles[0].position, _bullet).transform.rotation = _muzzles[0].rotation;

            //���������𐔉�(_maximumCount)�J��Ԃ�
            for (int count = INITIAL_COUNT; count < _maximumCount; count++)
            {
                ///�}�Y������]����///
                Vector3 localAngle = _muzzles[0].localEulerAngles;// ���[�J�����W����Ɏ擾
                // �����_���Ȋp�x��ݒ�i�i�@0�x�@�`�@360�x/�}�Y���̊p�x�Ԋu�@�j* �}�Y���̊p�x�Ԋu�@)
                localAngle.z = Random.Range(MINIMUM_ROTATION_RANGE, MAXIMUM_ROTATION_RANGE / _rotationInterval) * _rotationInterval;
                _muzzles[0].localEulerAngles = localAngle;//��]����
                //�e���}�Y���̌����ɍ��킹�Ēe�𔭎�
                ObjectPool.Instance.UseBullet(_muzzles[0].position, _bullet).transform.rotation = _muzzles[0].rotation;
            }

            yield return new WaitForSeconds(_attackInterval);
        }
    }
}
