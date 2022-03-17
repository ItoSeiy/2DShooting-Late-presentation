using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMove : BossMoveAction
{
    /// <summary>�^�C�}�[</summary>
    float _timer = 0f;
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
    /// <summary>�����ʒu</summary>
    const float MIDDLE_POS = 0f;
    /// <summary>����񐔂̐���</summary>
    const float JUDGMENT_TIME = 1 / 60f;

    public override void Enter(BossController contlloer)
    {
        Debug.Log("��������");
        StartCoroutine(Test(contlloer));
    }

    public override void ManagedUpdate(BossController contlloer)
    {
        //���̕����Ɉړ�
        contlloer.Rb.velocity = _dir * contlloer.Speed / 2f;
        //�^�C�}�[
        _timer += Time.deltaTime;

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

    public override void Exit(BossController contlloer)
    {
        StopAllCoroutines();
    }

    IEnumerator Test(BossController controller)
    {
        _timer = 0;

        while (true)
        {
            Debug.Log("a");
            //�v���C���[�̕����Ɉړ�
            _dir = new Vector2(GameManager.Instance.Player.transform.position.x - controller.transform.position.x, GameManager.Instance.Player.transform.position.y - controller.transform.position.y).normalized;
            //yield return new WaitForSeconds(1f);

            if (_timer >= 10f)
            {
                break;
            }
        }
        _dir = Vector2.zero;
        yield break;
    }
}
