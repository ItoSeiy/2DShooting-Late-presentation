using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperAttackPrison : MonoBehaviour
{
    /// <summary>�`���傫���̊T�O������������</summary>
    Rigidbody2D _rb;
    /// <summary>����</summary>
    Vector3 _dir;
    /// <summary>�v���C���[�̃I�u�W�F�N�g</summary>
    private GameObject _player;
    [SerializeField, Header("player��tag")] string _playerTag = null;
    /// <summary>�o���b�g�𔭎˂���|�W�V����</summary>
    [SerializeField, Header("Bullet�𔭎˂���|�W�V����")] Transform[] _muzzles = null;
    /// <summary>���x</summary>
    [SerializeField, Header("�X�s�[�h")] float _speed = 4f;
    /// <summary>�K�E�O�Ɉړ�����|�W�V����</summary>
    [SerializeField, Header("�K�E�O�Ɉړ�����|�W�V����")] Transform _superAttackPos = null;
    /// <summary>�����̍U������</summary>
    float _initialDamageRatio;
    /// <summary>�^�C�}�[</summary>
    float _timer = 0f;
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
    /// <summary>�K�E�Z�ҋ@����</summary>
    [SerializeField, Header("�K�E�Z�ҋ@����")] float _waitTime = 5f;
    /// <summary>�K�E�Z��������</summary>
    [SerializeField, Header("�K�E�Z��������")] float _activationTime = 30f;
    /// <summary>�U���p�x</summary>
    [SerializeField, Header("�U���p�x(�b)")] private float _attackInterval = 0.6f;
    /// <summary>�C���l</summary>
    const float PLAYER_POS_OFFSET = 0.5f;
    /// <summary>����񐔂̐���</summary>
    const float JUDGMENT_TIME = 1 / 60f;
    /// <summary>0�x�̊p�x</summary>
    const float ZERO_DEGREE_ANGLE = 0f;
    /// <summary>���Z�b�g�^�C�}�[</summary>
    const float RESET_TIME = 0f;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag(_playerTag);
        StartCoroutine(Prison());
    }

    void Update()
    {
        _timer += Time.deltaTime;//�^�C�}�[
        //�^�[�Q�b�g�i�v���C���[�j�̕������v�Z
        _dir = (_player.transform.position - _muzzles[0].transform.position);
        //�^�[�Q�b�g�i�v���C���[�j�̕����ɉ�]
        _muzzles[0].transform.rotation = Quaternion.FromToRotation(Vector3.up, _dir);
    }

    /// <summary>player������߂�player����������ɒe�𔭎˂���</summary>
    IEnumerator Prison()
    {
        _timer = RESET_TIME;//�^�C�����Z�b�g

        //�K�E����Ƃ���BOSS�͕��O�ɂ���0�A�x��2���̈ʒu(��)�ɁA�ړ�����
        while (true)
        {
            yield return new WaitForSeconds(JUDGMENT_TIME);//����񐔂̐���
            //������
            _horizontalDir = _superAttackPos.position.x - transform.position.x;
            //�c����
            _verticalDir = _superAttackPos.position.y - transform.position.y;
            //���͈̔͂̏�����      
            _rightRange = transform.position.x < _superAttackPos.position.x + PLAYER_POS_OFFSET;
            _leftRange = transform.position.x > _superAttackPos.position.x - PLAYER_POS_OFFSET;
            //�c�͈̔͂̏�����
            _upperRange = transform.position.y < _superAttackPos.position.y + PLAYER_POS_OFFSET;
            _downRange = transform.position.y > _superAttackPos.position.y - PLAYER_POS_OFFSET;
            //�s�������|�W�V�����Ɉړ�����
            //�߂�������
            if (_rightRange && _leftRange && _upperRange && _downRange)
            {
                Debug.Log("���ʂ�" + _rightRange + _leftRange + _upperRange + _downRange);
                //�X���[�Y�Ɉړ�
                _rb.velocity = new Vector2(_horizontalDir, _verticalDir) * _speed;
            }
            //����������
            else
            {
                Debug.Log("���ʂ�" + _rightRange + _leftRange + _upperRange + _downRange);
                //���肵�Ĉړ�
                _rb.velocity = new Vector2(_horizontalDir, _verticalDir).normalized * _speed;
            }

            //���b�o������
            if (_timer >= _waitTime)
            {
                Debug.Log("stop");
                _rb.velocity = Vector2.zero;//��~
                transform.position = _superAttackPos.position;//�{�X�̈ʒu���C��
                break;//�I���
            }
        }
        //_initialDamageRatio = AddDamageRatio;//�����l��ݒ�
        //AddDamageRatio = 0.5f;//�K�E���͍U��������ύX
        _timer = RESET_TIME;//�^�C�����Z�b�g

        //�K�E�Z����
        while (true)
        {           
            //�����}�Y��

            //�e�𔭎ˁi����Bomb�ɂ��Ă܂��j
            var bossEnemyBulletStraight = ObjectPool.Instance.UseBullet(_muzzles[0].position, PoolObjectType.Player01BombChild);
            //�e���}�Y��0�̌����ɍ��킹��
            bossEnemyBulletStraight.transform.rotation = _muzzles[0].rotation;

            //�����Ȃ��}�Y��

            //�e�𔭎ˁi�����}�Y���̒e���E���j
            var bossEnemyBulletRight = ObjectPool.Instance.UseBullet(_muzzles[1].position, PoolObjectType.Player01BombChild);
            //�e���}�Y��1�̌����ɍ��킹��
            bossEnemyBulletRight.transform.rotation = _muzzles[1].rotation;
            //�e�𔭎ˁi�����}�Y����荶���j
            var bossEnemyBulletLeft = ObjectPool.Instance.UseBullet(_muzzles[2].position, PoolObjectType.Player01BombChild);
            //�e���}�Y��2�̌����ɍ��킹��
            bossEnemyBulletLeft.transform.rotation = _muzzles[2].rotation;

            yield return new WaitForSeconds(_attackInterval);//�U���p�x

            //���b�o������
            if (_timer >= _activationTime)
            {
                break;//�I��
            }
        }
        //AddDamageRatio = _initialDamageRatio;//�U�����������ɖ߂�
        yield break;//�I��
    }
}
