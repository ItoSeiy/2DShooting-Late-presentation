using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyBulletDelayFollow : BulletBese
{
    /// <summary>�^�[�Q�b�g��GameObject</summary>
    GameObject _opponen;
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
        _opponen = GameObject.FindWithTag(OpponenTag);//Player��Tag���Ƃ��Ă���
        base.OnEnable();
    }

    protected override void BulletMove()
    {
        _timer += Time.deltaTime;//�^�C�}�[

        //���ԂɂȂ�����
        if (_timer >= _delayChangeDirTime + DELAY_CHANGE_DIR_TIME_OFFSET) return;

        if(_timer < _delayChangeDirTime && _opponen)//���ԂɂȂ�܂ł�
        {
            //�}�Y���̌����ɍ��킹�������Ɉړ�
            Rb.velocity = gameObject.transform.rotation * new Vector3(0, Speed, 0);
        }

        else if (_timer >= _delayChangeDirTime && _opponen)//�������ԂɂȂ�����
        {
            //��]�������������v�Z
            Vector2 dir = transform.position - _opponen.transform.position;
            //��]�����������ɉ�]
            transform.rotation = Quaternion.FromToRotation(Vector3.down, dir);
            //��]���������Ɉړ�
            Rb.velocity = gameObject.transform.rotation * new Vector3(0, Speed, 0);
        }
        else//���ԊO�J���ɂȂ�����
        {
            //�v���C���[�������������������Ɉړ�
            Rb.velocity = _oldDir.normalized * Speed;
        }
    }
}
