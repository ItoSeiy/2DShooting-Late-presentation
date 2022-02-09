using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveThunder : MonoBehaviour
{
    /// <summary>�`���傫���̊T�O������������</summary>
    Rigidbody2D _rb;
    /// <summary>�J�E���g</summary>
    int _count = 0;
    /// <summary>�J�E���g�̌��E��</summary>
    int _limitCount = 5;
    /// <summary>�����A������</summary>
    float _horizontal = 1f;
    /// <summary>�����A�c����</summary>
    float _vertical = 1f;
    /// <summary>�X�s�[�h</summary>
    [SerializeField, Header("�X�s�[�h")] float _speed;
    /// <summary>����</summary>
    Vector2 _dir;
    /// <summary>�����ʒu</summary>
    float _middlePos = 0;
    /// <summary>����񐔂̐���</summary>
    float _judgmentLimit = 0.1f;
    /// <summary>��~����</summary>
    [SerializeField, Header("��~����")] float _stopTime = 2f;
    /// <summary>�E��</summary>
    [SerializeField, Header("�E��")] float _rightLimit = 7.5f;
    /// <summary>����</summary>
    [SerializeField, Header("����")] float _leftLimit = -7.5f;
    /// <summary>�t�̓���</summary>
    int _reverseMovement = -1;
    /// <summary>�؂�ւ�</summary>
    int _switch = 0;
    /// <summary>�ŏ��ɍ��ɂ���p�^�[��</summary>
    int _pattern01 = 1;
    /// <summary>�ŏ��ɉE�ɂ���p�^�[��</summary>
    int _pattern02 = 2;
    /// <summary>������</summary>
    int _initialize = 0;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine("Thunder");
    }
    void Update()
    {
        _rb.velocity = _dir.normalized * _speed;
    }

    /// <summary>
    /// �[�Ɉړ����Ă��甽�Α��ɃW�O�U�O�ړ�����
    /// </summary>
    /// <returns></returns>
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

        while (true)
        {
            yield return new WaitForSeconds(_judgmentLimit);

            if (transform.position.x <= _leftLimit)//���ɂ�����
            {
                Debug.Log("a");
                _switch = _pattern01;//�p�^�[��1�ɐ؂�ւ�
                _dir = Vector2.zero;//��~
                yield return new WaitForSeconds(_stopTime);//��~����
                break;
            }
            else if (transform.position.x >= _rightLimit)//�E�ɂ�����
            {
                Debug.Log("a");
                _switch = _pattern02;//�p�^�[��2�ɐ؂�ւ�
                _dir = Vector2.zero;//��~
                yield return new WaitForSeconds(_stopTime);//��~����
                break;
            }
        }

        //������E�ɃW�O�U�O����
        while (true && _switch == _pattern01)
        {
            yield return new WaitForSeconds(_judgmentLimit);
            _count++;//�J�E���g��+1

            if (transform.position.x <= _rightLimit)//�[�ɂ��Ă��Ȃ��Ȃ�J��Ԃ�
            {
                _dir = new Vector2(_horizontal, _vertical);//�E�ɓ���

                if (_count >= _limitCount)//���̃J�E���g�ɂȂ�����
                {
                    _count = _initialize;//�J�E���g��0��
                    _vertical *= _reverseMovement;//�㉺�̓������t�ɂ���                   
                }
            }
            else
            {
                _dir = Vector2.zero;//��~
                break;
            }
        }

        //�E���獶�ɃW�O�U�O����
        while (true && _switch == _pattern02)
        {
            yield return new WaitForSeconds(_judgmentLimit);
            _count++;//�J�E���g��+1
            if (transform.position.x >= _leftLimit)//�[�ɂ��Ă��Ȃ��Ȃ�J��Ԃ�
            {
                _dir = new Vector2(-_horizontal, _vertical);//���ɓ���

                if (_count >= _limitCount)//���̃J�E���g�ɂȂ�����
                {
                    _count = _initialize;//�J�E���g���O��                    
                    _vertical *= _reverseMovement;//�㉺�̓������t�ɂ���   
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
