using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveRandom : MonoBehaviour
{
    /// <summary>�`���傫���̊T�O������������</summary>
    Rigidbody2D _rb;
    
    /// <summary>�����A������</summary>
    private float _horizontal = 0f;
    /// <summary>�����A�c����</summary>
    private float _veritical = 0f;
    /// <summary>���x</summary>
    float _speed = 4f;
    /// <summary>��~����</summary>
    [SerializeField,Header("��~����")] float _stopTime = 2f;
    /// <summary>�E��</summary>
    [SerializeField,Header("�E��")] float _rightLimit = 4f;
    /// <summary>����</summary>
    [SerializeField,Header("����")] float _leftLimit = -4f;
    /// <summary>���</summary>
    [SerializeField,Header("���")] float _upperLimit = 2.5f;
    /// <summary>����</summary>
    [SerializeField,Header("����")] float _lowerLimit = 1.5f;     
    /// <summary>�ړ�����</summary>
    [SerializeField,Header("�ړ�����")] float _moveTime = 0.5f;
    /// <summary>����</summary>
    Vector2 _dir;
    /// <summary>������</summary>
    float _leftDir = -1f;
    /// <summary>�E����</summary>
    float _rightDir = 1f;
    /// <summary>�����</summary>
    float _upDir = 1f;
    /// <summary>������</summary>
    float _downDir = -1;
    /// <summary>�����Ȃ�</summary>
    float _noDir = 0f;


    /// <summary>
    /// �����_�������ɓ���
    /// </summary>
    IEnumerator RandomMovement()
    {       
        while (true)
        {
            //��莞�Ԏ~�܂�
            _rb.velocity = Vector2.zero;
            yield return new WaitForSeconds(_stopTime);

            //�ꏊ�ɂ���Ĉړ��ł��鍶�E�����𐧌�����
            if (transform.position.x > _rightLimit)         //�E�Ɉړ�����������
            {
                _horizontal = Random.Range(_leftDir, _noDir);//���ړ��\
            }
            else if(transform.position.x < _leftLimit)   //���Ɉړ���������
            {
                _horizontal = Random.Range(_noDir, _rightDir);//�E�ړ��\
            }
            else�@�@�@�@�@�@�@�@�@�@�@�@         //���E�ǂ����ɂ��ړ��������ĂȂ��Ȃ�
            {
                _horizontal = Random.Range(_leftDir, _rightDir);//���R�ɍ��E�ړ��\          
            }

            //�ꏊ�ɂ���Ĉړ��ł���㉺�����𐧌�����
            if(transform.position.y > _upperLimit)      //��Ɉړ�����������
            {
                _veritical = Random.Range(_downDir, _noDir);//���ړ��\
            }
            else if (transform.position.y < _lowerLimit)//���Ɉړ�����������
            {
                _veritical = Random.Range(_noDir, _upDir);//��ړ��\
            }
            else�@�@�@�@�@�@�@�@�@�@�@�@�@�@      //�㉺�ǂ����ɂ��ړ��������ĂȂ��Ȃ�
            {
                _veritical = Random.Range(_downDir, _upDir);//���R�ɏ㉺�ړ��\
            }

            _dir = new Vector2(_horizontal, _veritical);
            //��莞�Ԉړ�����
            _rb.velocity = _dir.normalized * _speed;
            yield return new WaitForSeconds(_moveTime);
            
            Debug.Log("x" + _horizontal);
            Debug.Log("y" + _veritical);
        }
    }
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine("RandomMovement");
        
    }
    private void Update()
    {
        //_rb.velocity = _dir * _speed;
    }
}
