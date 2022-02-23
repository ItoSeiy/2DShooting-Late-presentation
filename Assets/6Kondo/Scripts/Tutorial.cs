using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Tutorial : SingletonMonoBehaviour<Tutorial>
{
    [SerializeField, Header("�ړ��`���[�g���A���̃`�F�b�N")] GameObject _moveCheck;
    [SerializeField, Header("�ړ��`���[�g���A���̑Ή��L�[")] InputAction _inputMove;
    [SerializeField, Header("�V���b�g�`���[�g���A���̃`�F�b�N")] GameObject _shotCheck;
    [SerializeField, Header("�V���b�g�`���[�g���A���̑Ή��L�[")] InputAction _inputShot;
    [SerializeField, Header("�`���[�W�V���b�g�`���[�g���A���̃`�F�b�N")] GameObject _chargeShotCheck;
    [SerializeField, Header("�`���[�W�V���b�g�`���[�g���A���̑Ή��L�[")] InputAction _inputChargeShot;
    [SerializeField, Header("��������̃`���[�g���A���̃`�F�b�N")] GameObject _slowMoveCheck;
    [SerializeField, Header("��������̃`���[�g���A���̑Ή��L�[")] InputAction _inputSlowMove;
    [SerializeField, Header("�A�C�e���擾�̃`���[�g���A���̃`�F�b�N")] GameObject _itemCheck;
    private void Start()
    {
        MoveTutorial();
        ShotTutorial();
        SlowMoveTutorial();
        CharegeShotTutorial();
    }
    private void OnEnable()
    {
        _inputMove.Enable();
        _inputShot.Enable();
        _inputSlowMove.Enable();
        _inputChargeShot.Enable();
    }
    private void OnDisable()
    {
        _inputMove.Disable();
        _inputShot.Disable();
        _inputSlowMove.Disable();
        _inputChargeShot.Disable();
    }
    private void MoveTutorial()
    {            
        _inputMove.performed += _ => _moveCheck.SetActive(true);
    }
    private void ShotTutorial()
    {
        _inputShot.performed += _ => _shotCheck.SetActive(true);
    }
    private void CharegeShotTutorial()
    {
        _inputChargeShot.performed += _ => _chargeShotCheck.SetActive(true);
    }
    private void SlowMoveTutorial()
    {
        _inputSlowMove.performed += _ => _slowMoveCheck.SetActive(true);
    }
    public void GetItemTutorial()
    {
        _itemCheck.SetActive(true);
    }
}
