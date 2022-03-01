using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveRandom : BossMoveAction
{
    /// <summary>�����A������</summary>
    float _horizontal = 0f;
    /// <summary>�����A�c����</summary>
    float _veritical = 0f;
    /// <summary>����</summary>
    Vector2 _dir;
    /// <summary>��~����</summary>
    [SerializeField,Header("��~����")] float _stopTime = 2f;
    /// <summary>�ړ�����</summary>
    [SerializeField,Header("�ړ�����")] float _moveTime = 0.5f;
    /// <summary>�E��</summary>
    [SerializeField,Header("�E��")] float _rightLimit = 4f;
    /// <summary>����</summary>
    [SerializeField,Header("����")] float _leftLimit = -4f;
    /// <summary>���</summary>
    [SerializeField,Header("���")] float _upperLimit = 2.5f;
    /// <summary>����</summary>
    [SerializeField,Header("����")] float _lowerLimit = 1.5f;           
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

    public override void Enter(BossController contlloer)
    {
        StartCoroutine(RandomMovement(contlloer)) ;
    }

    public override void ManagedUpdate(BossController contlloer)
    {       
        //�E�Ɉړ�������E������
        if(contlloer.Rb.velocity.x > MIDDLE_POS)
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
    /// �����_�������ɓ���
    /// </summary>
    IEnumerator RandomMovement(BossController controller)
    {       
        while (true)
        {
            //��莞�Ԏ~�܂�
            controller.Rb.velocity = Vector2.zero;
            yield return new WaitForSeconds(_stopTime);

            //�ꏊ�ɂ���Ĉړ��ł��鍶�E�����𐧌�����
            if (controller.transform.position.x > _rightLimit)         //�E�Ɉړ�����������
            {
                _horizontal = Random.Range(LEFT_DIR, NO_DIR);//���ړ��\
            }
            else if(controller.transform.position.x < _leftLimit)   //���Ɉړ���������
            {
                _horizontal = Random.Range(NO_DIR, RIGHT_DIR);//�E�ړ��\
            }
            else�@�@�@�@�@�@�@�@�@�@�@�@         //���E�ǂ����ɂ��ړ��������ĂȂ��Ȃ�
            {
                _horizontal = Random.Range(LEFT_DIR, RIGHT_DIR);//���R�ɍ��E�ړ��\          
            }

            //�ꏊ�ɂ���Ĉړ��ł���㉺�����𐧌�����
            if(controller.transform.position.y > _upperLimit)      //��Ɉړ�����������
            {
                _veritical = Random.Range(DOWN_DIR, NO_DIR);//���ړ��\
            }
            else if (controller.transform.position.y < _lowerLimit)//���Ɉړ�����������
            {
                _veritical = Random.Range(NO_DIR, UP_DIR);//��ړ��\
            }
            else�@�@�@�@�@�@�@�@�@�@�@�@�@�@      //�㉺�ǂ����ɂ��ړ��������ĂȂ��Ȃ�
            {
                _veritical = Random.Range(DOWN_DIR, UP_DIR);//���R�ɏ㉺�ړ��\
            }

            _dir = new Vector2(_horizontal, _veritical);//�����_���Ɉړ�
            //��莞�Ԉړ�����
            controller.Rb.velocity = _dir.normalized * controller.Speed;
            yield return new WaitForSeconds(_moveTime);
            
            Debug.Log("x" + _horizontal);
            Debug.Log("y" + _veritical);
        }
    }
}
