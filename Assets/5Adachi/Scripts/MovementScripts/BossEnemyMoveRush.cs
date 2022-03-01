using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveRush : BossMoveAction
{
    /// <summary>�v���C���[�̃I�u�W�F�N�g</summary>
    GameObject _player;
    /// <summary>����</summary>
    Vector2 _dir;
    /// <summary>�^�C�}�[</summary>
    float _time = 0f;
    /// <summary>�v���C���[�̃^�O</summary>
    [SerializeField,Header("�v���C���[�̃^�O")] private string _playerTag = null;
    /// <summary>���</summary>
    [SerializeField, Header("���")] float _upperLimit = 3f;
    /// <summary>����</summary>
    [SerializeField, Header("����")] float _lowerLimit = -3f;
    /// <summary>��~����</summary>
    [SerializeField, Header("�~�肽��̒�~����")] float _stopTime = 2f;
    /// <summary>��ɑ؍݂��鎞�ԁA�ǔ�����</summary>
    [SerializeField,Header("�ǔ�����(��ɑ؍݂��Ă��鎞��)")] float _stayingTime = 4f;    
    /// <summary>����񐔂̐���</summary>
    const float JUDGMENT_TIME = 1/60f;
    /// <summary>�C���l</summary>
    const float PLAYER_POSTION_OFFSET = 0.5f;
    /// <summary>�����Ȃ�</summary>
    const float NO_DIR = 0f;
    /// <summary>�����ʒu</summary>
    const float MIDDLE_POS = 0f;

    public override void Enter(BossController contlloer)
    {
        _player = GameObject.FindGameObjectWithTag(_playerTag);
        StartCoroutine(Rush(contlloer));
    }

    public override void ManagedUpdate(BossController contlloer)
    {
        contlloer.Rb.velocity = _dir * contlloer.Speed;
        _time += Time.deltaTime;

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
        
    }

    /// <summary>
    /// ��莞�ԃv���C���[�����b�N�I���������Ɛ^���ɃT�K��B���̌��ɏオ��B
    /// </summary>
    IEnumerator Rush(BossController controller)        
    {
        _time = 0;

        //x���W�����v���C���[�̋߂��Ɉړ�����
        while (true)
        {
            yield return new WaitForSeconds(JUDGMENT_TIME);
            
            //�v���C���[���E�ɂ�����
            if (_player.transform.position.x > controller.transform.position.x + PLAYER_POSTION_OFFSET || _player.transform.position.x < controller.transform.position.x - PLAYER_POSTION_OFFSET)
            {
                Debug.Log("right");
                _dir = new Vector2(_player.transform.position.x - controller.transform.position.x, NO_DIR).normalized;
            }
            else//�v���C���[��x���W���߂�������
            {
                Debug.Log("stop");
                _dir = new Vector2(_player.transform.position.x - controller.transform.position.x, NO_DIR);
            }
            //���E�ɒB������
            if ( _time >= _stayingTime)
            {
                break;
            }
        }

        while (true)//�T�K��
        {
            yield return new WaitForSeconds(JUDGMENT_TIME);
            _dir = Vector2.down;//�T�K��

            if (controller.transform.position.y <= _lowerLimit)//�T�K������
            {
                _dir = Vector2.zero;//��~
                yield return new WaitForSeconds(_stopTime);//��~����
                _dir = Vector2.up;//�オ��
                break;
            }
        }       
        
        while (true)//���̏ꏊ�܂ŏオ��
        {
            yield return new WaitForSeconds(JUDGMENT_TIME);
            
            if (_upperLimit <= controller.transform.position.y)//���̏ꏊ�܂ł�����
            {
                Debug.Log("���X�g");
                _dir = Vector2.zero;//��~
                yield return new WaitForSeconds(_stopTime);//��~����
                break;
            }            
        }
        yield break;
    }

}
