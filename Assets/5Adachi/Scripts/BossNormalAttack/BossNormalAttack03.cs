using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNormalAttack03 : BossAttackAction
{
    /// <summary>����</summary>
    Vector3 _dir;
    /// <summary>�}�Y���̊p�x�Ԋu</summary>
    float _rotationInterval = 360f;
    /// <summary>���˂�����</summary>
    int _attackCount = 0;
    /// <summary>�e�̌����ڂ̎��</summary>
    int _pattern = 0;
    /// <summary>�^�C�}�[</summary>
    float _timer = 0f;
    /// <summary>���˂���ő��</summary>
    [SerializeField,Header("���˂���ő��")] int _maxAttackCount = 7;
    /// <summary>�o���b�g�𔭎˂���|�W�V����</summary>
    [SerializeField, Header("Bullet�𔭎˂���|�W�V����")] Transform[] _muzzles = null;
    /// <summary>�U���p�x</summary>
    [SerializeField, Header("�U���p�x(�b)")] private float _attackInterval = 0.64f;
    /// <summary>���˂���e��ݒ�ł���</summary>
    [SerializeField, Header("���˂���e�̐ݒ�")] PoolObjectType[] _bullet;
    /// <summary>�ő�̒e��</summary>
    [SerializeField,Header("�ő�̒e��")] int _maximumBulletRange = 11;
    /// <summary>���̍s������o�鎞��</summary>
    [SerializeField, Header("���̍s������o�鎞��")] float _endingTime = 20f;
    /// <summary>�ŏ��̉�]�l</summary>
    const float MINIMUM_ROTATION_RANGE = 0f;
    /// <summary>�ő�̉�]�l</summary>
    const float MAX_ROTATION_RANGE = 360f;
    /// <summary>�ŏ��̒e��</summary>
    const int MINIMUM_BULLET_RANGE = 2;
    /// <summary>���ˉ񐔂����Z�b�g</summary>
    const int ATTACK_COUNT_RESET = 0;
    

    public override System.Action ActinoEnd { get; set; }

    public override void Enter(BossController contlloer)
    {
        _timer = 0f;
        StartCoroutine(Attack(contlloer));
    }

    public override void ManagedUpdate(BossController contlloer)
    {
        _timer += Time.deltaTime;

        if(_timer >= _endingTime)
        {
            ActinoEnd?.Invoke();
        }
    }

    public override void Exit(BossController contlloer)
    {
        contlloer.ItemDrop();
        StopAllCoroutines();
    }

    //Attack�֐��ɓ����ʏ�U��
    IEnumerator Attack(BossController controller)
    {
        while (true)
        {
            //���̉񐔔��˂�����
            if(_attackCount >= _maxAttackCount)
            {
                //�e�̌����ڂ������_���ŕς���
                _pattern = Random.Range(0, _bullet.Length);
                //1��̍U���Œe���΂���(3�`?)
                _rotationInterval = MAX_ROTATION_RANGE / Random.Range(MINIMUM_BULLET_RANGE, _maximumBulletRange);
                _attackCount = ATTACK_COUNT_RESET;//���ˉ񐔂����Z�b�g
            }
            //�^�[�Q�b�g�i�v���C���[�j�̕������v�Z
            _dir = (GameManager.Instance.Player.transform.position - _muzzles[0].transform.position);
            //�^�[�Q�b�g�i�v���C���[�j�̕����ɉ�]
            _muzzles[0].transform.rotation = Quaternion.FromToRotation(Vector3.up, _dir);

            Vector3 firstLocalAngle = _muzzles[0].localEulerAngles;// ���[�J�����W����Ɏ擾

            //���������𐔉�(_maximumCount)�J��Ԃ�
            for (float rotation = MINIMUM_ROTATION_RANGE + firstLocalAngle.z; rotation <= MAX_ROTATION_RANGE + firstLocalAngle.z; rotation += _rotationInterval)
            {
                Vector3 secondLocalAngle = _muzzles[1].localEulerAngles;// ���[�J�����W����Ɏ擾
                secondLocalAngle.z = rotation;// �p�x��ݒ�
                _muzzles[1].localEulerAngles = secondLocalAngle;//��]����
                //�e���}�Y���̌����ɍ��킹�Ēe�𔭎ˁi����Bomb�ɂ��Ă܂��j
                ObjectPool.Instance.UseObject(_muzzles[1].position, _bullet[_pattern]).transform.rotation = _muzzles[1].rotation;
            }
            _attackCount++;//���ˉ񐔂��P����
            yield return new WaitForSeconds(_attackInterval);//�U���p�x(�b)
        }
    }

}
