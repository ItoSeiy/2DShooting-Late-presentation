using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Overdose.Calculation;

public class BossNormalAttack02 : BossAttackAction
{
    /// <summary>����</summary>
    Vector3 _dir;
    /// <summary>�e�̌����ڂ̎��</summary>
    int _pattern = 0;
    /// <summary>�^�C�}�[</summary>
    float _timer = 0f;
    /// <summary>�o���b�g�𔭎˂���|�W�V����</summary>
    [SerializeField, Header("Bullet�𔭎˂���|�W�V����")] Transform[] _muzzles = null;
    /// <summary>�U���p�x</summary>
    [SerializeField, Header("�U���p�x(�b)")] private float _attackInterval = 0.2f;
    /// <summary>���˂���e��ݒ�ł���</summary>
    [SerializeField, Header("���˂���e�̐ݒ�")] PoolObjectType[] _firstBullet;
    /// <summary>���˂���e��ݒ�ł���</summary>
    [SerializeField, Header("���˂���e�̐ݒ�(��Ɠ�������ݒ肵�Ă�������")] PoolObjectType[] _secondBullet;
    /// <summary>���̍s������o�鎞��</summary>
    [SerializeField, Header("���̍s������o�鎞��")] float _endingTime = 20f;
    /// <summary>�A�C�e���𗎂Ƃ��m��</summary>
    [SerializeField, Header("�A�C�e���𗎂Ƃ��m��")] int _probability = 50;
    /// <summary>�΍��p</summary>
    const float OPPOSITE_ANGLE = 180f;
    

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
            //�e�̌����ڂ������_���ŕς���
            _pattern = Random.Range(0, _firstBullet.Length);
            
            ///�v���C���[�̌����Ƀ}�Y��������///

            //�^�[�Q�b�g�i�v���C���[�j�̕������v�Z
            _dir = (GameManager.Instance.Player.transform.position - _muzzles[0].transform.position);
            //�^�[�Q�b�g�i�v���C���[�j�̕����ɉ�]
            _muzzles[0].transform.rotation = Quaternion.FromToRotation(Vector3.up, _dir);

            //�e���}�Y��0�̌����ɍ��킹�Ēe�𔭎�
            ObjectPool.Instance.UseObject(_muzzles[0].position, _firstBullet[_pattern]).transform.rotation = _muzzles[0].rotation;

            ///�v���C���[����������Ƌt(X���t����)�Ƀ}�Y��������///
            //�}�Y������]����
            Vector3 localAngle = _muzzles[0].localEulerAngles;// ���[�J�����W����Ɏ擾           
            localAngle.z = -localAngle.z;//�������t���ɂ���
            _muzzles[0].localEulerAngles = localAngle;//��]����
            //�e���}�Y���̌����ɍ��킹�Ēe�𔭎�
            ObjectPool.Instance.UseObject(_muzzles[0].position, _secondBullet[_pattern/**/]).transform.rotation = _muzzles[0].rotation;

            ///�v���C���[����������Ƌt(X��Y���t�����j///
            _dir = (GameManager.Instance.Player.transform.position - _muzzles[0].transform.position);
            //�^�[�Q�b�g�i�v���C���[�j�̕����ɉ�]
            _muzzles[0].transform.rotation = Quaternion.FromToRotation(Vector3.up, -_dir);

            //�e���}�Y��0�̌����ɍ��킹�Ēe�𔭎�
            ObjectPool.Instance.UseObject(_muzzles[0].position, _secondBullet[_pattern/**/]).transform.rotation = _muzzles[0].rotation;

            ///�v���C���[����������Ƌt(Y���t����///
            //�}�Y������]����  
            localAngle.z = -localAngle.z + OPPOSITE_ANGLE;//�������t���ɂ���
            _muzzles[0].localEulerAngles = -localAngle;//��]����
            //�e���}�Y���̌����ɍ��킹�Ēe�𔭎�
            ObjectPool.Instance.UseObject(_muzzles[0].position, _firstBullet[_pattern]).transform.rotation = _muzzles[0].rotation;

            yield return new WaitForSeconds(_attackInterval);//�U���p�x(�b)
        }
    }

}
