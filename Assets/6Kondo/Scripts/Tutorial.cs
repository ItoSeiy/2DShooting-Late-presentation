using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Tutorial : MonoBehaviour
{
    [SerializeField, Header("�ړ��`���[�g���A���̃`�F�b�N")] GameObject _moveCheck;
    [SerializeField, Header("�ړ��`���[�g���A���̑Ή��L�[")] InputAction _inputMove;
    [SerializeField, Header("�ˌ��`���[�g���A���̃`�F�b�N")] GameObject _shotCheck;
    [SerializeField, Header("�ˌ��`���[�g���A���̑Ή��L�[")] InputAction _inputShot;
    [SerializeField, Header("��������̃`���[�g���A���̃`�F�b�N")] GameObject _slowMoveCheck;
    [SerializeField, Header("��������̃`���[�g���A���̑Ή��L�[")] InputAction _inputSlowMove;
    private void Start()
    {
        MoveTutorial();
        ShotTutorial();
        SlowMoveTutorial();
    }
    private void OnEnable()
    {
        _inputMove.Enable();
        _inputShot.Enable();
        _inputSlowMove.Enable();
    }
    private void OnDisable()
    {
        _inputMove.Disable();
        _inputShot.Disable();
        _inputSlowMove.Disable();
    }
    private void SetMoveCheck()
    {
        _moveCheck.SetActive(true);
    }
    private void MoveTutorial()
    {            
        _inputMove.performed += _ => SetMoveCheck();
    }
    private void SetShotCheck()
    {
        _shotCheck.SetActive(true);
    }
    private void ShotTutorial()
    {
        _inputShot.performed += _ => SetShotCheck();
    }
    private void SetSlowMoveCheck()
    {
        _slowMoveCheck.SetActive(true);
    }
    private void SlowMoveTutorial()
    {
        _inputShot.performed += _ => SetSlowMoveCheck();
    }
}
