using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMovement03 : EnemyBese
{
    [SerializeField, Header("Bomb�̃^�O")] public string _bombTag = null;

    //bool _isMove02 = false;
    private float x = 0;
    private float y = 0;
    float _speed = 4f;
    [SerializeField, Header("�ҋ@����")] public float _stopTime = default;
    Vector2 _dir;
    int _count = 0;
    private GameObject _player;
    [SerializeField] private string _playerTag = null;
    int _switch = 0;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag(_playerTag);
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
    }
    IEnumerator RandomMovement()
    {
        yield return new WaitForSeconds(0.5f);


        Rb.velocity = new Vector2(0, 0);


        while (true)
        {
            Rb.velocity = new Vector2(0, 0);
            yield return new WaitForSeconds(_stopTime);

            if (transform.position.y > 2.5f)      //��Ɉړ�����������
            {
                y = Random.Range(-1.0f, -0.1f);
            }
            else if (transform.position.y < 1.5f)//���Ɉړ�����������
            {
                y = Random.Range(0.1f, 1.0f);
            }
            else                                //�㉺�ǂ����ɂ��ړ��������ĂȂ��Ȃ�
            {
                y = Random.Range(-1.0f, 1.0f);
            }
            if (transform.position.x > 4)         //�E�Ɉړ�����������
            {
                x = (Random.Range(-3.0f, -1.0f));
            }
            else if (transform.position.x < -4)   //���Ɉړ���������
            {
                x = (Random.Range(1.0f, 3.0f));
            }
            else                                //���E�ǂ����ɂ��ړ��������ĂȂ��Ȃ�
            {
                x = Random.Range(-3.0f, 3.0f);
            }

            _dir = new Vector2(x, y);

            Rb.velocity = _dir * _speed;
            yield return new WaitForSeconds(0.5f);

            Debug.Log(x);
            Debug.Log(y);
            _count = Random.Range(0, 10);
            Debug.Log(_count);
            if (_count >= 1)
            {
                StartCoroutine("Rush");
                break;
            }
        }
    }
    IEnumerator Rush()//�v���C���[�̈ʒu�ɂ���č��E�̂ǂ��炩�Ɉړ����邩�����߂�
    {
        Debug.Log("huuuu");
        if (_player.transform.position.x >= transform.position.x)//�v���C���[���E�ɂ�����
        {
            Debug.Log("Right");
            _switch = 1;
            Rb.velocity = new Vector2(4, 0);
        }
        else                                                     //���ɂ�����
        {
            Debug.Log("Left");
            _switch = 2;
            Rb.velocity = new Vector2(-4, 0);
        }

        while (true) //�v���C���[�̕ӂ�ɒ�������
        {
            yield return new WaitForSeconds(0.1f);
            if (_switch == 1 && _player.transform.position.x <= transform.position.x)//�E�Ɉړ������Ƃ���
            {
                Debug.Log("Right2");
                Rb.velocity = new Vector2(0, 0);
                break;
            }
            else if (_switch == 2 && _player.transform.position.x >= transform.position.x)//���Ɉړ������Ƃ���
            {
                Debug.Log("Left2");
                Rb.velocity = new Vector2(0, 0);
                break;
            }
        }

        _switch = 0;

        yield return new WaitForSeconds(0.5f);

        while (true)//���ɍs��
        {
            Debug.Log("Down");
            yield return new WaitForSeconds(0.1f);
            Rb.velocity = new Vector2(0, -4);
            if (transform.position.y <= -3)//���܂ŗ�����
            {
                Rb.velocity = new Vector2(0, 0);
                yield return new WaitForSeconds(1); ;
                break;
            }
        }



        while (true)//���̏ꏊ�܂ŏ�ɏオ��
        {
            Rb.velocity = new Vector2(0, 3);
            Debug.Log("Up");
            yield return new WaitForSeconds(0.1f);
            if (3 <= transform.position.y)//���̏ꏊ�܂ł�����
            {
                Debug.Log("Up2");
                Rb.velocity = new Vector2(0, 0);
                yield return new WaitForSeconds(2);
                StartCoroutine("RandomMovement");
                break;
            }
        }

    }
}
