using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveRush : MonoBehaviour
{
    /// <summary>�`���傫���̊T�O������������</summary>
    Rigidbody2D _rb;
    /// <summary>�v���C���[�̃I�u�W�F�N�g</summary>
    private GameObject _player;
    /// <summary>�v���C���[�̃^�O</summary>
    [SerializeField,Header("�v���C���[�̃^�O")] private string _playerTag = null;
    /// <summary>�X�s�[�h</summary>
    [SerializeField, Header("�X�s�[�h")] float _speed = 5f;
    /// <summary>����</summary>
    [SerializeField, Header("����")] float _lowerLimit = -3f;
    /// <summary>��~����</summary>
    [SerializeField, Header("��~����")] float _stopTime = 2f;
    /// <summary>�؂�ւ�</summary>
    bool _switch = false;
    /// <summary>����</summary>
    Vector2 _dir;
    /// <summary>����񐔂̐���</summary>
    float _judgmentLimit = 0.1f;   
    /// <summary>�C���l</summary>
    float _correctionValue = 0.5f;
    /// <summary>����</summary>
    float _time;
    /// <summary>���Ԃ̌��E</summary>
    [SerializeField,Header("�ǔ�����")] float _timeLimit = 4f;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag(_playerTag);
        StartCoroutine("Rush");
    }

    private void Update()
    {
        _rb.velocity = _dir.normalized * _speed;
        _time += Time.deltaTime;
    }

    /// <summary>
    /// ��莞�ԃv���C���[�����b�N�I���������Ɛ^���ɓːi����
    /// </summary>
    IEnumerator Rush()        
    {
        //�v���C���[�̈ʒu�ɂ���č��E�̂ǂ��炩�Ɉړ����邩�����߂�
        /*//�v���C���[���E�ɂ�����
        if (_player.transform.position.x >= transform.position.x)
        {
            Debug.Log("right");
            _switch = true;//�p�^�[���P�ɐ؂�ւ�
            _dir = Vector2.right;//�E�Ɉړ�
        }
        else//���ɂ�����
        {
            Debug.Log("left");
            _switch = false;//�p�^�[���Q�ɐ؂�ւ�
            _dir = Vector2.left;//���Ɉړ�
        }
      
        while (true) //�v���C���[�̕ӂ�ɒ�������
        {
            yield return new WaitForSeconds(_judgmentLimit);

            //�E�Ɉړ������Ƃ���
            if (_switch && _player.transform.position.x <= transform.position.x)
            {
                _dir = Vector2.zero;//��~
                break;
            }
            //���Ɉړ������Ƃ���
            else if (!_switch && _player.transform.position.x >= transform.position.x)
            {
               _dir = Vector2.zero;//��~
                break;
            }
        }*/

        _time = 0;

        //x���W�����v���C���[�̋߂��Ɉړ�����
        while (true)
        {
            yield return new WaitForSeconds(_judgmentLimit);
            
            //�v���C���[���E�ɂ�����
            if (_player.transform.position.x > transform.position.x +_correctionValue)
            {
                Debug.Log("right");
                _dir = Vector2.right;//�E�Ɉړ�
            }
            //�v���C���[�����ɂ�����
            else if(_player.transform.position.x  < transform.position.x -_correctionValue)
            {
                Debug.Log("left");  
                _dir = Vector2.left;//���Ɉړ�
            }
            else//�v���C���[��x���W���߂�������
            {
                _dir = Vector2.zero;//��~
            }
            //���E�ɒB������
            if ( _time >= _timeLimit)
            {
                break;
            }
        }

        while (true)//�T�K��
        {
            yield return new WaitForSeconds(_judgmentLimit);
            _dir = Vector2.down;//�T�K��

            if (transform.position.y <= _lowerLimit)//�T�K������
            {
                _dir = Vector2.zero;//��~
                yield return new WaitForSeconds(_stopTime);//��~����
                _dir = Vector2.up;//�オ��
                break;
            }
        }       
        
        while (true)//���̏ꏊ�܂ŏオ��
        {
            yield return new WaitForSeconds(_judgmentLimit);
            
            if (3 <= transform.position.y)//���̏ꏊ�܂ł�����
            {
                Debug.Log("���X�g");
                _dir = Vector2.zero;//��~
                yield return new WaitForSeconds(_stopTime);//��~����
                break;
            }            
        }
    }
}
