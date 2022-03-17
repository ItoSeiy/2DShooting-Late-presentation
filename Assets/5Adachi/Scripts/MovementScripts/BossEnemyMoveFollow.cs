using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveFollow : BossMoveAction
{
    /// <summary>�^�C�}�[</summary>
    float _timer = 0f;
    /// <summary>�X�s�[�h�A�b�v���邽�߂̐؂�ւ��X�C�b�`</summary>
    bool _speedUp = false;
    /// <summary>����</summary>
    Vector2 _dir;
    /// <summary>�X�s�[�h�A�b�v����^�C�~���O</summary>
    [SerializeField, Header("�X�s�[�h�A�b�v����^�C�~���O")] float _speedUpInterval = 0.5f;
    /// <summary>��~����</summary>
    [SerializeField, Header("��~����")] float _stopTime = 1f;
    /// <summary>�ړ�����</summary>
    [SerializeField, Header("�_�b�V������")] float _speedUpTime = 0.5f;
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
            contlloer.Rb.velocity = _dir.normalized * contlloer.Speed * 2f;
        }
        else
        {
            contlloer.Rb.velocity = _dir.normalized * contlloer.Speed * 0.5f;
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
            _dir = new Vector2(GameManager.Instance.Player.transform.position.x - controller.transform.position.x, GameManager.Instance.Player.transform.position.y - controller.transform.position.y);
            if(_timer >= _speedUpInterval)
            {
                //�_�b�V������
                _speedUp = true;
                yield return new WaitForSeconds(_speedUpTime);
                //��~����
                _dir = Vector2.zero;
                yield return new WaitForSeconds(_stopTime);
                //���Z�b�g
                _speedUp = false;
                _timer = 0;
            }
        }
    }
}
