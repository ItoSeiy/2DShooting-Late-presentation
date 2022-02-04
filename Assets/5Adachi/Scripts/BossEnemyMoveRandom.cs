using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveRandom : MonoBehaviour
{
    bool _isMove = false;
    private float x = 0;
    private float y = 0;
    float _speed = 4f;
    [SerializeField] public float _stopTime = 2;
    Rigidbody2D _rb;
    Vector2 _dir;

    IEnumerator StationA_Move()
    {       
        yield return new WaitForSeconds(0.5f);
        _isMove = true;
        while (true)
        {
            _rb.velocity = new Vector2(0, 0);
            yield return new WaitForSeconds(_stopTime);

            if(transform.position.y > 2.5f)      //��Ɉړ�����������
            {
                y = Random.Range(-1.0f, -0.1f);
            }
            else if (transform.position.y < 1.5f)//���Ɉړ�����������
            {
                y = Random.Range(0.1f, 1.0f);
            }
            else�@�@�@�@�@�@�@�@�@�@�@�@�@�@      //�㉺�ǂ����ɂ��ړ��������ĂȂ��Ȃ�
            {
                y = Random.Range(-1.0f, 1.0f);
            }
            if (transform.position.x > 4)         //�E�Ɉړ�����������
            {
                x = (Random.Range(-3.0f, -1.0f));
            }
            else if(transform.position.x < -4)   //���Ɉړ���������
            {
                x = (Random.Range(1.0f, 3.0f));
            }
            else�@�@�@�@�@�@�@�@�@�@�@�@         //���E�ǂ����ɂ��ړ��������ĂȂ��Ȃ�
            {
                x = Random.Range(-3.0f, 3.0f);               
            }

            _dir = new Vector2(x, y);

            _rb.velocity = _dir * _speed;
            yield return new WaitForSeconds(0.5f);
            
            Debug.Log(x);
            Debug.Log(y);
        }
    }
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(StationA_Move());
    }
    private void Update()
    {

    }
}
