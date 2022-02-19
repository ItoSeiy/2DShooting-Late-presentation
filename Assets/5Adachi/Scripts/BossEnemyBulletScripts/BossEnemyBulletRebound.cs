using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyBulletRebound : BulletBese
{
    /// <summary>�E��</summary>
    [SerializeField, Header("�E��")] float _rightLimit = 7.5f;
    /// <summary>����</summary>
    [SerializeField, Header("����")] float _leftLimit = -7.5f;
    bool _rebound = true;
    protected override void BulletMove()
    {
        //�e����ʒ[�܂ł�����P�񂾂����˕Ԃ�
        if ((transform.position.x <= _leftLimit || transform.position.x >= _rightLimit) && _rebound)
        {
            Vector3 localAngle = transform.localEulerAngles;// ���[�J�����W����Ɏ擾
            localAngle.z = -localAngle.z;// �p�x��ݒ�
            transform.localEulerAngles = localAngle;//��]����
            _rebound = false;//�Q��ڂ̓��o�E���h�ł��Ȃ��Ȃ�
        }
        else
        {
            Rb.velocity = gameObject.transform.rotation * new Vector3(0, Speed, 0);//�}�Y���̌����ɍ��킹�Ĉړ�
        }
    }
}
