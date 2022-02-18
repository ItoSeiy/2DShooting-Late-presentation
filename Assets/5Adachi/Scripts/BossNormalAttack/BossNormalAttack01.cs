using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNormalAttack01 : MonoBehaviour
{
    /// <summary>�`���傫���̊T�O������������</summary>
    Rigidbody2D _rb;
    /// <summary>����</summary>
    Vector3 _dir;
    /// <summary>�v���C���[�̃I�u�W�F�N�g</summary>
    private GameObject _player;
    [SerializeField,Header("player��tag")] string _playerTag = null;
    /// <summary>�o���b�g�𔭎˂���|�W�V����</summary>
    [SerializeField, Header("Bullet�𔭎˂���|�W�V����")] Transform[] _muzzles = null;
    /// <summary>�U���p�x</summary>
    [SerializeField, Header("�U���p�x(�b)")] private float _attackInterval = 0.6f;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag(_playerTag);
        StartCoroutine(Attack());
    }

    void Update()
    {
        //�^�[�Q�b�g�i�v���C���[�j�̕������v�Z
        _dir = (_player.transform.position - _muzzles[0].transform.position);
        //�^�[�Q�b�g�i�v���C���[�j�̕����ɉ�]
        _muzzles[0].transform.rotation = Quaternion.FromToRotation(Vector3.up, _dir);
    }

    //Attack�֐��ɓ����ʏ�U��
    IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(_attackInterval);
            //�e�I�u�W�F�N�g�̃}�Y��

            //�e�𔭎ˁi����Bomb�ɂ��Ă܂��j
            var bossEnemyBulletStraight = ObjectPool.Instance.UseBullet(_muzzles[0].position, PoolObjectType.Player01BombChild);
            //�e���}�Y��0�̌����ɍ��킹��
            bossEnemyBulletStraight.transform.rotation = _muzzles[0].rotation;

            //�q�I�u�W�F�N�g�̃}�Y��

            //�e�𔭎ˁi�e�I�u�W�F�N�g�̒e���E���j
            var bossEnemyBulletRight = ObjectPool.Instance.UseBullet(_muzzles[1].position, PoolObjectType.Player01BombChild);
            //�e���}�Y��1�̌����ɍ��킹��
            bossEnemyBulletRight.transform.rotation = _muzzles[1].rotation;
            //�e�𔭎ˁi�e�I�u�W�F�N�g�̒e��荶���j
            var bossEnemyBulletLeft = ObjectPool.Instance.UseBullet(_muzzles[2].position, PoolObjectType.Player01BombChild);
            //�e���}�Y��2�̌����ɍ��킹��
            bossEnemyBulletLeft.transform.rotation = _muzzles[2].rotation;

            
        }
    }
}
