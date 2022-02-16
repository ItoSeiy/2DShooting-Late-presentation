using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpecialAttack : EnemyBase
{
    /// <summary>�o���b�g�̃v���n�u</summary>
    [SerializeField, Header("Bullet�̃v���n�u")] List<GameObject> _enemyBulletPrefab = new List<GameObject>();
    /// <summary>�o���b�g�𔭎˂���|�W�V����</summary>
    [SerializeField, Header("Bullet�𔭎˂���|�W�V����")] Transform _muzzle = null;
    /// <summary>�X�v���C�g(�X�N���C�g����Ȃ���)</summary>
    SpriteRenderer _sr;
    /// <summary>�����ʒu</summary>
    const float MIDDLE_POSITION = 0f;
    /// <summary>�����̍U������</summary>
     float _initialDamageRatio;
    /// <summary>�K�E�O�Ɉړ�����|�W�V����</summary>
    [SerializeField, Header("�K�E�O�Ɉړ�����|�W�V����")] Transform _spAttackPos = null;
    /// <summary>����</summary>
    float _time = 0f;
    /// <summary>HP�o�[�̖{��</summary>
    [SerializeField,Header("HP�o�[�̐�")]
    float _hPBar = 0;
    /// <summary>HP�o�[�P�{����HP</summary>
    float _hP = 0f;
    /// <summary>HP�o�[�̃J�E���g</summary>
    float _hPBarCount = 0f;
    /// <summary>-1����</summary>
    const float _minus = -1f;
    /// <summary>�E���͈̔�</summary>
    bool _rightRange;
    /// <summary>�����͈̔�</summary>
    bool _leftRange;
    /// <summary>�㑤�͈̔�</summary>
    bool _upperRange;    
    /// <summary>�����͈̔�</summary>
    bool _downRange;
    /// <summary>������</summary>
    float _horizontalDir = 0f;
    /// <summary>�c����</summary>
    float _verticalDir = 0f;
    /// <summary>�K�E�ҋ@����</summary>
    [SerializeField,Header("�K�E�Z�ҋ@����")]
    float _waitTime = 5f;
    /// <summary>�C���l</summary>
    const float PLAYER_POS_OFFSET = 0.5f;
    /// <summary>����񐔂̐���</summary>
    const float JUDGMENT_TIME = 1/60f;

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
        _hPBarCount = _hPBar;
        _hP = EnemyHp / _hPBar;
        Debug.Log(_hPBar);

        //StartCoroutine(RandomMovement());
        //StartCoroutine(SpecialAttack());
        Attack();
       
    }
    protected override void Update()
    {
        base.Update();
        _time += Time.deltaTime;

        if (Rb.velocity.x > MIDDLE_POSITION)//�E�Ɉړ�������
        {
            _sr.flipX = true;//�E������
        }
        else if (Rb.velocity.x < MIDDLE_POSITION)//���Ɉړ�������
        {
            _sr.flipX = false;//��������
        }
        //0�̎��A��~���͉����s��Ȃ��i�O�̏�Ԃ̂܂܁j

        //HP�o�[�P�{�������Ȃ�����
        if (EnemyHp <= _hP * (_hPBarCount + _minus))
        {
            Debug.Log("�̈�W�J");
            StartCoroutine(SpecialAttack());            
            _hPBarCount--;           
        }
    }

    
    protected override void Attack()
    {
        //Quaternion.AngleAxis(_muzzle.rotation, Vector3.forward);
        //(�e�̎��,muzzle�̈ʒu,��]�l)
        //Instantiate(_enemyBulletPrefab[0], _muzzle.position, Quaternion.AngleAxis(_muzzle.rotation, Vector3.forward));
        //_muzzle.rotation += 10f;
        //(muzzle�̈ʒu,enum.�e�̎��)
        ObjectPool.Instance.UseBullet(_muzzle.position, PoolObjectType.Player01Power1);
    }

    IEnumerator SpecialAttack()
    {
        _time = 0f;//�^�C�����Z�b�g
        
        //�K�E����Ƃ���BOSS�͕��O�ɂ���0�A�x��2���̈ʒu(��)�ɁA�ړ�����
        while (true)
        {
            yield return new WaitForSeconds(JUDGMENT_TIME);//����񐔂̐���
            //������
            _horizontalDir = _spAttackPos.position.x - transform.position.x;
            //�c����
            _verticalDir = _spAttackPos.position.y - transform.position.y;           
            //���͈̔͂̏�����      
            _rightRange = transform.position.x < _spAttackPos.position.x + PLAYER_POS_OFFSET;
            _leftRange = transform.position.x > _spAttackPos.position.x - PLAYER_POS_OFFSET;
            //�c�͈̔͂̏�����
            _upperRange = transform.position.y < _spAttackPos.position.y + PLAYER_POS_OFFSET;
            _downRange = transform.position.y > _spAttackPos.position.y - PLAYER_POS_OFFSET;
            //�s�������|�W�V�����Ɉړ�����
            //�߂�������
            if (_rightRange && _leftRange && _upperRange && _downRange)
            {
                Debug.Log("���ʂ�" + _rightRange + _leftRange + _upperRange + _downRange);
                //�X���[�Y�Ɉړ�
                Rb.velocity = new Vector2(_horizontalDir, _verticalDir) * _speed;
            }
            //����������
            else
            {
                Debug.Log("���ʂ�" + _rightRange + _leftRange + _upperRange + _downRange);
                //���肵�Ĉړ�
                Rb.velocity = new Vector2(_horizontalDir, _verticalDir).normalized * _speed;
            }

            //���b�o������
            if (_time >= _waitTime)
            {
                Debug.Log("stop");
                Rb.velocity = Vector2.zero;//��~
                transform.position = _spAttackPos.position;//�{�X�̈ʒu���C��
                break;//�I���
            }
        }
        _initialDamageRatio = AddDamageRatio;//�����l��ݒ�
        //AddDamageRatio = 0.5f;//�K�E���͍U��������ύX
        _time = 0f;//�^�C�����Z�b�g

        while (true)
        {
            // 8�b���ɁA�Ԋu10�x�A���x�P��muzzle�𒆐S�Ƃ��đS���ʒe���˗\��
            yield return new WaitForSeconds(JUDGMENT_TIME);
            if(_time >= 20f)
            {
                break;
            }
        }

        //AddDamageRatio = _initialDamageRatio;//���ɖ߂�
        yield break;
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