using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveUShaped: MonoBehaviour
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
    /// <summary>�����ʒu</summary>
    float _middlePosition = 0;
    /// <summary>����</summary>
    Vector2 _direction;
    /// <summary>�؂�ւ�</summary>
    bool _switch = false;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(UShaped());
    }

    void Update()
    {
        _rb.velocity = _direction * _speed;
    }
    public IEnumerator UShaped()
    {
        if (transform.position.x < _middlePosition)
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
                _switch = true;
                break;
            }
            else if (transform.position.y <= _lowerLimit && transform.position.x >= _horizontalLimit)
            {
                Debug.Log("f");
                //��ʉ��[�ɂ����獶�ɍs��
                _direction = Vector2.left;
                _switch = false;
                break;
            }
        }


        while (true)//��ɏオ��
        {
            yield return new WaitForSeconds(0.1f);
            if ((transform.position.x >= _horizontalLimit && _switch) || (transform.position.x >= -_horizontalLimit && !_switch))
            {
                Debug.Log("g");
                _direction = Vector2.up;
                break;
            }
        }

        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (transform.position.y >= _upperLimit)
            {
                Debug.Log("h");
                if (transform.position.x < _middlePosition)
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
        }

        yield return new WaitForSeconds(Random.Range(1, 3));
        //�~�܂�
        _direction = Vector2.zero;
    }
}
