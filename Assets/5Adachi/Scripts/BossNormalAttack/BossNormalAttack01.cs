using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNormalAttack01 : MonoBehaviour
{
    /// <summary>����</summary>
    Vector3 _dir;
    /// <summary>�v���C���[�̃I�u�W�F�N�g</summary>
    private GameObject _player;
    /// <summary>�v���C���[�̃^�O</summary>
    [SerializeField,Header("player��tag")] string _playerTag = null;
    /// <summary>�o���b�g�𔭎˂���|�W�V����</summary>
    [SerializeField, Header("Bullet�𔭎˂���|�W�V����")] Transform[] _muzzles = null;
    /// <summary>�U���p�x</summary>
    [SerializeField, Header("�U���p�x(�b)")] private float _attackInterval = 0.6f;
    /// <summary>���˂���e��ݒ�ł���</summary>
    [SerializeField, Header("���˂���e�̐ݒ�")] PoolObjectType _bullet;

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
            //�^�[�Q�b�g�i�v���C���[�j�̕������v�Z
            _dir = (_player.transform.position - _muzzles[0].transform.position);
            //�^�[�Q�b�g�i�v���C���[�j�̕����ɉ�]
            _muzzles[0].transform.rotation = Quaternion.FromToRotation(Vector3.up, _dir);
     
            //�e�I�u�W�F�N�g�̃}�Y��
            
            //�e���}�Y��0�̌����ɍ��킹�Ēe�𔭎�
            ObjectPool.Instance.UseObject(_muzzles[0].position, _bullet).transform.rotation = _muzzles[0].rotation;

            //�q�I�u�W�F�N�g�̃}�Y��

            //�e���}�Y��1�̌����ɍ��킹�Ēe�𔭎ˁi�e�I�u�W�F�N�g�̒e���E���j
            ObjectPool.Instance.UseObject(_muzzles[1].position, _bullet).transform.rotation= _muzzles[1].rotation;
            
            //�e���}�Y��2�̌����ɍ��킹�Ēe�𔭎ˁi�e�I�u�W�F�N�g�̒e��荶���j
            ObjectPool.Instance.UseObject(_muzzles[2].position, _bullet).transform.rotation = _muzzles[2].rotation;

            //�e���}�Y��3�̌����ɍ��킹�Ēe�𔭎ˁi�e�I�u�W�F�N�g�̒e���E���j
            ObjectPool.Instance.UseObject(_muzzles[3].position, _bullet).transform.rotation = _muzzles[3].rotation;

            //�e���}�Y��4�̌����ɍ��킹�Ēe�𔭎ˁi�e�I�u�W�F�N�g�̒e��荶���j
            ObjectPool.Instance.UseObject(_muzzles[4].position, _bullet).transform.rotation = _muzzles[4].rotation;

            yield return new WaitForSeconds(_attackInterval);
        }
    }
}
