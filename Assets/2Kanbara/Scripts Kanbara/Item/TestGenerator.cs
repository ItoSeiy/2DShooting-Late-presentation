using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGenerator : MonoBehaviour
{
    [SerializeField, Header("�A�C�e���̃C���^�[�o��")] float _intarval;
    [SerializeField, Header("��������A�C�e��")] GameObject _items;
    float _time;

    void Update()
    {
        _time += Time.deltaTime;
        if(_time > _intarval)
        {
            Instantiate(_items);
            _time = 0;
        }
    }
}
