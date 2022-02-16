using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpecialAttack : EnemyBese
{
    /// <summary>�o���b�g�̃v���n�u</summary>
    [SerializeField, Header("Bullet�̃v���n�u")] List<GameObject> _enemyBulletPrefab = new List<GameObject>();
    /// <summary>�o���b�g�𔭎˂���|�W�V����</summary>
    [SerializeField, Header("Bullet�𔭎˂���|�W�V����")] Rigidbody2D _muzzle = null;
    /// <summary>�X�v���C�g(�X�N���C�g����Ȃ���)</summary>
    SpriteRenderer _sr;
    /// <summary>�����ʒu</summary>
    const float MIDDLE_POSITION = 0f;
    /// <summary>�����̍U������</summary>
     float _initialDamageRatio;

    [SerializeField, Header("�K�E���Ɉړ�����|�W�V����")] Transform _specialAttackPos = null;


    /// <summary>�����A������</summary>
    private float _horizontal = 0f;
    /// <summary>�����A�c����</summary>
    private float _veritical = 0f;
    /// <summary>���x</summary>
    [SerializeField, Header("�X�s�[�h")] float _speed = 4f;
    /// <summary>��~����</summary>
    [SerializeField, Header("��~����")] float _stopTime = 2f;
    /// <summary>�ړ�����</summary>
    [SerializeField, Header("�ړ�����")] float _moveTime = 0.5f;
    /// <summary>�E��</summary>
    [SerializeField, Header("�E��")] float _rightLimit = 4f;
    /// <summary>����</summary>
    [SerializeField, Header("����")] float _leftLimit = -4f;
    /// <summary>���</summary>
    [SerializeField, Header("���")] float _upperLimit = 2.5f;
    /// <summary>����</summary>
    [SerializeField, Header("����")] float _lowerLimit = 1.5f;
    /// <summary>����</summary>
    Vector2 _dir;
    /// <summary>������</summary>
    const float LEFT_DIR = -1f;
    /// <summary>�E����</summary>
    const float RIGHT_DIR = 1f;
    /// <summary>�����</summary>
    const float UP_DIR = 1f;
    /// <summary>������</summary>
    const float DOWN_DIR = -1;
    /// <summary>�����Ȃ�</summary>
    const float NO_DIR = 0f;


    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        /*if (_muzzles == null || _muzzles.Length == 0)
        {
            _muzzles = new Transform[1] { this.transform };
        }*/
        //StartCoroutine(RandomMovement());

    }
    protected override void Update()
    {
        base.Update();
        Rb.velocity = Vector3.MoveTowards(-transform.position, _specialAttackPos.position, 1f).normalized;

        if (Rb.velocity.x > MIDDLE_POSITION)//�E�Ɉړ�������
        {
            _sr.flipX = true;//�E������
        }
        else if (Rb.velocity.x < MIDDLE_POSITION)//���Ɉړ�������
        {
            _sr.flipX = false;//��������
        }
        //0�̎��A��~���͉����s��Ȃ��i�O�̏�Ԃ̂܂܁j
    }

    protected override void Attack()
    {
        //Quaternion.AngleAxis(_muzzle.rotation, Vector3.up);
        //Quaternion.AngleAxis(_muzzle.rotation, Vector3.forward);
        //(�e�̎��,muzzle�̈ʒu,��]�l)
        //Instantiate(_enemyBulletPrefab[0], _muzzle.position, Quaternion.AngleAxis(_muzzle.rotation, Vector3.forward));
        _muzzle.rotation += 10f;
        //(muzzle�̈ʒu,enum.�e�̎��)
        ObjectPool.Instance.UseBullet(_muzzle.position, PoolObjectType.Player01Power1);
    }

    /// <summary>��ƒ�������}�W�b�N�i���o�[�ɂ��Ă͉��������Ȃ�_��</summary>
    IEnumerator SpecialAttack()
    {
        int count = 0;
        _initialDamageRatio = AddDamageRatio;//�����l��ݒ�
        //AddDamageRatio = 0.5f;//�K�E���͍U��������ύX
        Rb.velocity = Vector2.MoveTowards(transform.position, new Vector2(0f,4f), _speed);
        while (true)
        {
            // 8�b���ɁA�Ԋu�U�x�A���x�P��muzzle�𒆐S�Ƃ��đS���ʒe���ˁB
            for (int rad = 0; rad < 360; rad += 6)
            {
                _muzzle.rotation += 10f;
            }
            yield return new WaitForSeconds(8.0f);
            count++;
            if(0 == 0)
            {
                break;
            }
        }
        //AddDamageRatio = _initialDamageRatio;//���ɖ߂�

    }


    protected override void OnGetDamage()
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// �����_�������ɓ���
    /// </summary>
    IEnumerator RandomMovement()
    {
        while (true)
        {
            //��莞�Ԏ~�܂�
            Rb.velocity = Vector2.zero;
            yield return new WaitForSeconds(_stopTime);

            //�ꏊ�ɂ���Ĉړ��ł��鍶�E�����𐧌�����
            if (transform.position.x > _rightLimit)         //�E�Ɉړ�����������
            {
                _horizontal = Random.Range(LEFT_DIR, NO_DIR);//���ړ��\
            }
            else if (transform.position.x < _leftLimit)   //���Ɉړ���������
            {
                _horizontal = Random.Range(NO_DIR, RIGHT_DIR);//�E�ړ��\
            }
            else�@�@�@�@�@�@�@�@�@�@�@�@         //���E�ǂ����ɂ��ړ��������ĂȂ��Ȃ�
            {
                _horizontal = Random.Range(LEFT_DIR, RIGHT_DIR);//���R�ɍ��E�ړ��\          
            }

            //�ꏊ�ɂ���Ĉړ��ł���㉺�����𐧌�����
            if (transform.position.y > _upperLimit)      //��Ɉړ�����������
            {
                _veritical = Random.Range(DOWN_DIR, NO_DIR);//���ړ��\
            }
            else if (transform.position.y < _lowerLimit)//���Ɉړ�����������
            {
                _veritical = Random.Range(NO_DIR, UP_DIR);//��ړ��\
            }
            else�@�@�@�@�@�@�@�@�@�@�@�@�@�@      //�㉺�ǂ����ɂ��ړ��������ĂȂ��Ȃ�
            {
                _veritical = Random.Range(DOWN_DIR, UP_DIR);//���R�ɏ㉺�ړ��\
            }

            _dir = new Vector2(_horizontal, _veritical);//�����_���Ɉړ�
            //��莞�Ԉړ�����
            Rb.velocity = _dir.normalized * _speed;
            yield return new WaitForSeconds(_moveTime);

            Debug.Log("x" + _horizontal);
            Debug.Log("y" + _veritical);
        }
    }
}