using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class ItemBase : MonoBehaviour
{
    [SerializeField, Header("�v���C���[�̃^�O")] string _playerTag = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == _playerTag)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
