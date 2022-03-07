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
    [SerializeField, Header("��������̃`���[�g���A���̃`�F�b�N")] GameObject _slowMoveCheck;
    [SerializeField, Header("��������̃`���[�g���A���̑Ή��L�[")] InputAction _inputSlowMove;
    [SerializeField, Header("�`���[�W�V���b�g�`���[�g���A���̃`�F�b�N")] GameObject _chargeShotCheck;
    [SerializeField, Header("�`���[�W�V���b�g�`���[�g���A���̑Ή��L�[")] InputAction _inputChargeShot;
    [SerializeField, Header("�A�C�e���擾�̃`���[�g���A���̃`�F�b�N")] GameObject _itemCheck;
    [SerializeField, Header("�{���擾�̃`���[�g���A���̃`�F�b�N")] GameObject _bombCheck;
    [SerializeField, Header("�c�@�擾�̃`���[�g���A���̃`�F�b�N")] GameObject _residueCheck;
    [SerializeField, Header("���G�A�C�e���擾�̃`���[�g���A���̃`�F�b�N")] GameObject _invisibleCheck;
    [SerializeField, Header("�p���[�A�C�e���擾�̃`���[�g���A���̃`�F�b�N")] GameObject _powerCheck;
    [SerializeField, Header("�X�R�A�A�C�e���擾�̃`���[�g���A���̃`�F�b�N")] GameObject _scoreCheck;
    [SerializeField, Header("�`���[�g���A���N���A���ɕ\��")] GameObject _tutorialClearCheck;

    protected override void Awake()
    {
        base.Awake();
        GameManager.Instance.SaveValue();
        GameManager.Instance.InitValue();
    }

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
        _inputMove.performed += _ =>
        {
            _moveCheck.SetActive(true);
            CheckClear();
        };
    }
    private void ShotTutorial()
    {
        _inputShot.started += _ =>
        {
            _shotCheck.SetActive(true);
            CheckClear();
        };
    }
    private void CharegeShotTutorial()
    {
        _inputChargeShot .performed += _ => 
        {
            _chargeShotCheck.SetActive(true);
            CheckClear();
        };
    }
    private void SlowMoveTutorial()
    {
        _inputSlowMove.performed += _ =>
        {
            _slowMoveCheck.SetActive(true);
            CheckClear();
        };
    }
    public void GetItemTutorial()
    {
        _itemCheck.SetActive(true);
        CheckClear();
    }
    public void BombTutorial()
    {
        _bombCheck.SetActive(true);
        CheckClear();
    }
    public void ResidueTutorial()
    {
        _residueCheck.SetActive(true);
        CheckClear();
    }
    public void InvisibleTutorial()
    {
        _invisibleCheck.SetActive(true);
        CheckClear();
    }
    public void PowerTutorial()
    {
        _powerCheck.SetActive(true);
        CheckClear();
    }
    public void ScoreTutorial()
    {
        _scoreCheck.SetActive(true);
        CheckClear();
    }
    private void CheckClear()
    {
        var isClear = _moveCheck.activeSelf 
                   && _shotCheck.activeSelf
                   && _chargeShotCheck.activeSelf
                   && _slowMoveCheck.activeSelf
                   && _itemCheck.activeSelf
                   && _bombCheck.activeSelf
                   && _residueCheck.activeSelf
                   && _invisibleCheck.activeSelf
                   && _powerCheck.activeSelf
                   && _scoreCheck.activeSelf;
        if (isClear)
        {
            _tutorialClearCheck.SetActive(true);
            GameManager.Instance.Player.CanMove = false;
            GameManager.Instance.ReturnValue();
        }
    }
}
