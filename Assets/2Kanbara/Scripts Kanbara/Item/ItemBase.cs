using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class ItemBase : MonoBehaviour
{
    [SerializeField, Header("�v���C���[�̃^�O")] string _playerTag = "Player";
    [SerializeField, Header("�A�C�e�����������R���C�_�[�̃^�O")] string _itemGetColiderTag = "PlayerTrigger";

    [SerializeField, Header("�Đ����鉉�o")] GameObject _childrenPS = default;

    [SerializeField, Header("���o���Đ������^�C�~���O")] StartPS _stratPS = StartPS.Contact;

    bool _isTaking = false;

    public bool IsTaking => _isTaking;

    private void OnEnable()
    {
        if (_stratPS == StartPS.FirstTime)
        {
            _childrenPS.SetActive(true);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == _playerTag)//�v���C���[�ɐڐG������
        {
            _childrenPS.SetActive(false);
            Destroy(this.gameObject);
        }
        if(collision.tag == _itemGetColiderTag)
        {
            _isTaking = true;
        }
    }

    protected virtual void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    /// <summary>
    /// ���o���Đ�������^�C�~���O
    /// </summary>
    enum StartPS
    {
        /// <summary>�ŏ�</summary>
        FirstTime,
        /// <summary>�ڐG��</summary>
        Contact
    }
}