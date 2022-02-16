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
    /// <summary>���</summary>
    [SerializeField, Header("���")] float _upperLimit = 3f;
    /// <summary>����</summary>
    [SerializeField, Header("����")] float _lowerLimit = -3f;
    /// <summary>��~����</summary>
    [SerializeField, Header("�~�肽��̒�~����")] float _stopTime = 2f;
    /// <summary>����</summary>
    Vector2 _dir;
    /// <summary>����񐔂̐���</summary>
    const float JUDGMENT_TIME = 0.1f;
    /// <summary>�C���l</summary>
    const float PLAYER_POSTION_OFFSET = 0.5f;
    /// <summary>����</summary>
    float _time = 0f;
    /// <summary>��ɑ؍݂��鎞�ԁA�ǔ�����</summary>
    [SerializeField,Header("�ǔ�����(��ɑ؍݂��Ă��鎞��)")] float _stayingTime = 4f;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag(_playerTag);
        StartCoroutine(Rush());
    }

    private void Update()
    {
        _rb.velocity = _dir.normalized * _speed;
        _time += Time.deltaTime;
    }

    /// <summary>
    /// ��莞�ԃv���C���[�����b�N�I���������Ɛ^���ɃT�K��B���̌��ɏオ��B
    /// </summary>
    IEnumerator Rush()        
    {
        _time = 0;

        //x���W�����v���C���[�̋߂��Ɉړ�����
        while (true)
        {
            yield return new WaitForSeconds(JUDGMENT_TIME);
            
            //�v���C���[���E�ɂ�����
            if (_player.transform.position.x > transform.position.x + PLAYER_POSTION_OFFSET)
            {
                Debug.Log("right");
                _dir = Vector2.right;//�E�Ɉړ�
            }
            //�v���C���[�����ɂ�����
            else if(_player.transform.position.x  < transform.position.x - PLAYER_POSTION_OFFSET)
            {
                Debug.Log("left");  
                _dir = Vector2.left;//���Ɉړ�
            }
            else//�v���C���[��x���W���߂�������
            {
                _dir = Vector2.zero;//��~
            }
            //���E�ɒB������
            if ( _time >= _stayingTime)
            {
                break;
            }
        }

        while (true)//�T�K��
        {
            yield return new WaitForSeconds(JUDGMENT_TIME);
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
            yield return new WaitForSeconds(JUDGMENT_TIME);
            
            if (_upperLimit <= transform.position.y)//���̏ꏊ�܂ł�����
            {
                Debug.Log("���X�g");
                _dir = Vector2.zero;//��~
                yield return new WaitForSeconds(_stopTime);//��~����
                break;
            }            
        }
    }
}
