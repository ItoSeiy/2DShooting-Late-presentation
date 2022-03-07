using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class ItemBase : MonoBehaviour, IPauseable
{
    Rigidbody2D _rb;

    [SerializeField, Header("�v���C���[�̃^�O")] string _playerTag = "Player";
    [SerializeField, Header("�A�C�e�����������R���C�_�[�̃^�O")] string _itemGetColiderTag = "PlayerTrigger";

    [SerializeField, Header("�A�C�e��������C���Ƀv���C���[���ڐG�����Ƃ��̃A�C�e��������̃A�C�e���̑��x")] float _itemSpeed = 10f;

    [SerializeField, Header("�Đ����鉉�o")] GameObject _childrenPS = default;

    [SerializeField, Header("���o���Đ������^�C�~���O")] StartPS _stratPS = StartPS.Contact;

    bool _isGetItemMode = false;
    public bool _isTaking = false;

    Vector2 _oldVerocity;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        PauseManager.Instance.SetEvent(this);
        _isTaking = false;
        if (_stratPS == StartPS.FirstTime)
        {
            _childrenPS.SetActive(true);
        }
    }

    private void OnDisable()
    {
        PauseManager.Instance.RemoveEvent(this);
        _isGetItemMode = false;
    }

    private void Update()
    {
        if (PauseManager.Instance.PauseFlg == true) return;

        switch (_isGetItemMode)
        {
            case true:
                if (_stratPS == StartPS.Contact)
                {
                    _childrenPS.SetActive(true);
                }
                var dir = GameManager.Instance.Player.transform.position - this.gameObject.transform.position;
                _rb.velocity = dir.normalized * _itemSpeed;
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
            ItemGet();
        }
    }

    public void ItemGet()
    {
        _isGetItemMode = true;
    }

    void IPauseable.PauseResume(bool isPause)
    {
        if(isPause)
        {
            _oldVerocity = _rb.velocity;
            _rb.velocity = Vector2.zero;
            _rb.Sleep();
        }
        else
        {
            _rb.velocity = _oldVerocity;
            _rb.WakeUp();
        }
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