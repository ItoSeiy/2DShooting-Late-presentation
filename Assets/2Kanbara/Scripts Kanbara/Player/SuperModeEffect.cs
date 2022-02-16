using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperModeEffect : MonoBehaviour
{
    [SerializeField, Header("�^�[�Q�b�g")] GameObject _target;
    [SerializeField, Header("���x")] float _speed = default;
    [SerializeField, Header("�p�x")] float _angle = 50;
    const float _delaySpeed = 100;

    void Update()
    {
        transform.RotateAround(_target.transform.position, Vector3.forward, _angle * _speed / _delaySpeed);
    }
}