using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMovement03 : EnemyBese
{
    [SerializeField, Header("Bomb�̃^�O")] public string _bombTag = null;

    bool _isMove = false;
    bool _isMove02 = false;
    private float x = 0;
    private float y = 0;
    float _speed = 4f;
    [SerializeField, Header("�ҋ@����")] public float _stopTime = default;
    int _count = 0;
    private GameObject _player;
    [SerializeField] private string _playerTag = null;

    private void Start()
    {        
        _player = GameObject.FindGameObjectWithTag(_playerTag);
        _isMove = true;
        StartCoroutine(RandomMovement());
    }

    protected override void Attack()
    {

    }

    protected override void OnGetDamage()
    {

    }

    protected override void Update()
    {
        base.Update();
        if (_isMove == true)
        {
            transform.position = new Vector2(Mathf.Clamp(transform.position.x, -8f, 8f), Mathf.Clamp(transform.position.y, 0f, 4f));
        }
        if (_isMove02 == true)
        {
            transform.position = new Vector2(Mathf.Clamp(transform.position.x, -8f, 8f), Mathf.Clamp(transform.position.y, -4f, 4f));
        }
        //int count = Random.Range(0, 2);
    }
    IEnumerator RandomMovement()
    {
        yield return new WaitForSeconds(0.5f);
        _isMove = true;
        while (true)
        {
            Vector2 dir = new Vector2(Random.Range(-3.0f, 3.0f) - x, Random.Range(-1.0f, 1.0f) - y);
            Rb.velocity = dir * _speed;
            yield return new WaitForSeconds(0.5f);
            Rb.velocity = new Vector2(0, 0);
            yield return new WaitForSeconds(_stopTime);
            x += dir.x;
            y += dir.y;
            Debug.Log(x);
            Debug.Log(y);
            _count = Random.Range(0, 10);
            Debug.Log(_count);
            if (_count == 9)
            {
                _isMove = false;
                Veritical();
                break;
            }
        }
    }
    private void Veritical()//�v���C���[�̈ʒu�ɂ���č��E�̂ǂ��炩�Ɉړ����邩�����߂�
    {
        _isMove02 = true;
        Debug.Log("huuuu");
        Rb.velocity = new Vector2(0, 0);
        if (_player.transform.position.x >= transform.position.x)//�v���C���[���E�ɂ�����
        {
            Debug.Log("right");
            Rb.velocity = new Vector2(4, 0);
            StartCoroutine(Stop1());
        }
        else                                                     //���ɂ�����
        {
            Debug.Log("left");
            Rb.velocity = new Vector2(4, 0);
            StartCoroutine(Stop2());
        }



        IEnumerator Stop1()//�v���C���[�Ɠ���x���W�ɂȂ�Ǝ~�܂�
        {
            Debug.Log("Step1");
            while (true)
            {
                yield return new WaitForSeconds(0.1f);
                if (_player.transform.position.x <= transform.position.x)
                {
                    Rb.velocity = new Vector2(0, 0);
                    StartCoroutine(Down());
                    break;
                }
            }
        }
        IEnumerator Stop2()//�v���C���[�Ɠ���x���W�ɂȂ�Ǝ~�܂�
        {
            Debug.Log("Step2");
            while (true)
            {
                yield return new WaitForSeconds(0.1f);
                if (_player.transform.position.x >= transform.position.x)
                {
                    Rb.velocity = new Vector2(0, 0);
                    StartCoroutine(Down());
                    break;
                }
            }
        }
        IEnumerator Down()//���ɍs��
        {
            Debug.Log("Down");
            yield return new WaitForSeconds(0.5f);
            while (true)
            {
                yield return new WaitForSeconds(0.1f);
                if (_isMove)
                {
                    Rb.velocity = new Vector2(0, -4);

                    if (_player.transform.position.y + 1 >= transform.position.y)//�v���C���[�̍����܂ŗ�����
                    {
                        Rb.velocity = new Vector2(0, 0);
                        yield return new WaitForSeconds(1);
                        Debug.Log("Up");
                        StartCoroutine(Up());
                        break;
                    }
                    else if (transform.position.y <= -4)//���~�����܂ŗ�����
                    {
                        Rb.velocity = new Vector2(0, 0);
                        yield return new WaitForSeconds(1);
                        //_isMove = false;
                        Debug.Log("Up");
                        StartCoroutine(Up());
                        break;
                    }
                }
            }
        }
        IEnumerator Up()//��ɍs��
        {
            Debug.Log("Up");
            _isMove = false;
            Rb.velocity = new Vector2(0, 3);
            yield return new WaitForSeconds(3);
            Rb.velocity = new Vector2(0, 0);
            yield return new WaitForSeconds(2);
            StartCoroutine(Back());
        }

        IEnumerator Back()//���Α��ɍs��
        {
            Debug.Log("Back");

            while (true)
            {
                yield return new WaitForSeconds(0.1f);
                if (0 >= transform.position.x)//�v���C���[���E�ɂ�����
                {
                    Debug.Log("right");
                    Rb.velocity = new Vector2(4, 0);
                    yield return new WaitForSeconds(Random.Range(1, 3));
                    Rb.velocity = new Vector2(0, 0);
                    break;
                }
                else                          //���ɂ�����
                {
                    Debug.Log("left");
                    Rb.velocity = new Vector2(-4, 0);
                    yield return new WaitForSeconds(Random.Range(1, 3));
                    Rb.velocity = new Vector2(0, 0);
                    break;
                }
            }
        }
    }
}
