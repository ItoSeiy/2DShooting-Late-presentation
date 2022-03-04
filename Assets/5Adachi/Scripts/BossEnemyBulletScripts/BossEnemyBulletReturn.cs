using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyBulletReturn : BulletBese
{
    /// <summary>Boss��GameObject</summary>
    GameObject _bossEnemy;
    /// <summary>Boss����������</summary>
    Vector2 _oldDir = Vector2.down;
    /// <summary>�{�X�̃^�O</summary>
    [SerializeField,Header("BossEnemy��Tag")] string _bossEnemyTag = null;
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
    /// <summary>�c���̏�����</summary>
    bool _verticalLimit;
    /// <summary>�{�X�̕����Ɉ�u�Ǐ]���邱�Ƃ��\���ǂ���(1���)</summary>
    bool _firstReturn = false;
    /// <summary>�{�X�̕����Ɉ�u�Ǐ]���邱�Ƃ��\���ǂ���(2���)</summary>
    bool _secondReturn = false;
    /// <summary>�^�C�}�[</summary>
    float _timer = 0f;
    /// <summary>�{�X�̕����Ɉ�u�Ǐ]���鎞��</summary>
    float _firstReturnTime = 0.5f;
    /// <summary>�{�X�̕����Ɉ�u�Ǐ]���鎞��</summary>
    float _secondReturnTime = 1.2f;
    /// <summary>�Ⴊ�����鎞��</summary>
    float _snowmeltTime = 4f;

    protected override void OnEnable()
    {
        _timer = 0f;//�^�C�}�[�����Z�b�g
        _bossEnemy = GameObject.FindWithTag(_bossEnemyTag);//Boss��Tag���Ƃ��Ă���
        base.OnEnable();
        _firstReturn = true;//�{�X�̕����Ɉ�u�Ǐ]���邱�Ƃ��\
        _secondReturn = true;//�{�X�̕����Ɉ�u�Ǐ]���邱�Ƃ��\
    }

    protected override void BulletMove()
    {
        _timer += Time.deltaTime;//�^�C�}�[
       
        if (_timer >= _firstReturnTime && _firstReturn)//�{�X�̕����Ɉ�u�Ǐ]���鎞�ԂɂȂ�����(1���)
        {           
            Vector2 dir = _bossEnemy.transform.position - transform.position;//�v���C���[�̕������v�Z           
            dir = dir.normalized * Speed;//���x���ς��Ȃ��悤�ɂ��A�X�s�[�h��������           
            Rb.velocity = dir;//������ς���            
            _oldDir = dir;//������ۑ�            
            _firstReturn = false;//�g���Ȃ��悤�ɂ���
        }
        
        else if (_timer >= _secondReturnTime && _secondReturn)//�{�X�̕����Ɉ�u�Ǐ]���鎞�ԂɂȂ�����(2���)
        {            
            Vector2 dir = _bossEnemy.transform.position - transform.position;//�v���C���[�̕������v�Z            
            dir = dir.normalized * Speed;//���x���ς��Ȃ��悤�ɂ��A�X�s�[�h��������            
            Rb.velocity = dir;//������ς���         
            _oldDir = dir;//������ۑ�            
            _secondReturn = false;//�g���Ȃ��悤�ɂ���
        }      
        
        else if (_timer >= _snowmeltTime && !_firstReturn && !_secondReturn)//�Ⴊ�����鎞�ԂɂȂ�����
        {            
            this.gameObject.SetActive(false);//�e��������
        }
        
        else if (!_firstReturn && !_secondReturn)//�Ⴊ�����鎞�ԂɂȂ�O��������
        {         
            Rb.velocity = _oldDir.normalized * Speed;//���݂̕����Ɉړ�
        }

        else
        {
            Rb.velocity = gameObject.transform.rotation * new Vector3(0, Speed, 0);//�}�Y���̌����ɍ��킹�Ĉړ�
        }
        
    }
}
