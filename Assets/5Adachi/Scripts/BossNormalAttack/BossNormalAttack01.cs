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
    /// <summary>�v���C���[�̃^�O</summary>
    [SerializeField,Header("player��tag")] string _playerTag = null;
    /// <summary>�o���b�g�𔭎˂���|�W�V����</summary>
    [SerializeField, Header("Bullet�𔭎˂���|�W�V����")] Transform[] _muzzles = null;
    /// <summary>�U���p�x</summary>
    [SerializeField, Header("�U���p�x(�b)")] private float _attackInterval = 0.6f;
    /// <summary>���˂���e��ݒ�ł���</summary>
    [SerializeField, Header("���˂���e�̐ݒ�")] PoolObjectType _bullet;

    int _count = 0;

    /// <summary>�����A������</summary>
    private float _horizontal = 0f;
    /// <summary>�����A�c����</summary>
    private float _veritical = 0f;
    /// <summary>���x</summary>
    [SerializeField, Header("�X�s�[�h")] float _speed = 4f;
    /// <summary>��~����</summary>
    [SerializeField, Header("��~����")] float _stopTime = 2f;
    /// <summary>�ړ�����</summary>
    [SerializeField, Header("�ړ�����")] float _moveTime = 0.5f;
    /// <summary>�E��</summary>
    [SerializeField, Header("�E��")] float _rightLimit = 4f;
    /// <summary>����</summary>
    [SerializeField, Header("����")] float _leftLimit = -4f;
    /// <summary>���</summary>
    [SerializeField, Header("���")] float _upperLimit = 2.5f;
    /// <summary>����</summary>
    [SerializeField, Header("����")] float _lowerLimit = 1.5f;
    /// <summary>������</summary>
    const float LEFT_DIR = -1f;
    /// <summary>�E����</summary>
    const float RIGHT_DIR = 1f;
    /// <summary>�����</summary>
    const float UP_DIR = 1f;
    /// <summary>������</summary>
    const float DOWN_DIR = -1;
    /// <summary>�����Ȃ�</summary>
    const float NO_DIR = 0f;


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
            
            //�e���}�Y��0�̌����ɍ��킹�Ēe�𔭎�
            ObjectPool.Instance.UseBullet(_muzzles[0].position, _bullet).transform.rotation = _muzzles[0].rotation;

            //�q�I�u�W�F�N�g�̃}�Y��

            //�e���}�Y��1�̌����ɍ��킹�Ēe�𔭎ˁi�e�I�u�W�F�N�g�̒e���E���j
            ObjectPool.Instance.UseBullet(_muzzles[1].position, _bullet).transform.rotation= _muzzles[1].rotation;
            
            //�e���}�Y��2�̌����ɍ��킹�Ēe�𔭎ˁi�e�I�u�W�F�N�g�̒e��荶���j
            ObjectPool.Instance.UseBullet(_muzzles[2].position, _bullet).transform.rotation = _muzzles[2].rotation;

            //�e���}�Y��3�̌����ɍ��킹�Ēe�𔭎ˁi�e�I�u�W�F�N�g�̒e���E���j
            ObjectPool.Instance.UseBullet(_muzzles[3].position, _bullet).transform.rotation = _muzzles[3].rotation;

            //�e���}�Y��4�̌����ɍ��킹�Ēe�𔭎ˁi�e�I�u�W�F�N�g�̒e��荶���j
            ObjectPool.Instance.UseBullet(_muzzles[4].position, _bullet).transform.rotation = _muzzles[4].rotation;

            //��莞�Ԏ~�܂�
            _rb.velocity = Vector2.zero;
            yield return new WaitForSeconds(_stopTime);


            if (_count % 2 == 0)
            {
                //�ꏊ�ɂ���Ĉړ��ł��鍶�E�����𐧌�����
                if (transform.position.x > _rightLimit)         //�E�Ɉړ�����������
                {
                    _horizontal = Random.Range(LEFT_DIR, NO_DIR);//���ړ��\
                }
                else if (transform.position.x < _leftLimit)   //���Ɉړ���������
                {
                    _horizontal = Random.Range(NO_DIR, RIGHT_DIR);//�E�ړ��\
                }
                else                     //���E�ǂ����ɂ��ړ��������ĂȂ��Ȃ�
                {
                    _horizontal = Random.Range(LEFT_DIR, RIGHT_DIR);//���R�ɍ��E�ړ��\          
                }

                //�ꏊ�ɂ���Ĉړ��ł���㉺�����𐧌�����
                if (transform.position.y > _upperLimit)      //��Ɉړ�����������
                {
                    _veritical = Random.Range(DOWN_DIR, NO_DIR);//���ړ��\
                }
                else if (transform.position.y < _lowerLimit)//���Ɉړ�����������
                {
                    _veritical = Random.Range(NO_DIR, UP_DIR);//��ړ��\
                }
                else                    //�㉺�ǂ����ɂ��ړ��������ĂȂ��Ȃ�
                {
                    _veritical = Random.Range(DOWN_DIR, UP_DIR);//���R�ɏ㉺�ړ��\
                }

                _dir = new Vector2(_horizontal, _veritical);//�����_���Ɉړ�
                                                            //��莞�Ԉړ�����
                _rb.velocity = _dir.normalized * _speed;
                yield return new WaitForSeconds(_moveTime);
            }

            _count++;
        }
    }
}
