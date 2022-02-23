using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyBulletReboundFollow : BulletBese
{
    /// <summary>Boss��GameObject</summary>
    GameObject _bossEnemy;
    /// <summary>�^�C�}�[</summary>
    float _timer;
    /// <summary>Player����������</summary>
    Vector2 _oldDir = Vector2.down;
    /// <summary>��莞�Ԓx�ꂽ��v���C���[�ɕ�����ς���(�b)</summary>
    [SerializeField, Header("��莞�Ԓx�ꂽ��v���C���[����������ɕς���(�b)")] float _delayChangeDirTime = 2f;
    /// <summary>���Ԃ̏C���l</summary>
    const float DELAY_CHANGE_DIR_TIME_OFFSET = 0.1f;
    /// <summary>�E��</summary>
    [SerializeField, Header("�E��")] float _rightLimit = 7.5f;
    /// <summary>����</summary>
    [SerializeField, Header("����")] float _leftLimit = -7.5f;
    /// <summary>���</summary>
    [SerializeField,Header("���")] float _upperLimit = 4f;
    /// <summary>����</summary>
    [SerializeField, Header("����")] float _downLimit = -4f;
    /// <summary>�����̏�����</summary>
    bool _horizontalLimit;
    /// <summary></summary>
    bool _verticalLimit;
    bool _rebound = true;
    protected override void OnEnable()
    {
        _bossEnemy = GameObject.FindWithTag(PlayerTag);//Boss��Tag���Ƃ��Ă���
        base.OnEnable();
    }

    protected override void BulletMove()
    {
        //�����̏�����
        _horizontalLimit = transform.position.x >= _rightLimit || transform.position.x <= _leftLimit;
        //�c���̏�����
        _verticalLimit = transform.position.y >= _upperLimit || transform.position.y <= _downLimit;
        //�����g���ĂȂ�������
        if ((_horizontalLimit || _verticalLimit) && _rebound)
        {
            //�v���C���[�̕������v�Z
            Vector2 dir = _bossEnemy.transform.position - transform.position;
            //���x���ς��Ȃ��悤�ɂ��A�X�s�[�h��������
            dir = dir.normalized * Speed;
            //������ς���
            Rb.velocity = dir;
            //������ۑ�
            _oldDir = dir;
        }
        else if(!_rebound)
        {
            Rb.velocity = _oldDir.normalized * Speed;
        }
        else
        {
            Rb.velocity = gameObject.transform.rotation * new Vector3(0, Speed, 0);//�}�Y���̌����ɍ��킹�Ĉړ�
        }
    }
}
