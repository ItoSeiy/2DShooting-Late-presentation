using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNormalAttack04 : MonoBehaviour
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
    [SerializeField, Header("�}�Y���̊p�x�Ԋu")] float _rotationInterval = 20f;
    /// <summary>�ŏ��̉�]�l</summary>
    const float MINIMUM_ROT_RANGE = 0f;
    /// <summary>�ő�̉�]�l</summary>
    const float MAXIMUM_ROT_RANGE = 360f;

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


            Vector3 firstLocalAngle = _muzzles[0].localEulerAngles;// ���[�J�����W����Ɏ擾

            //�e���}�Y��0�̌����ɍ��킹�Ēe�𔭎�
            ObjectPool.Instance.UseObject(_muzzles[0].position, _bullet).transform.rotation = _muzzles[0].rotation;

            //_rotationInterval�Őݒ肵���p�x�Ԋu�őS���ʂɔ��˂���
            for (float rotation = MINIMUM_ROT_RANGE + firstLocalAngle.z; rotation < MAXIMUM_ROT_RANGE + firstLocalAngle.z; rotation += _rotationInterval)
            {
                ///�}�Y��1����]����///
                Vector3 secondLocalAngle = _muzzles[1].localEulerAngles;// ���[�J�����W����Ɏ擾
                //�p�x��ݒ�
                secondLocalAngle.z = rotation;
                _muzzles[1].localEulerAngles = secondLocalAngle;//��]����
                //�e���}�Y��1�̌����ɍ��킹�Ēe�𔭎�
                ObjectPool.Instance.UseObject(_muzzles[1].position, _bullet).transform.rotation = _muzzles[1].rotation;
            }

            yield return new WaitForSeconds(_attackInterval);
        }
    }
}
