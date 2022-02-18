using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemGet : MonoBehaviour
{
    [SerializeField, Header("�A�C�e�����v���C���[�ɋ߂Â����x")] float _itemSpeed = 10f;

    [SerializeField, Header("�A�C�e���̃^�O")] string[] _itemTags = default;

    Rigidbody2D _itemObjectrb;
    Rigidbody2D _playerRb;
    private void OnEnable()
    {
        _playerRb = GetComponentInParent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var itemTag in _itemTags)
        {
            if (collision.tag == itemTag)//�A�C�e���ɐڐG������
            {
                if(_itemObjectrb == null)
                {
                    _itemObjectrb = collision.GetComponent<Rigidbody2D>();
                }
                ApproachPlayer();
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)//�g���K�[���Ƀv���C���[��������ǂ�������
    {
        foreach (var itemTag in _itemTags)
        {
            if (collision.tag == itemTag)
            {
                if (_itemObjectrb == null)
                {
                    _itemObjectrb = collision.GetComponent<Rigidbody2D>();
                }
                ApproachPlayer();
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        foreach(var itemTag in _itemTags)
        {
            if (collision.tag == itemTag)
            {
                if (_itemObjectrb == null)
                {
                    _itemObjectrb = collision.GetComponent<Rigidbody2D>();
                }
                ApproachPlayer();
            }
        }
    }

    /// <summary>
    /// �v���C���[�ɋ߂Â��֐�
    /// </summary>
    void ApproachPlayer()
    {
        if(_playerRb == null)
        {
            _playerRb = GetComponentInParent<Rigidbody2D>();
        }
        var dir = _playerRb.transform.position - _itemObjectrb.transform.position;
        _itemObjectrb.velocity = dir.normalized * _itemSpeed;
    }
}
