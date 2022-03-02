using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveUShaped : BossMoveAction
{
    /// <summary>����</summary>
    Vector2 _dir;
    /// <summary>�E��</summary>
    [SerializeField, Header("�E��")] float _rightLimit = 7.5f;
    /// <summary>����</summary>
    [SerializeField, Header("����")] float _leftLimit = -7.5f;
    /// <summary>���</summary>
    [SerializeField, Header("���")] float _upperLimit = 3.5f;
    /// <summary>����</summary>
    [SerializeField, Header("����")] float _lowerLimit = -3f;
    /// <summary>�ŒZ�ړ�����</summary>
    [SerializeField, Header("�ŒZ�ړ�����")] float _shortMoveTime = 1f;
    /// <summary>�Œ��ړ�����</summary>
    [SerializeField, Header("�Œ��ړ�����")] float _longMoveTime = 3f;
    /// <summary>��~����</summary>
    [SerializeField,Header("��~����")] float _stopTime = 1f;
    /// <summary>�����ʒu</summary>
    const float MIDDLE_POS = 0;
    /// <summary>����񐔂̐���</summary>
    const float JUDGMENT_TIME = 1/60f;
    /// <summary>�����x�炷</summary>
    const float DELAY_TIME = 1f;
    /// <summary>�����Ȃ�</summary>
    const float NO_DIR = 0f;


    public override void Enter(BossController contlloer)
    {
        StartCoroutine(UShaped(contlloer));
    }

    public override void ManagedUpdate(BossController contlloer)
    {
        contlloer.Rb.velocity = _dir.normalized * contlloer.Speed;//�����ɃX�s�[�h��������

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

    /// <summary>
    /// U���^�̂悤�Ȉړ�������
    /// </summary>
    public IEnumerator UShaped(BossController controller)
    {
        _dir = new Vector2(-controller.transform.position.x, NO_DIR);

        //�{�X�̃|�W�V����X��0��������_�������ċl�ނ̂�
        if (controller.transform.position.x == 0f)
        {
            _dir = Vector2.right;//�E�Ɉړ�
        }

        while(true)//�[�ɒ����܂ŉ��ɓ���
        {           
            //���Α��ɒ�������
            if ((controller.transform.position.x <= _leftLimit) || (controller.transform.position.x >= _rightLimit))
            {
                Debug.Log("c");
                _dir = Vector2.zero;//��~
                yield return new WaitForSeconds(_stopTime);//��~����
                _dir = Vector2.down;//������
                break;
            }
            yield return new WaitForSeconds(JUDGMENT_TIME);//����񐔂̐���
        }

        while (true)//���Α��ɒ����܂ňړ�����
        {           
            if(controller.transform.position.y <= _lowerLimit)
            {

                Debug.Log("f");
                _dir = Vector2.zero;//��~
                yield return new WaitForSeconds(_stopTime);//��~����
                _dir = new Vector2(-controller.transform.position.x, NO_DIR);//��ʉ��[�ɂ����獡����ꏊ�̔��Α��ɉ��ړ�
                break;
            }
            yield return new WaitForSeconds(JUDGMENT_TIME);//����񐔂̐���
        }

        yield return new WaitForSeconds(DELAY_TIME);//�����x�炷

        while (true)//���Α��ɒ����܂ŉ��ړ�����
        {          
            //���Α��ɂ������ɍs��
            if (controller.transform.position.x >= _rightLimit || controller.transform.position.x <= _leftLimit)
            {
                Debug.Log("g");
                _dir = Vector2.zero;//��~
                yield return new WaitForSeconds(_stopTime);//��~����
                _dir = Vector2.up;//��ɏオ��
                break;
            }
            yield return new WaitForSeconds(JUDGMENT_TIME);//����񐔂̐���
        }

        while (true)//������x��ɂ����܂ňړ�����
        {
            yield return new WaitForSeconds(JUDGMENT_TIME);

            if (controller.transform.position.y >= _upperLimit)//������x��ɂ�������
            {
                _dir = Vector2.zero;//��~
                yield return new WaitForSeconds(_stopTime);//��~����
                Debug.Log("h");

                if (controller.transform.position.x < MIDDLE_POS)//���ɂ�����
                {                   
                    _dir = Vector2.right;//�E�ɍs��
                    Debug.Log("a");
                    break;
                }
                else//�E�ɂ�����
                {    
                    _dir = Vector2.left;//���ɍs��
                    Debug.Log("b");
                    break;
                }
            }
        }
        
        yield return new WaitForSeconds(Random.Range(_shortMoveTime, _longMoveTime)); //�ړ�����(�����_��)    
        _dir = Vector2.zero;//��~
        yield break;
    }

}
