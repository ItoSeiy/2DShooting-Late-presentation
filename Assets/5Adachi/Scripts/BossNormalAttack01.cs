using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNormalAttack01 : MonoBehaviour
{
    /// <summary>�`���傫���̊T�O������������</summary>
    Rigidbody2D _rb;
    /// <summary>�v���C���[�̃I�u�W�F�N�g</summary>
    private GameObject _player;
    [SerializeField,Header("player��tag")] string _playerTag = null;
    /// <summary>�o���b�g�𔭎˂���|�W�V����</summary>
    [SerializeField, Header("Bullet�𔭎˂���|�W�V����")] Transform[] _muzzles = null;
    /// <summary>���x</summary>
    [SerializeField, Header("�X�s�[�h")] float _speed = 4f;
    Vector3 _rotation;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag(_playerTag);
    }

    void Update()
    {
        _rotation = _player.transform.position - _muzzles[0].transform.position;

        _rotation.y = 0f;

        Quaternion quaternion = Quaternion.LookRotation(_rotation);
        // �Z�o������]�l�����̃Q�[���I�u�W�F�N�g��rotation�ɑ��
        _muzzles[0].transform.rotation = quaternion;
    }
}
