using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNormalAttack02 : MonoBehaviour
{
    /// <summary>����</summary>
    Vector3 _dir;
    /// <summary>�v���C���[�̃I�u�W�F�N�g</summary>
    private GameObject _player;
    /// <summary>�e�̌����ڂ̎��</summary>
    int _firstPattern = 0;
    /// <summary>�e�̌����ڂ̎��</summary>
    int _secondPattern = 0;
    /// <summary>�v���C���[�̃^�O</summary>
    [SerializeField, Header("player��tag")] string _playerTag = null;
    /// <summary>�o���b�g�𔭎˂���|�W�V����</summary>
    [SerializeField, Header("Bullet�𔭎˂���|�W�V����")] Transform[] _muzzles = null;
    /// <summary>�U���p�x</summary>
    [SerializeField, Header("�U���p�x(�b)")] private float _attackInterval = 0.5f;
    /// <summary>���˂���e��ݒ�ł���</summary>
    [SerializeField, Header("���˂���e�̐ݒ�")] PoolObjectType[] _firstBullet;
    /// <summary>���˂���e��ݒ�ł���</summary>
    [SerializeField, Header("���˂���e�̐ݒ�")] PoolObjectType[] _secondBullet;
    /// <summary>�΍��p</summary>
    const float OPPOSITE_ANGLE = 180f;
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
            //�e�̌����ڂ������_���ŕς���
            _firstPattern = Random.Range(0, _firstBullet.Length);
            _secondPattern = Random.Range(0, _secondBullet.Length);

            ///�v���C���[�̌����Ƀ}�Y��������///

            //�^�[�Q�b�g�i�v���C���[�j�̕������v�Z
            _dir = (_player.transform.position - _muzzles[0].transform.position);
            //�^�[�Q�b�g�i�v���C���[�j�̕����ɉ�]
            _muzzles[0].transform.rotation = Quaternion.FromToRotation(Vector3.up, _dir);

            //�e���}�Y��0�̌����ɍ��킹�Ēe�𔭎�
            ObjectPool.Instance.UseObject(_muzzles[0].position, _firstBullet[_firstPattern]).transform.rotation = _muzzles[0].rotation;

            ///�v���C���[����������Ƌt(X���t����)�Ƀ}�Y��������///
            //�}�Y������]����
            Vector3 localAngle = _muzzles[0].localEulerAngles;// ���[�J�����W����Ɏ擾           
            localAngle.z = -localAngle.z;//�������t���ɂ���
            _muzzles[0].localEulerAngles = localAngle;//��]����
            //�e���}�Y���̌����ɍ��킹�Ēe�𔭎�
            ObjectPool.Instance.UseObject(_muzzles[0].position, _secondBullet[_secondPattern]).transform.rotation = _muzzles[0].rotation;

            ///�v���C���[����������Ƌt(X��Y���t�����j///
            _dir = (_player.transform.position - _muzzles[0].transform.position);
            //�^�[�Q�b�g�i�v���C���[�j�̕����ɉ�]
            _muzzles[0].transform.rotation = Quaternion.FromToRotation(Vector3.up, -_dir);

            //�e���}�Y��0�̌����ɍ��킹�Ēe�𔭎�
            ObjectPool.Instance.UseObject(_muzzles[0].position, _secondBullet[_secondPattern]).transform.rotation = _muzzles[0].rotation;

            ///�v���C���[����������Ƌt(Y���t����///
            //�}�Y������]����  
            localAngle.z = -localAngle.z + OPPOSITE_ANGLE;//�������t���ɂ���
            _muzzles[0].localEulerAngles = -localAngle;//��]����
            //�e���}�Y���̌����ɍ��킹�Ēe�𔭎�
            ObjectPool.Instance.UseObject(_muzzles[0].position, _firstBullet[_firstPattern]).transform.rotation = _muzzles[0].rotation;

            yield return new WaitForSeconds(_attackInterval);//�U���p�x(�b)
        }
    }
}
