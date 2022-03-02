using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNormalAttack05 : BossAttackAction
{
    /// <summary>����</summary>
    Vector3 _dir;
    /// <summary>���̃X�N���v�g�Ŏg���^�C�}�[</summary>
    float _defaultTimer = 0f;
    /// <summary>�^�C�}�[</summary>
    float _timer = 0f;
    /// <summary>�v���C���[�̃I�u�W�F�N�g</summary>
    private GameObject _player;
    /// <summary>�e�̌����ڂ̎��</summary>
    int _pattern = 0;
    /// <summary>���S�Ȓʏ�U���ɂȂ�̂���񂵂��ł��Ȃ��悤�ɂ���</summary>
    bool _perfect = false;
    /// <summary>�v���C���[�̃^�O</summary>
    [SerializeField, Header("player��tag")] string _playerTag = null;
    /// <summary>�o���b�g�𔭎˂���|�W�V����</summary>
    [SerializeField, Header("Bullet�𔭎˂���|�W�V����")] Transform[] _muzzles = null;
    /// <summary>�U���p�x</summary>
    [SerializeField, Header("�U���p�x(�b)")] private float _attackInterval = 0.46f;
    /// <summary>���˂���e��ݒ�ł���</summary>
    [SerializeField, Header("���˂���e�̐ݒ�")] PoolObjectType[] _bullet;
    /// <summary>���˂���e��ݒ�ł���</summary>
    [SerializeField, Header("���˂���e�̐ݒ�(Boss05DefaultBullet2)")] PoolObjectType _secondBullet;
    /// <summary>�}�Y���̊p�x�Ԋu</summary>
    [SerializeField, Header("�}�Y���̊p�x�Ԋu")] float _rotationInterval = 45f;
    /// <summary>���̍s������o�鎞��</summary>
    [SerializeField, Header("���̍s������o�鎞��")] float _endingTime = 20f;
    /// <summary>���S�Ȓʏ�U���ɂȂ�n�߂鎞��</summary>
    const float _perfectTime = 3f;
    /// <summary>�ŏ��̉�]�l</summary>
    const float MINIMUM_ROT_RANGE = 0f;
    /// <summary>�ő�̉�]�l</summary>
    const float MAXIMUM_ROT_RANGE = 360f;
   

    public override System.Action ActinoEnd { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    
    public override void Enter(BossController contlloer)
    {
        _defaultTimer = 0f;
        _timer = 0f;
        StartCoroutine(Attack(contlloer));
    }

    public override void ManagedUpdate(BossController contlloer)
    {
        _defaultTimer += Time.deltaTime;//�^�C�}�[
        _timer += Time.deltaTime;

        if(_timer >= _endingTime)
        {
            ActinoEnd?.Invoke();
        }
    }

    public override void Exit(BossController contlloer)
    {
        StopAllCoroutines();
    }

    //Attack�֐��ɓ����ʏ�U��
    IEnumerator Attack(BossController controller)
    {
        while (true)
        {      
            if (_defaultTimer >= _perfectTime)
            {
                //�e�̌����ڂ������_���ŕς���
                _pattern = Random.Range(0, _bullet.Length);
                _defaultTimer = 0;//�^�C�}�[�����Z�b�g
                _perfect = true;//����
            }

            if(_perfect)
            {
                //�e���}�Y��2�̌����ɍ��킹�Ēe�𔭎ˁi�e�I�u�W�F�N�g�̒e��荶���j
                ObjectPool.Instance.UseObject(_muzzles[2].position, _bullet[_pattern]).transform.rotation = _muzzles[2].rotation;

                //�e���}�Y��3�̌����ɍ��킹�Ēe�𔭎ˁi�e�I�u�W�F�N�g�̒e���E���j
                ObjectPool.Instance.UseObject(_muzzles[3].position, _bullet[_pattern]).transform.rotation = _muzzles[3].rotation;
            }  
            
            //�^�[�Q�b�g�i�v���C���[�j�̕������v�Z
            _dir = (_player.transform.position - _muzzles[0].transform.position);
            //�^�[�Q�b�g�i�v���C���[�j�̕����ɉ�]
            _muzzles[0].transform.rotation = Quaternion.FromToRotation(Vector3.up, _dir);

            //�e���}�Y��0�̌����ɍ��킹�Ēe�𔭎�
            ObjectPool.Instance.UseObject(_muzzles[0].position, _bullet[_pattern]).transform.rotation = _muzzles[0].rotation;

            //_rotationInterval�Őݒ肵���p�x�Ԋu�őS���ʂɔ��˂���i�}�Y���P�Łj
            for (float rotation = MINIMUM_ROT_RANGE; rotation < MAXIMUM_ROT_RANGE; rotation += _rotationInterval)
            {
                ///�}�Y������]����///
                Vector3 secondLocalAngle = _muzzles[1].localEulerAngles;// ���[�J�����W����Ɏ擾
                //�p�x��ݒ�
                secondLocalAngle.z = rotation;
                _muzzles[1].localEulerAngles = secondLocalAngle;//��]����
                //�e���}�Y���̌����ɍ��킹�Ēe�𔭎�
                ObjectPool.Instance.UseObject(_muzzles[1].position, _secondBullet).transform.rotation = _muzzles[1].rotation;
            }

            yield return new WaitForSeconds(_attackInterval);
        }
    }

}
