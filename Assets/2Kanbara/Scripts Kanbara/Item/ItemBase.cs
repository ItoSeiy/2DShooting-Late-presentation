using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class ItemBase : MonoBehaviour
{
    [SerializeField, Header("�A�C�e�����v���C���[�ɋ߂Â����x")] float _itemSpeed = 10f;
    [SerializeField, Header("�v���C���[���A�C�e��������C���ɐG�ꂽ�Ƃ��ɃA�C�e�����v���C���[�ɋ߂Â����x")] float _getItemSpeed = 100f;

    [SerializeField, Header("�v���C���[�̃^�O")] string _playerTag = "Player";
    [SerializeField, Header("�v���C���[�̎��A�C�e������p�̃R���C�_�[")] string _playerTriggerTag = "PlayerTrigger";

    [SerializeField, Header("�Đ����鉉�o")] GameObject _childrenPS = default;

    [SerializeField, Header("���o���Đ������^�C�~���O")] StartPS _stratPS = StartPS.Contact;

    Rigidbody2D _rb;
    GameObject _player;
    PlayerBase _playerBase;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == _playerTag)//�v���C���[�ɐڐG������
        {
            _childrenPS.SetActive(false);
            Destroy(this.gameObject);
        }
        if (collision.tag == _playerTriggerTag)//�A�C�e������R���C�_�[�ɐڐG������
        {
            _player = GameObject.FindWithTag("Player");
            _playerBase = _player.GetComponent<PlayerBase>();
            if (_stratPS == StartPS.Contact)
            {
                _childrenPS.SetActive(true);
            }
            ApproachPlayer();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)//�g���K�[���Ƀv���C���[��������ǂ�������
    {
        ApproachPlayer();
    }

    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody2D>();
        if(_stratPS == StartPS.FirstTime)
        {
            _childrenPS.SetActive(true);
        }
    }

    protected virtual void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
    /// <summary>
    /// �v���C���[�ɋ߂Â��֐�
    /// </summary>
    public void ApproachPlayer()
    {
        var dir = _player.transform.position - this.gameObject.transform.position;
        _rb.velocity = dir.normalized * _itemSpeed;
    }

    void PlayerOnItemGetLine()
    {
        var dir = _player.transform.position - this.gameObject.transform.position;
        _rb.velocity = dir.normalized * _getItemSpeed;
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