using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveThunder : MonoBehaviour
{
    /// <summary>�`���傫���̊T�O������������</summary>
    Rigidbody2D _rb;   
    /// <summary>�����A������</summary>
    const float HORIZONTAL = 1f;
    /// <summary>�����A�c����</summary>
    float _vertical = 1f;
    /// <summary>�X�s�[�h</summary>
    [SerializeField, Header("�X�s�[�h")] float _speed;
    /// <summary>����</summary>
    Vector2 _dir;
    /// <summary>�����ʒu</summary>
    float _middlePos = 0;
    /// <summary>����̍ۂɑ҂��Ăق�������</summary>
    float _judgmentTime = 0.1f;
    /// <summary>��~����</summary>
    [SerializeField, Header("��~����")] float _stopTime = 2f;
    /// <summary>�E��</summary>
    [SerializeField, Header("�E��")] float _rightLimit = 7.5f;
    /// <summary>����</summary>
    [SerializeField, Header("����")] float _leftLimit = -7.5f;
    /// <summary>�t�̓���</summary>
    const float REVERSE_MOVEMENT = -1f;
    /// <summary>���݂̃p�^�[��</summary>
    int _pattern = 0;
    /// <summary>�ŏ��ɍ��ɂ���p�^�[��</summary>
    const  int PATTERN1 = 1;
    /// <summary>�ŏ��ɉE�ɂ���p�^�[��</summary>
    const int PATTERN2 = 2;
    /// <summary>�^�C�}�[�̃��Z�b�g�p</summary>
    const float TIMER_RESET = 0f;
    /// <summary>����</summary>
    float _timer = 0f;
    /// <summary>���Ԑ���,�㉺�ړ����t�ɂ��鎞��<summary>
    [SerializeField,Header("�㉺�ړ����t�ɂ��鎞��")] float _timeLimit = 0.5f;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Thunder());
    }
    void Update()
    {
        _rb.velocity = _dir.normalized * _speed;//���̕����Ɉړ�
        _timer += Time.deltaTime;//����
    }

    /// <summary>
    /// �[�Ɉړ����Ă��甽�Α��ɃW�O�U�O�ړ�����
    /// </summary>
    IEnumerator Thunder()
    {
        if (transform.position.x >= _middlePos)//��ʉE���ɂ�����
        {
            Debug.Log("left");
            _dir = Vector2.left;//���Ɉړ�
        }
        else//�����ɂ�����
        {
            Debug.Log("right");
            _dir = Vector2.right;//�E�Ɉړ�
        }

        //�[�ɂ������~
        while (true)
        {
            yield return new WaitForSeconds(_judgmentTime);//����񐔂̐���

            if (transform.position.x <= _leftLimit)//���ɂ�����
            {
                Debug.Log("a");
                _pattern = PATTERN1;//�p�^�[��1�ɐ؂�ւ�
                _dir = Vector2.zero;//��~
                yield return new WaitForSeconds(_stopTime);//��~����
                break;
            }
            else if (transform.position.x >= _rightLimit)//�E�ɂ�����
            {
                Debug.Log("a");
                _pattern = PATTERN2;//�p�^�[��2�ɐ؂�ւ�
                _dir = Vector2.zero;//��~
                yield return new WaitForSeconds(_stopTime);//��~����
                break;
            }
        }

        _timer = TIMER_RESET;//�^�C�������Z�b�g

        //������E�ɃW�O�U�O����
        while (true && _pattern == PATTERN1)
        {
            Debug.Log("1");
            yield return new WaitForSeconds(_judgmentTime);//����񐔂̐���

            if (transform.position.x <= _rightLimit)//�[�ɂ��Ă��Ȃ��Ȃ�J��Ԃ�
            {
                _dir = new Vector2(HORIZONTAL, _vertical);//�E��or�E���ɓ����Ȃ���

                if (_timer >= _timeLimit)//�������ԂɂȂ�����
                {
                    _timer = TIMER_RESET;//�^�C�������Z�b�g
                    _vertical *= REVERSE_MOVEMENT;//�㉺�̓������t�ɂ���                   
                }
                else if (transform.position.y >= 4)
                {
                    _timer = TIMER_RESET;//�^�C�������Z�b�g
                    _vertical *= REVERSE_MOVEMENT;//�㉺�̓������t�ɂ���   
                }
                else if (transform.position.y <= -4)
                {
                    _timer = TIMER_RESET;//�^�C�������Z�b�g
                    _vertical *= REVERSE_MOVEMENT;//�㉺�̓������t�ɂ���   
                }
            }
            else
            {
                _dir = Vector2.zero;//��~
                break;
            }
        }

        //�E���獶�ɃW�O�U�O����
        while (true && _pattern == PATTERN2)
        {
            Debug.Log("2");
            yield return new WaitForSeconds(_judgmentTime);//����񐔂̐���

            if (transform.position.x >= _leftLimit)//�[�ɂ��Ă��Ȃ��Ȃ�J��Ԃ�
            {
                _dir = new Vector2(-HORIZONTAL, _vertical);//����or�����ɓ����Ȃ���

                if (_timer >= _timeLimit)//�������ԂɂȂ�����
                {
                    _timer = TIMER_RESET;//�^�C�������Z�b�g
                    _vertical *= REVERSE_MOVEMENT;//�㉺�̓������t�ɂ���   
                }
                else if (transform.position.y >= 4)
                {
                    _timer = TIMER_RESET;//�^�C�������Z�b�g
                    _vertical *= REVERSE_MOVEMENT;//�㉺�̓������t�ɂ���   
                }
                else if (transform.position.y <= -4)
                {
                    _timer = TIMER_RESET;//�^�C�������Z�b�g
                    _vertical *= REVERSE_MOVEMENT;//�㉺�̓������t�ɂ���   
                }
            }
            else//�[�ɂ�����
            {
                _dir = Vector2.zero;//��~
                break;
            }
        }
    }
}
