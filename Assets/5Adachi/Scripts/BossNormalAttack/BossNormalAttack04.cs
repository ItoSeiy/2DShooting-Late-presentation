using Overdose.Calculation;
using Overdose.Data;
using System.Collections;
using UnityEngine;

public class BossNormalAttack04 : BossAttackAction
{
    /// <summary>����</summary>
    Vector3 _dir;
    /// <summary>�e�̌����ڂ̎��</summary>
    int _pattern = 0;
    /// <summary>�U����</summary>
    int _attackCount = 0;
    /// <summary>�^�C�}�[</summary>
    float _timer = 0f;
    /// <summary>�o���b�g�𔭎˂���|�W�V����</summary>
    [SerializeField, Header("Bullet�𔭎˂���|�W�V����")] Transform _muzzle = null;
    /// <summary>�U���p�x</summary>
    [SerializeField, Header("�U���p�x(�b)")] private float _attackInterval = 0.64f;
    /// <summary>���˂���e��ݒ�ł���</summary>
    [SerializeField, Header("���˂���e�̐ݒ�")] PoolObjectType[] _bullet;
    /// <summary>�}�Y���̊p�x�Ԋu</summary>
    [SerializeField, Header("�}�Y���̊p�x�Ԋu")] float _rotationInterval = 1f;
    /// <summary>1��̏����Œe�𔭎˂����</summary>
    [SerializeField, Header("1��̏����Œe�𔭎˂����")] int _maximumCount = 7;
    /// <summary>���̍s������o�鎞��</summary>
    [SerializeField, Header("���̍s������o�鎞��")] float _endingTime = 20f;
    /// <summary>�A�C�e���𗎂Ƃ��m��</summary>
    [SerializeField, Header("�A�C�e���𗎂Ƃ��m��")] int _probability = 50;
    /// <summary>�U�����̉�</summary>
    [SerializeField, Header("�U�����̉�")] SoundType _normalAttack;    
    /// <summary>����炷�^�C�~���O</summary>
    [SerializeField, Header("����炷�^�C�~���O")] int _maxAttackCount = 2;
    /// <summary>�ŏ��̉�]�l</summary>
    const float MINIMUM_ROTATION_RANGE = 0f;
    /// <summary>�ő�̉�]�l</summary>
    const float MAXIMUM_ROTATION_RANGE = 360f;
    /// <summary>1��̏����Œe�𔭎˂���񐔂̏����l</summary>
    const int INITIAL_COUNT = 0;

    public override System.Action ActinoEnd { get; set; }

    public override void Enter(BossController contlloer)
    {
        _timer = 0f;

        SoundManager.Instance.UseSound(_normalAttack);
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
        //���̊m���ŃA�C�e���𗎂Ƃ�
        if (Calculator.RandomBool(_probability))
        {
            contlloer.ItemDrop();
        }

        StopAllCoroutines();
    }

    //Attack�֐��ɓ����ʏ�U��
    IEnumerator Attack(BossController controller)
    {
        while (true)
        {
            //�^�[�Q�b�g�i�v���C���[�j�̕������v�Z
            _dir = (GameManager.Instance.Player.transform.position - _muzzle.transform.position);
            //�^�[�Q�b�g�i�v���C���[�j�̕����ɉ�]
            _muzzle.transform.rotation = Quaternion.FromToRotation(Vector3.up, _dir);
            //�e���}�Y��0�̌����ɍ��킹�Ēe�𔭎�
            ObjectPool.Instance.UseObject(_muzzle.position, _bullet[_pattern]).transform.rotation = _muzzle.rotation;

            if(_attackCount >= _maxAttackCount)
            {
                //�U�����̉�
                SoundManager.Instance.UseSound(_normalAttack);
                _attackCount = 0;
            }
            

            //���������𐔉�(_maximumCount)�J��Ԃ�
            for (int count = INITIAL_COUNT; count < _maximumCount; count++)
            {
                //�e�̌����ڂ������_���ŕς���
                _pattern = Random.Range(0, _bullet.Length);
                ///�}�Y������]����///
                Vector3 localAngle = _muzzle.localEulerAngles;// ���[�J�����W����Ɏ擾
                // �����_���Ȋp�x��ݒ�i�i�@0�x�@�`�@360�x/�}�Y���̊p�x�Ԋu�@�j* �}�Y���̊p�x�Ԋu�@)
                localAngle.z = Random.Range(MINIMUM_ROTATION_RANGE, MAXIMUM_ROTATION_RANGE / _rotationInterval) * _rotationInterval;
                _muzzle.localEulerAngles = localAngle;//��]����
                //�e���}�Y���̌����ɍ��킹�Ēe�𔭎�
                ObjectPool.Instance.UseObject(_muzzle.position, _bullet[_pattern]).transform.rotation = _muzzle.rotation;
            }

            _attackCount++;

            yield return new WaitForSeconds(_attackInterval);
        }
    }

}
