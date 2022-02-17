using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveUShaped: MonoBehaviour
{
    /// <summary>�`���傫���̊T�O������������</summary>
    Rigidbody2D _rb;
    [SerializeField,Header("�X�s�[�h")] float _speed = 5f;    
    /// <summary>�E��</summary>
    [SerializeField, Header("�E��")] float _rightLimit = 7.5f;
    /// <summary>����</summary>
    [SerializeField, Header("����")] float _leftLimit = -7.5f;
    /// <summary>���</summary>
    [SerializeField, Header("���")] float _upperLimit = 3.5f;
    /// <summary>����</summary>
    [SerializeField, Header("����")] float _lowerLimit = -3f;
    /// <summary>�����ʒu</summary>
    const float MIDDLE_POS = 0;
    /// <summary>����</summary>
    Vector2 _dir;
    /// <summary>�ŒZ�ړ�����</summary>
    [SerializeField, Header("�ŒZ�ړ�����")] float _shortMoveTime = 1f;
    /// <summary>�Œ��ړ�����</summary>
    [SerializeField, Header("�Œ��ړ�����")] float _longMoveTime = 3f;
    /// <summary>��~����</summary>
    [SerializeField,Header("��~����")] float _stopTime = 1f;
    /// <summary>����񐔂̐���</summary>
    const float DUDGMENT_TIME = 0.1f;
    /// <summary>���݂̃p�^�[��</summary>
    int _pattern = 0;
    /// <summary>���ɂ��鎞�ɍ��ɂ���p�^�[��</summary>
    const int PATTERN = 1;
    /// <summary>���ɂ��鎞�ɉE�ɂ���p�^�[��</summary>
    const int PATTERN2 = 2;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(UShaped());
    }

    void Update()
    {
        _rb.velocity = _dir.normalized * _speed;
    }

    /// <summary>
    /// U���^�̂悤�Ȉړ�������
    /// </summary>
    public IEnumerator UShaped()
    {
        if (transform.position.x < MIDDLE_POS)//��ʍ������ɂ�����
        {            
            _dir = Vector2.right;//�E�ɓ���
            Debug.Log("a");
        }
        else//��ʉE�����ɂ�����
        {         
            _dir = Vector2.left;//���ɓ���
            Debug.Log("b");
        }

        while(true)//�[�ɒ����܂ŉ��ɓ���
        {
            yield return new WaitForSeconds(DUDGMENT_TIME);
            //���Α��ɒ�������
            if ((transform.position.x <= _leftLimit) || (transform.position.x >= _rightLimit))
            {
                Debug.Log("c");
                _dir = Vector2.zero;//��~
                yield return new WaitForSeconds(_stopTime);//��~����
                _dir = Vector2.down;//������
                break;
            }
        }


        while (true)//���Α��ɒ����܂ňړ�����
        {
            yield return new WaitForSeconds(DUDGMENT_TIME);
            //���Α��ɒ�������
            if (transform.position.x <= _leftLimit && transform.position.y <= _lowerLimit)
            {
                Debug.Log("e");
                _dir = Vector2.zero;//��~
                yield return new WaitForSeconds(_stopTime);//��~����
                _dir = Vector2.right;//��ʉ��[�ɂ�����E�ɍs��
                _pattern = PATTERN;//�p�^�[��1
                break;
            }
            //���Α��ɒ�������
            else if (transform.position.x >= _rightLimit && transform.position.y <= _lowerLimit)
            {
                Debug.Log("f");
                _dir = Vector2.zero;//��~
                yield return new WaitForSeconds(_stopTime);//��~����
                _dir = Vector2.left;//��ʉ��[�ɂ����獶�ɍs��
                _pattern = PATTERN2;//�p�^�[��2
                break;
            }
        }

        while (true)//���Α��ɒ����܂ŉ��ړ�����
        {
            yield return new WaitForSeconds(DUDGMENT_TIME);
            //���Α��ɂ������ɍs��(�p�^�[��1or2�j
            if ((transform.position.x >= _rightLimit && _pattern == PATTERN) || (transform.position.x <= _leftLimit && _pattern == PATTERN2))
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
            yield return new WaitForSeconds(DUDGMENT_TIME);

            if (transform.position.y >= _upperLimit)//������x��ɂ�������
            {
                _dir = Vector2.zero;//��~
                yield return new WaitForSeconds(_stopTime);//��~����
                Debug.Log("h");

                if (transform.position.x < MIDDLE_POS)//���ɂ�����
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
