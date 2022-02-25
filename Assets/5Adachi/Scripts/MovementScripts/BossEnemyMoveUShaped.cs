using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveUShaped: MonoBehaviour
{
    /// <summary>�`���傫���̊T�O������������</summary>
    Rigidbody2D _rb;  
    /// <summary>����</summary>
    Vector2 _dir;
    /// <summary>���݂̃p�^�[��</summary>
    int _pattern = 0;
    /// <summary>�X�s�[�h</summary>
    [SerializeField,Header("�X�s�[�h")] float _speed = 5f;    
    /// <summary>�E��</summary>
    [SerializeField, Header("�E��")] float _rightLimit = 7.5f;
    /// <summary>����</summary>
    [SerializeField, Header("����")] float _leftLimit = -7.5f;
    /// <summary>���</summary>
    [SerializeField, Header("���")] float _upperLimit = 3.5f;
    /// <summary>����</summary>
    [SerializeField, Header("����")] float _lowerLimit = -3f;
    /// <summary>�ŒZ�ړ�����</summary>
    [SerializeField, Header("�ŒZ�ړ�����")] float _shortMoveTime = 1f;
    /// <summary>�Œ��ړ�����</summary>
    [SerializeField, Header("�Œ��ړ�����")] float _longMoveTime = 3f;
    /// <summary>��~����</summary>
    [SerializeField,Header("��~����")] float _stopTime = 1f;
    /// <summary>�����ʒu</summary>
    const float MIDDLE_POS = 0;
    /// <summary>����񐔂̐���</summary>
    const float JUDGMENT_TIME = 1/60f;
    /// <summary>�����x�炷</summary>
    const float DELAY_TIME = 1f;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();//�Q�b�g�R���|�[�l���g
        StartCoroutine(UShaped());//�X�^�[�g�R���[�`��
    }

    void Update()
    {
        _rb.velocity = _dir.normalized * _speed;//�����ɃX�s�[�h��������
    }

    /// <summary>
    /// U���^�̂悤�Ȉړ�������
    /// </summary>
    public IEnumerator UShaped()
    {
        _dir = new Vector2(-transform.position.x, 0f);

        //�{�X�̃|�W�V����X��0��������_�������ċl�ނ̂�
        if (transform.position.x == 0f)
        {
            _dir = Vector2.right;//�E�Ɉړ�
        }

        while(true)//�[�ɒ����܂ŉ��ɓ���
        {           
            //���Α��ɒ�������
            if ((transform.position.x <= _leftLimit) || (transform.position.x >= _rightLimit))
            {
                Debug.Log("c");
                _dir = Vector2.zero;//��~
                yield return new WaitForSeconds(_stopTime);//��~����
                _dir = Vector2.down;//������
                break;
            }
            yield return new WaitForSeconds(JUDGMENT_TIME);//����񐔂̐���
        }


        while (true)//���Α��ɒ����܂ňړ�����
        {           
            if(transform.position.y <= _lowerLimit)
            {

                Debug.Log("f");
                _dir = Vector2.zero;//��~
                yield return new WaitForSeconds(_stopTime);//��~����
                _dir = new Vector2(-transform.position.x, 0f);//��ʉ��[�ɂ����獡����ꏊ�̔��Α��ɉ��ړ�
                break;
            }
            yield return new WaitForSeconds(JUDGMENT_TIME);//����񐔂̐���
        }

        yield return new WaitForSeconds(DELAY_TIME);//�����x�炷

        while (true)//���Α��ɒ����܂ŉ��ړ�����
        {          
            //���Α��ɂ������ɍs��
            if (transform.position.x >= _rightLimit || transform.position.x <= _leftLimit)
            {
                Debug.Log("g");
                _dir = Vector2.zero;//��~
                yield return new WaitForSeconds(_stopTime);//��~����
                _dir = Vector2.up;//��ɏオ��
                break;
            }
            yield return new WaitForSeconds(JUDGMENT_TIME);//����񐔂̐���
        }

        while (true)//������x��ɂ����܂ňړ�����
        {
            yield return new WaitForSeconds(JUDGMENT_TIME);

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
        
        yield return new WaitForSeconds(Random.Range(_shortMoveTime, _longMoveTime)); //�ړ�����(�����_��)    
        _dir = Vector2.zero;//��~
    }
}
