using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveThunder : MonoBehaviour
{
    /// <summary>�`���傫���̊T�O������������</summary>
    Rigidbody2D _rb;
    /// <summary>����</summary>
    Vector2 _dir;
    /// <summary>�����A�c����</summary>
    float _vertical = 1f;
    /// <summary>�^�C�}�[</summary>
    float _timer = 0f;    
    /// <summary>�����ʒu</summary>
    float _middlePos = 0;
    /// <summary>���݂̃p�^�[��</summary>
    int _pattern = 0;
    /// <summary>����ʒu�ɋO���C������</summary>
    bool _fix = false;   
    /// <summary>�X�s�[�h</summary>
    [SerializeField, Header("�X�s�[�h")] float _speed;
    /// <summary>��~����</summary>
    [SerializeField, Header("��~����")] float _stopTime = 2f;
    /// <summary>�E��</summary>
    [SerializeField, Header("�E��")] float _rightLimit = 7.5f;
    /// <summary>����</summary>
    [SerializeField, Header("����")] float _leftLimit = -7.5f;
    /// <summary>���</summary>
    [SerializeField, Header("���")] float _upperLimit = 4f;
    /// <summary>����</summary>
    [SerializeField, Header("����")] float _lowerLimit = -4f;
    /// <summary>���Ԑ���,�㉺�ړ����t�ɂ��鎞��<summary>
    [SerializeField, Header("�㉺�ړ����t�ɂ��鎞��")] float _timeLimit = 0.5f;
    /// <summary>�ŏ��ɍ��ɂ���p�^�[��</summary>
    const int PATTERN1 = 1;
    /// <summary>�ŏ��ɉE�ɂ���p�^�[��</summary>
    const int PATTERN2 = 2;
    /// <summary>�����A������</summary>
    const float HORIZONTAL = 1f;
    /// <summary>�t�̓���</summary>
    const float REVERSE_MOVEMENT = -1f;
    /// <summary>����̍ۂɑ҂��Ăق�������</summary>
    const float JUDGMENT_TIME = 1/60;      
    /// <summary>�^�C�}�[�̃��Z�b�g�p</summary>
    const float TIMER_RESET = 0f;      
    /// <summary>��ɏオ��</summary>
    const float MOVEUP = 1f;
    /// <summary>���ɃT�K��</summary>
    const float MOVEDOWN = -1f;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();//�Q�b�g�R���|�[�l���g
        _fix = true;//���̂悤�ȋO�����C���ł���悤�ɂ���
        StartCoroutine(Thunder());//�X�^�[�g�R���[�`��
        
    }
    void Update()
    {
        _rb.velocity = _dir.normalized * _speed;//���̕����Ɉړ�
        _timer += Time.deltaTime;//����
    }

    /// <summary>
    /// �[�Ɉ꒼���Ɉړ�������A���Α��ɒ����܂ŃW�O�U�O�ړ�����
    /// </summary>
    IEnumerator Thunder()
    {
        _dir = new Vector2(-transform.position.x, 0f);

        //�{�X�̃|�W�V����X��0��������_�������ċl�ނ̂�
        if (transform.position.x == 0f)
        {
            _dir = Vector2.right;//�E�Ɉړ�
        }

        //�[�ɂ������~
        while (true)
        {
            yield return new WaitForSeconds(JUDGMENT_TIME);//����񐔂̐���

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

        //�W�O�U�N���铮��

        //������E�ɃW�O�U�O����
        while (true && _pattern == PATTERN1)
        {
            Debug.Log("1");
            yield return new WaitForSeconds(JUDGMENT_TIME);//����񐔂̐���

            if (transform.position.x <= _rightLimit)//�[�ɂ��Ă��Ȃ��Ȃ�J��Ԃ�
            {
                _dir = new Vector2(HORIZONTAL, _vertical);//�E��or�E���ɓ����Ȃ���

                if (_timer >= _timeLimit)//�������ԂɂȂ�����
                {
                    _timer = TIMER_RESET;//�^�C�������Z�b�g
                    _vertical *= REVERSE_MOVEMENT;//�㉺�̓������t�ɂ���                   
                }

                //��ʊO�ɍs�������ɂȂ�����P�x�����O���C������
                else if (transform.position.y >= _upperLimit && _fix)
                {
                    _vertical = MOVEDOWN;//���ɃT�K�铮���ɂ���   
                    _timer = TIMER_RESET;//�^�C�������Z�b�g
                    _fix = false;//�g���Ȃ��悤�ɂ���
                    Debug.Log("3");
                }
                else if (transform.position.y <= _lowerLimit && _fix)
                {
                    _vertical = MOVEUP;//��̓����ɂ���   
                    _timer = TIMER_RESET;//�^�C�������Z�b�g
                    _fix = false;//�g���Ȃ��悤�ɂ���
                    Debug.Log("4");
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
            yield return new WaitForSeconds(JUDGMENT_TIME);//����񐔂̐���

            if (transform.position.x >= _leftLimit)//�[�ɂ��Ă��Ȃ��Ȃ�J��Ԃ�
            {
                _dir = new Vector2(-HORIZONTAL, _vertical);//����or�����ɓ����Ȃ���

                if (_timer >= _timeLimit)//�������ԂɂȂ�����
                {
                    Debug.Log("4");
                    _timer = TIMER_RESET;//�^�C�������Z�b�g
                    _vertical *= REVERSE_MOVEMENT;//�㉺�̓������t�ɂ���   
                }

                //��ʊO�ɍs�������ɂȂ�����P�x�����O���C������
                else if (transform.position.y >= _upperLimit && _fix)
                {
                    _vertical = MOVEDOWN;//���ɃT�K�铮���ɂ���   
                    _timer = TIMER_RESET;//�^�C�������Z�b�g
                    _fix = false;//�g���Ȃ��悤�ɂ���
                    Debug.Log("3");
                }
                else if (transform.position.y <= _lowerLimit && _fix)
                {
                    _vertical = MOVEUP;//��ɏオ�铮���ɂ���   
                    _timer = TIMER_RESET;//�^�C�������Z�b�g
                    _fix = false;//�g���Ȃ��悤�ɂ���
                    Debug.Log("3");
                }
            }
            else//�[�ɂ�����
            {
                _dir = Vector2.zero;//��~                
                break;
            }
        }

        _fix = true;//�g����悤�ɂ���
        yield break;
    }
}
