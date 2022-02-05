using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveRandom : MonoBehaviour
{
    /// <summary>�`���傫���̊T�O������������</summary>
    Rigidbody2D _rb;
    /// <summary>����</summary>
    Vector2 _dir;
    /// <summary>�����A������</summary>
    private float _horizontal = 0;
    /// <summary>�����A�c����</summary>
    private float _veritical = 0;
    /// <summary>���x</summary>
    float _speed = 4f;
    /// <summary>��~����</summary>
    [SerializeField,Header("��~����")] public float _stopTime = 2;
    /// <summary>���</summary>
    [SerializeField,Header("���")] float _upperLimit = 2.5f;
    /// <summary>����</summary>
    [SerializeField,Header("����")] float _lowerLimit = 1.5f;  
    /// <summary>�E��</summary>
    [SerializeField,Header("�E��")] float _rightLimit = 4f;
    /// <summary>����</summary>
    [SerializeField,Header("����")] float _leftLimit = -4f;
    /// <summary>�ړ�����</summary>
    [SerializeField,Header("�ړ�����")] float _moveTime = 0.5f;

    IEnumerator RandomMovement()
    {       
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            _rb.velocity = new Vector2(0, 0);
            yield return new WaitForSeconds(_stopTime);

            if(transform.position.y > _upperLimit)      //��Ɉړ�����������
            {
                _veritical = Random.Range(-1.0f, -0.1f);
            }
            else if (transform.position.y < _lowerLimit)//���Ɉړ�����������
            {
                _veritical = Random.Range(0.1f, 1.0f);
            }
            else�@�@�@�@�@�@�@�@�@�@�@�@�@�@      //�㉺�ǂ����ɂ��ړ��������ĂȂ��Ȃ�
            {
                _veritical = Random.Range(-1.0f, 1.0f);
            }
            if (transform.position.x > _rightLimit)         //�E�Ɉړ�����������
            {
                _horizontal = (Random.Range(-3.0f, -1.0f));
            }
            else if(transform.position.x < _leftLimit)   //���Ɉړ���������
            {
                _horizontal = (Random.Range(1.0f, 3.0f));
            }
            else�@�@�@�@�@�@�@�@�@�@�@�@         //���E�ǂ����ɂ��ړ��������ĂȂ��Ȃ�
            {
                _horizontal = Random.Range(-3.0f, 3.0f);               
            }

            _dir = new Vector2(_horizontal, _veritical);

            _rb.velocity = _dir * _speed;
            yield return new WaitForSeconds(_moveTime);
            
            Debug.Log(_horizontal);
            Debug.Log(_veritical);
        }
    }
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(RandomMovement());
    }
    private void Update()
    {

    }
}
