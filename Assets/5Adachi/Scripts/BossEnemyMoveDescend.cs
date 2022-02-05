using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveDescend : MonoBehaviour
{
    /// <summary>�`���傫���̊T�O������������</summary>
    Rigidbody2D _rb;
    [SerializeField] float _speed = 5f;
    
    /// <summary>���̌��E</summary>
    [SerializeField] float _horizontalLimit = 7.5f;
    /// <summary>���</summary>
    [SerializeField] float _upperLimit = 3.5f;
    /// <summary>����</summary>
    [SerializeField] float _lowerLimit = -3;

    Vector2 _direction;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Descend());
    }

    void Update()
    {
        _rb.velocity = _direction * _speed;
    }

    public IEnumerator Descend()
    {
        if (transform.position.x < 0)
        {
            //��ʂ�荶�����ɂ�����E�ɓ���
            _direction = Vector2.right;
            Debug.Log("a");
        }
        else                         //�E�ɂ�����
        {
            //��ʂ��E�����ɂ�����E�ɓ���
            _direction = Vector2.left;
            Debug.Log("b");
        }

        

        while(true)//�[�ɒ����܂ŉ�����
        {
            yield return new WaitForSeconds(0.1f);
            if (transform.position.x <= -_horizontalLimit)
            {
                Debug.Log("c");
                //��ʍ��[�ɂ����牺����
                _direction = Vector2.down;
                break;
            }
            else if (transform.position.x >= _horizontalLimit)
            {
                Debug.Log("d");
                //��ʉE�[�ɂ����牺����
                _direction = Vector2.down;
                break;
            }
        }


        while (true)//���Α��ɒ����܂ňړ�����
        {
            yield return new WaitForSeconds(0.1f);

            if (transform.position.y <= _lowerLimit && transform.position.x <= -_horizontalLimit)
            {
                Debug.Log("e");
                //��ʉ��[�ɂ�����E�ɍs��
                _direction = Vector2.right;
                break;
            }
            else if (transform.position.y <= _lowerLimit && transform.position.x >= _horizontalLimit)
            {
                Debug.Log("f");
                //��ʉ��[�ɂ����獶�ɍs��
                _direction = Vector2.left;
                break;
            }
        }

        yield return new WaitForSeconds(2);

        while (true)//��ɏオ��
        {
            Debug.Log("g");
            yield return new WaitForSeconds(0.1f);
            _direction = Vector2.up;

            if (transform.position.y >= _upperLimit)
            {
                Debug.Log("h");
                //��[�ɂ�����~�܂�
                break;
            }
        }

        while(true)//���������̈ʒu��
        {
            if (transform.position.x < 0)
            {
                //���ɂ�����E�ɍs��
                _direction = Vector2.right;
                Debug.Log("a");
                break;
            }
            else
            {
                //�E�ɂ����獶�ɍs��
                _direction = Vector2.left;
                Debug.Log("b");
                break;
            }
        }
        yield return new WaitForSeconds(Random.Range(1, 3));
        //�~�܂�
        _direction = Vector2.zero;
    }
}
