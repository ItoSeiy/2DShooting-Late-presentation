using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMove : BossMoveAction
{
    /// <summary>�����A������</summary>
    float _horizontal = 0f;
    /// <summary>�����A�c����</summary>
    float _veritical = 0f;
    /// <summary>����</summary>
    Vector2 _dir;
    /// <summary>��~����</summary>
    [SerializeField, Header("��~����")] float _stopTime = 2f;
    /// <summary>�ړ�����</summary>
    [SerializeField, Header("�ړ�����")] float _moveTime = 0.5f;
    /// <summary>�E��</summary>
    [SerializeField, Header("�E��")] float _rightLimit = 4f;
    /// <summary>����</summary>
    [SerializeField, Header("����")] float _leftLimit = -4f;
    /// <summary>���</summary>
    [SerializeField, Header("���")] float _upperLimit = 2.5f;
    /// <summary>����</summary>
    [SerializeField, Header("����")] float _lowerLimit = 1.5f;
    /// <summary>������</summary>
    const float LEFT_DIR = -1f;
    /// <summary>�E����</summary>
    const float RIGHT_DIR = 1f;
    /// <summary>�����</summary>
    const float UP_DIR = 1f;
    /// <summary>������</summary>
    const float DOWN_DIR = -1f;
    /// <summary>�����Ȃ�</summary>
    const float NO_DIR = 0f;
    /// <summary>�����ʒu</summary>
    const float MIDDLE_POS = 0f;
    /// <summary>����񐔂̐���</summary>
    const float JUDGMENT_TIME = 1 / 60f;

    public override void Enter(BossController contlloer)
    {
        StartCoroutine(Test(contlloer));
    }

    public override void Exit(BossController contlloer)
    {
        //���̕����Ɉړ�
        contlloer.Rb.velocity = _dir * contlloer.Speed;

        //�E�Ɉړ�������E������
        if (contlloer.Rb.velocity.x > MIDDLE_POS)
        {
            contlloer.Sprite.flipX = true;
        }
        //���Ɉړ������獶������
        else if (contlloer.Rb.velocity.x < MIDDLE_POS)
        {
            contlloer.Sprite.flipX = false;
        }
    }

    public override void ManagedUpdate(BossController contlloer)
    {
        StopAllCoroutines();
    }

    IEnumerator Test(BossController controller)
    {
        while(true)
        {
            //�v���C���[�̕����Ɉړ�
            _dir = (GameManager.Instance.Player.transform.position - controller.transform.position).normalized;
            yield return new WaitForSeconds(JUDGMENT_TIME);
        }
    }
}
