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
    [SerializeField, Header("�{���擾�̃`���[�g���A���̃`�F�b�N")] GameObject _bombCheck;
    [SerializeField, Header("�c�@�擾�̃`���[�g���A���̃`�F�b�N")] GameObject _residueCheck;
    [SerializeField, Header("���G�A�C�e���擾�̃`���[�g���A���̃`�F�b�N")] GameObject _invisibleCheck;
    [SerializeField, Header("�p���[�A�C�e���擾�̃`���[�g���A���̃`�F�b�N")] GameObject _powerCheck;
    [SerializeField, Header("�`���[�g���A���N���A���ɕ\��")] GameObject _tutorialClearCheck;
    private void Start()
    {
        MoveTutorial();
        ShotTutorial();
        SlowMoveTutorial();
        CharegeShotTutorial();
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBase>();
    }
    public void Update()
    {
        if (_moveCheck && _shotCheck && _chargeShotCheck && _slowMoveCheck && _itemCheck &&
            _bombCheck && _residueCheck && _invisibleCheck && _powerCheck)
        {
            _tutorialClearCheck.SetActive(true);
        }
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
    public void BombTutorial()
    {
        _bombCheck.SetActive(true);
    }
    public void ResidueTutorial()
    {
        _residueCheck.SetActive(true);
    }
    public void InvisibleTutorial()
    {
        _invisibleCheck.SetActive(true);
    }
    public void PowerTutorial()
    {
        _powerCheck.SetActive(true);
    }
}