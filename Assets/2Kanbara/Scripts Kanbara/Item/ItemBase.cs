using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class ItemBase : MonoBehaviour
{
    Rigidbody2D _rb;

    [SerializeField, Header("�v���C���[�̃^�O")] string _playerTag = "Player";
    [SerializeField, Header("�A�C�e�����������R���C�_�[�̃^�O")] string _itemGetColiderTag = "PlayerTrigger";

    [SerializeField, Header("�A�C�e��������C���Ƀv���C���[���ڐG�����Ƃ��̃A�C�e��������̃A�C�e���̑��x")] float _itemSpeed = 10f;

    [SerializeField, Header("�Đ����鉉�o")] GameObject _childrenPS = default;

    [SerializeField, Header("���o���Đ������^�C�~���O")] StartPS _stratPS = StartPS.Contact;

    bool _isGetItemMode = false;
    public bool _isTaking = false;

    private void OnEnable()
    {
        _isTaking = false;
        if (_stratPS == StartPS.FirstTime)
        {
            _childrenPS.SetActive(true);
        }
    }

    private void OnDisable()
    {
        _isGetItemMode = false;
    }

    private void Update()
    {
        switch (_isGetItemMode)
        {
            case true:
                if (_stratPS == StartPS.Contact)
                {
                    _childrenPS.SetActive(true);
                }
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                var playerRb = GameObject.FindWithTag(_playerTag);
                var dir = playerRb.transform.position - this.gameObject.transform.position;
                rb.velocity = dir.normalized * _itemSpeed;
                break;
            case false:
                break;
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == _playerTag || collision.tag == "Finish")//�v���C���[�ɐڐG������
        {
            _childrenPS.SetActive(false);
            gameObject.SetActive(false);
        }
        if(collision.tag == _itemGetColiderTag)
        {
            if(_stratPS == StartPS.Contact)
            {
                _childrenPS.SetActive(true);
            }
        }
    }

    public void OnItemGetLine()
    {
        _isGetItemMode = true;
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