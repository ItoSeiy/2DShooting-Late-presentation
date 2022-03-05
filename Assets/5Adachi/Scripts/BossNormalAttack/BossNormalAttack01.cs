using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Overdose.Calculation;

public class BossNormalAttack01 : BossAttackAction
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
    [SerializeField, Header("�U���p�x(�b)")] float _attackInterval = 0.6f;
    /// <summary>���˂���e��ݒ�ł���</summary>
    [SerializeField, Header("���˂���e�̐ݒ�")] PoolObjectType[] _bullet;
    /// <summary>���̍s������o�鎞��</summary>
    [SerializeField,Header("���̍s������o�鎞��")] float _endingTime = 20f;
    /// <summary>�A�C�e���𗎂Ƃ��m��</summary>
    [SerializeField, Header("�A�C�e���𗎂Ƃ��m��")] int _probability = 50;

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
            _pattern = Random.Range(0, _bullet.Length);

            //�^�[�Q�b�g�i�v���C���[�j�̕������v�Z
            _dir = (GameManager.Instance.Player.transform.position - _muzzles[0].transform.position);
            //�^�[�Q�b�g�i�v���C���[�j�̕����ɉ�]
            _muzzles[0].transform.rotation = Quaternion.FromToRotation(Vector3.up, _dir);

            yield return new WaitForSeconds(1f);

            //�e�I�u�W�F�N�g�̃}�Y��

            //�e���}�Y��0�̌����ɍ��킹�Ēe�𔭎�
            ObjectPool.Instance.UseObject(_muzzles[0].position, _bullet[_pattern]).transform.rotation = _muzzles[0].rotation;

            //�q�I�u�W�F�N�g�̃}�Y��

            //�e���}�Y��1�̌����ɍ��킹�Ēe�𔭎ˁi�e�I�u�W�F�N�g�̒e���E���j
            ObjectPool.Instance.UseObject(_muzzles[1].position, _bullet[_pattern]).transform.rotation = _muzzles[1].rotation;

            //�e���}�Y��2�̌����ɍ��킹�Ēe�𔭎ˁi�e�I�u�W�F�N�g�̒e��荶���j
            ObjectPool.Instance.UseObject(_muzzles[2].position, _bullet[_pattern]).transform.rotation = _muzzles[2].rotation;

            //�e���}�Y��3�̌����ɍ��킹�Ēe�𔭎ˁi�e�I�u�W�F�N�g�̒e���E���j
            ObjectPool.Instance.UseObject(_muzzles[3].position, _bullet[_pattern]).transform.rotation = _muzzles[3].rotation;

            //�e���}�Y��4�̌����ɍ��킹�Ēe�𔭎ˁi�e�I�u�W�F�N�g�̒e��荶���j
            ObjectPool.Instance.UseObject(_muzzles[4].position, _bullet[_pattern]).transform.rotation = _muzzles[4].rotation;

            yield return new WaitForSeconds(_attackInterval);
        }
    }

}
