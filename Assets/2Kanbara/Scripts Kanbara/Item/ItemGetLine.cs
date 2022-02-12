using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGetLine : MonoBehaviour
{
    [SerializeField, Header("�A�C�e��")] GameObject[] _item;
    [SerializeField, Header("�v���C���[�̃^�O")] string _playerTag = "Player";
    GameObject _player;

    private void Start()
    {
        _player = GameObject.FindWithTag(_playerTag);
    }
}
