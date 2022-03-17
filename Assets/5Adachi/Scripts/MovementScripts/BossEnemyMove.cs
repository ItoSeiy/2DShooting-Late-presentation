using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMove : BossMoveAction
{
    /// <summary>�^�C�}�[</summary>
    float _timer = 0f;
    bool _speedUp = false;
    /// <summary>����</summary>
    Vector2 _dir;
    /// <summary>��~����</summary>
    [SerializeField, Header("��~����")] float _stopTime = 2f;
    /// <summary>�ړ�����</summary>
    [SerializeField, Header("�_�b�V������")] float _moveTime = 0.5f;
    /// <summary>�����ʒu</summary>
    const float MIDDLE_POS = 0f;
    /// <summary>����񐔂̐���</summary>
    const float JUDGMENT_TIME = 1 / 60f;

    public override void Enter(BossController contlloer)
    {
        _speedUp = false;
        StartCoroutine(Test(contlloer));
    }

    public override void ManagedUpdate(BossController contlloer)
    {
        //���̕����Ɉړ�
        if (_speedUp)
        {
            contlloer.Rb.velocity = _dir * (contlloer.Speed * 2f);
        }
        else
        {
            contlloer.Rb.velocity = _dir * contlloer.Speed * 0.5f;
        }
        
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
            yield return new WaitForSeconds(JUDGMENT_TIME);

            //�v���C���[�̕����Ɉړ�
            _dir = new Vector2(GameManager.Instance.Player.transform.position.x - controller.transform.position.x, GameManager.Instance.Player.transform.position.y - controller.transform.position.y).normalized;
            if(_timer >= 2f)
            {
                //�_�b�V������
                _speedUp = true;
                yield return new WaitForSeconds(0.5f);
                //��~����
                _dir = Vector2.zero;
                yield return new WaitForSeconds(1f);
                _speedUp = false;
                _timer = 0;
            }
        }
    }
}
