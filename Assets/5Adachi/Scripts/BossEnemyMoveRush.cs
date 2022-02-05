using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveRush : MonoBehaviour
{
    Rigidbody2D _rb;
    private GameObject _player;
    [SerializeField] private string _playerTag = null;
    bool _switch = false;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag(_playerTag);
        StartCoroutine(Rush());
    }
    private void Update()
    {
        //transform.position = new Vector2(Mathf.Clamp(transform.position.x, -8f, 8f), Mathf.Clamp(transform.position.y, -4f, 4f));
    }
    IEnumerator Rush()//�v���C���[�̈ʒu�ɂ���č��E�̂ǂ��炩�Ɉړ����邩�����߂�
    {
        Debug.Log("huuuu");
        _rb.velocity = new Vector2(0, 0);
        if (_player.transform.position.x >= transform.position.x)//�v���C���[���E�ɂ�����
        {
            Debug.Log("right");
            _switch = true;
            _rb.velocity = new Vector2(4, 0);
        }
        else                                                     //���ɂ�����
        {
            Debug.Log("left");
            _switch = false;
            _rb.velocity = new Vector2(-4, 0);
        }
      
        while (true) //�v���C���[�̕ӂ�ɒ�������
        {
            yield return new WaitForSeconds(0.1f);
            if (_switch && _player.transform.position.x <= transform.position.x)//�E�Ɉړ������Ƃ���
            {
                _rb.velocity = new Vector2(0, 0);
                break;
            }
            else if (!_switch && _player.transform.position.x >= transform.position.x)//���Ɉړ������Ƃ���
            {
                _rb.velocity = new Vector2(0, 0);
                break;
            }
        }
       
        yield return new WaitForSeconds(0.5f);

        while (true)//���ɍs��
        {
            yield return new WaitForSeconds(0.1f);
            _rb.velocity = new Vector2(0, -4);
            if (transform.position.y <= -3)//���܂ŗ�����
            {
                _rb.velocity = new Vector2(0, 0);
                yield return new WaitForSeconds(1);;
                Debug.Log("Up");
                break;
            }
        }       
        
        _rb.velocity = new Vector2(0, 3);

        while (true)//���̏ꏊ�܂ŏ�ɏオ��
        {
            yield return new WaitForSeconds(0.1f);
            if (3 <= transform.position.y)//���̏ꏊ�܂ł�����
            {
                _rb.velocity = new Vector2(0, 0);
                yield return new WaitForSeconds(2);
                //StartCoroutine(Back());
                break;
            }            
        }
    }
}
