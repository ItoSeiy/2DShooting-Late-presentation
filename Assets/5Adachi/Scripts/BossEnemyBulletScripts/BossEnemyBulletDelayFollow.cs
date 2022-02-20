using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyBulletDelayFollow : BulletBese
{
    /// <summary>Player��GameObject</summary>
    GameObject _player;
    /// <summary>�^�C�}�[</summary>
    float _timer;
    /// <summary>Player����������</summary>
    Vector2 _oldDir = Vector2.down;
    /// <summary>��莞�Ԓx�ꂽ��v���C���[�ɕ�����ς���(�b)</summary>
    [SerializeField, Header("��莞�Ԓx�ꂽ��v���C���[����������ɕς���(�b)")] float _delayChangeDirTime = 2f;
    /// <summary>���Ԃ̏C���l</summary>
    const float DELAY_CHANGE_DIR_TIME_OFFSET = 0.1f;

    protected override void OnEnable()
    {
        _timer = 0;//�^�C�}�[�����Z�b�g
        _player = GameObject.FindWithTag(PlayerTag);//Player��Tag���Ƃ��Ă���
        base.OnEnable();
    }

    protected override void BulletMove()
    {
        _timer += Time.deltaTime;//�^�C�}�[

        //���ԂɂȂ�����
        if (_timer >= _delayChangeDirTime + DELAY_CHANGE_DIR_TIME_OFFSET) return;

        if(_timer < _delayChangeDirTime && _player)//���ԂɂȂ�܂ł�
        {
            //�}�Y���̌����ɍ��킹�������Ɉړ�
            Rb.velocity = gameObject.transform.rotation * new Vector3(0, Speed, 0);
        }

        else if (_timer >= _delayChangeDirTime && _player)//�������ԂɂȂ�����
        {
            //�v���C���[�̕������v�Z
            Vector2 dir = _player.transform.position - transform.position;
            //���x���ς��Ȃ��悤�ɂ��A�X�s�[�h��������
            dir = dir.normalized * Speed;
            //������ς���
            Rb.velocity = dir;
            //������ۑ�
            _oldDir = dir;
        }
        else//���ԊO�J���ɂȂ�����
        {
            //�v���C���[�������������������Ɉړ�
            Rb.velocity = _oldDir.normalized * Speed;
        }
    }
}
