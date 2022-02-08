using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveUShaped: MonoBehaviour
{
    /// <summary>�`���傫���̊T�O������������</summary>
    Rigidbody2D _rb;
    [SerializeField,Header("�X�s�[�h")] float _speed = 5f;    
    /// <summary>����</summary>
    [SerializeField,Header("����")] float _horizontalLimit = 7.5f;
    /// <summary>���</summary>
    [SerializeField, Header("���")] float _upperLimit = 3.5f;
    /// <summary>����</summary>
    [SerializeField, Header("����")] float _lowerLimit = -3;
    /// <summary>�����ʒu</summary>
    float _middlePos = 0;
    /// <summary>����</summary>
    Vector2 _dir;
    /// <summary>�ŒZ�ړ�����</summary>
    float _shortMoveTime = 1f;
    /// <summary>�Œ��ړ�����</summary>
    float _longMoveTime = 3f;
    /// <summary>��~����</summary>
    [SerializeField,Header("��~����")] float _stopTime = 1f;
    /// <summary>����񐔂̐���</summary>
    float _judgmentLimit = 0.1f;
    /// <summary>�؂�ւ�</summary>
    int _switch = 0;
    /// <summary>���ɂ��鎞�ɍ��ɂ���p�^�[��</summary>
    int _pattern01 = 1;
    /// <summary>���ɂ��鎞�ɉE�ɂ���p�^�[��</summary>
    int _pattern02 = 2;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(UShaped());
    }

    void Update()
    {
        _rb.velocity = _dir.normalized * _speed;
    }
    public IEnumerator UShaped()
    {
        if (transform.position.x < _middlePos)//��ʂ�荶�����ɂ�����
        {            
            _dir = Vector2.right;//�E�ɓ���
            Debug.Log("a");
        }
        else//��ʂ��E�����ɂ�����
        {         
            _dir = Vector2.left;//���ɓ���
            Debug.Log("b");
        }

        while(true)//�[�ɒ����܂ŉ�����
        {
            yield return new WaitForSeconds(_judgmentLimit);
            //���Α��ɒ�������
            if (transform.position.x <= -_horizontalLimit || transform.position.x >= _horizontalLimit)
            {
                Debug.Log("c");
                _dir = Vector2.zero;//��~
                yield return new WaitForSeconds(_stopTime);//��~����
                _dir = Vector2.down;//��ʍ��[�ɂ����牺����
                break;
            }
        }


        while (true)//���Α��ɒ����܂ňړ�����
        {
            yield return new WaitForSeconds(_judgmentLimit);
            //���Α��ɒ�������
            if (transform.position.x <= -_horizontalLimit && transform.position.y <= _lowerLimit)
            {
                Debug.Log("e");
                _dir = Vector2.zero;//��~
                yield return new WaitForSeconds(_stopTime);//��~����
                _dir = Vector2.right;//��ʉ��[�ɂ�����E�ɍs��
                _switch = _pattern01;//�p�^�[��1
                break;
            }
            //���Α��ɒ�������
            else if (transform.position.x >= _horizontalLimit�@&& transform.position.y <= _lowerLimit)
            {
                Debug.Log("f");
                _dir = Vector2.zero;//��~
                yield return new WaitForSeconds(_stopTime);//��~����
                _dir = Vector2.left;//��ʉ��[�ɂ����獶�ɍs��
                _switch = _pattern02;//�p�^�[��2
                break;
            }
        }

        while (true)//���Α��ɒ����܂ŉ��ړ�����
        {
            yield return new WaitForSeconds(_judgmentLimit);
            //���Α��ɂ������ɍs��(�p�^�[��1or2�j
            if ((transform.position.x >= _horizontalLimit && _switch == _pattern01) || (transform.position.x <= -_horizontalLimit && _switch == _pattern02))
            {
                Debug.Log("g");
                _dir = Vector2.zero;//��~
                yield return new WaitForSeconds(_stopTime);//��~����
                _dir = Vector2.up;//��ɏオ��
                break;
            }
        }

        while (true)//������x��ɂ����܂ňړ�����
        {
            yield return new WaitForSeconds(_judgmentLimit);
            if (transform.position.y >= _upperLimit)//������x��ɂ�������
            {
                _dir = Vector2.zero;//��~
                yield return new WaitForSeconds(_stopTime);//��~����
                Debug.Log("h");

                if (transform.position.x < _middlePos)//���ɂ�����
                {                   
                    _dir = Vector2.right;//�E�ɍs��
                    Debug.Log("a");
                    break;
                }
                else//�E�ɂ�����
                {    
                    _dir = Vector2.left;//���ɍs��
                    Debug.Log("b");
                    break;
                }
            }
        }
        //�ړ�����
        yield return new WaitForSeconds(Random.Range(_shortMoveTime, _longMoveTime));      
        _dir = Vector2.zero;//��~
    }
}
