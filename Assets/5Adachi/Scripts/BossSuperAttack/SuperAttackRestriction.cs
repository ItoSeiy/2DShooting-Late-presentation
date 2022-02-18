using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperAttackRestriction: MonoBehaviour
{
    /// <summary>�`���傫���̊T�O������������</summary>
    Rigidbody2D _rb;
    /// <summary>�o���b�g�𔭎˂���|�W�V����</summary>
    [SerializeField, Header("Bullet�𔭎˂���|�W�V����")] Transform[] _muzzles = null;
    /// <summary>�K�E�O�Ɉړ�����Ƃ��̃X�s�[�h</summary>
    [SerializeField, Header("�K�E�O�Ɉړ�����Ƃ��̃X�s�[�h")] float _speed = 4f;
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
    float a = 0f;
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
    /// <summary>���Z�b�g�^�C�}�[</summary>
    const float RESET_TIME = 0f;
    /// <summary>�C���l</summary>
    const float MUZZLE_ROT_OFFSET = 4.9f;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();//�s�X�^�[�g�t�ŃQ�b�g�R���|�[�l���g
        StartCoroutine(Restriction()); //�R���[�`���𔭓�    
    }
    void Update()
    {
        _timer += Time.deltaTime;//�^�C�}�[
    }

    /// <summary>Firework�~Windmill�̂悤�ȋO���A�K�E�Z�c�莞�Ԃ�������؂�Ƌt��]�ɂȂ�
    /// <para>Firework���ԉ΂̂悤�ȋO��,�S���ʂɔ���</para>
    /// <para>Windmill���Q���̂悤�ȋO��,�����v���ɔ���</para></summary>
    IEnumerator Restriction()
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
            //�K�E�Z�������Ԃ̌㔼�ɂȂ����甽���v���ɑS���ʔ���
            if (_timer >= _activationTime / 2f)
            {
                //360�x�S���ʂɔ���
                for (float angle = 0 + a + MUZZLE_ROT_OFFSET; angle <= 360f + a + MUZZLE_ROT_OFFSET; angle += 10f)
                {
                    Vector3 localAngle = _muzzles[0].localEulerAngles;// ���[�J�����W����Ɏ擾
                    localAngle.z = -angle;// �p�x��ݒ�
                    _muzzles[0].localEulerAngles = localAngle;//��]����
                    //�e�𔭎ˁi����Bomb�ɂ��Ă܂��j
                    var bossEnemyBullet = ObjectPool.Instance.UseBullet(_muzzles[0].position, PoolObjectType.Player01BombChild);
                    //�e���}�Y���̌����ɍ��킹��
                    bossEnemyBullet.transform.rotation = _muzzles[0].rotation;
                }

            }
            //�K�E�Z�������Ԃ̑O���܂ł͔����v���ɑS���ʔ���
            else
            {
                //360�x�S���ʂɔ���
                for (float angle = 0 + a; angle <= 360f + a; angle += 10)
                {
                    Vector3 localAngle = _muzzles[0].localEulerAngles;// ���[�J�����W����Ɏ擾
                    localAngle.z = angle;// �p�x��ݒ�
                    _muzzles[0].localEulerAngles = localAngle;//��]����
                                                              //�e�𔭎ˁi����Bomb�ɂ��Ă܂��j
                    var bossEnemyBullet = ObjectPool.Instance.UseBullet(_muzzles[0].position, PoolObjectType.Player01BombChild);
                    //�e���}�Y���̌����ɍ��킹��
                    bossEnemyBullet.transform.rotation = _muzzles[0].rotation;
                }
            }
            a++;

            yield return new WaitForSeconds(_attackInterval);//�U���p�x(�b)
            //���b�o������
            if (_timer >= _activationTime)
            {
                /*localAngle.z = ZERO_DEGREE_ANGLE;// �p�x��0�x�ɐݒ�
                _muzzles[0].localEulerAngles = localAngle;//��~*/
                break;//�I��
            }
        }

        //AddDamageRatio = _initialDamageRatio;//�U�����������ɖ߂�
        yield break;//�I��
    }
}
