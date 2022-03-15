using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.Linq;

public class InputCommand : MonoBehaviour, IPauseable
{
    [SerializeField]
    InputAction _commandsAnswer1 = default;
    [SerializeField]
    InputAction _commandsAnswer2 = default;
    [SerializeField]
    InputAction _commandsAnswer3 = default;
    [SerializeField]
    InputAction _commandsAnswer4 = default;
    [SerializeField]
    InputAction _commandsAnswer5 = default;
    [SerializeField]
    InputAction _commandsAnswer6 = default;
    [SerializeField]
    InputAction _commandsAnswer7 = default;
    [SerializeField]
    InputAction _commandsAnswer8 = default;
    [SerializeField]
    InputAction _commandsAnswer9 = default;
    [SerializeField]
    InputAction _commandsAnswer10 = default;

    bool[] _isCommandSuccess = new bool[9];
    bool _isCommandStarted = false; 

    [SerializeField]
    [Header("�R�}���h�����������ۂ̉�")]
    SoundType _onCommandSuccessSound = SoundType.Sword;

    [SerializeField]
    Text _onCommandSuccessText;

    [SerializeField]
    [Header("�R�}���h���͂��I��点�Ȃ��Ƃ����Ȃ�����(�~���b)")]
    int _commandFinishTime = 1000;

    void Start()
    {
        _commandsAnswer1.Enable();
        _commandsAnswer2.Enable();
        _commandsAnswer3.Enable();
        _commandsAnswer4.Enable();
        _commandsAnswer5.Enable();
        _commandsAnswer6.Enable();
        _commandsAnswer7.Enable();
        _commandsAnswer8.Enable();
        _commandsAnswer9.Enable();
        _commandsAnswer10.Enable();
        OnInputCommand();
    }

    private async void StartCommand()
    {
        _isCommandStarted = true;
        await Task.Delay(_commandFinishTime);
        _isCommandStarted = false;
    }

    private void OnInputCommand()
    {
        _commandsAnswer1.started += _ =>
        {
            if (PauseManager.Instance.PauseFlg == true)
            {
                Debug.Log("�R�}���h�J�n");
                StartCommand();
                _isCommandSuccess[0] = true;
            }
        };

        _commandsAnswer2.started += _ =>
        {
            if (_isCommandSuccess[0] && PauseManager.Instance.PauseFlg == true && _isCommandStarted)
            {
                _isCommandSuccess[1] = true;
            }
        };

        _commandsAnswer3.started += _ =>
        {
            if (_isCommandSuccess[1] && PauseManager.Instance.PauseFlg == true && _isCommandStarted)
            {
                _isCommandSuccess[2] = true;
            }
        };

        _commandsAnswer4.started += _ =>
        {
            if (_isCommandSuccess[2] && PauseManager.Instance.PauseFlg == true && _isCommandStarted)
            {
                _isCommandSuccess[3] = true;
            }
        };

        _commandsAnswer5.started += _ =>
        {
            if (_isCommandSuccess[3] && PauseManager.Instance.PauseFlg == true && _isCommandStarted)
            {
                _isCommandSuccess[4] = true;
            }
        };

        _commandsAnswer6.started += _ =>
        {
            if (_isCommandSuccess[4] && PauseManager.Instance.PauseFlg == true && _isCommandStarted)
            {
                _isCommandSuccess[5] = true;
            }
        };

        _commandsAnswer7.started += _ =>
        {
            if (_isCommandSuccess[5] && PauseManager.Instance.PauseFlg == true && _isCommandStarted)
            {
                _isCommandSuccess[6] = true;
            }
        };

        _commandsAnswer8.started += _ =>
        {
            if (_isCommandSuccess[6] && PauseManager.Instance.PauseFlg == true && _isCommandStarted)
            {
                _isCommandSuccess[7] = true;
            }
        };

        _commandsAnswer9.started += _ =>
        {
            if (_isCommandSuccess[7] && PauseManager.Instance.PauseFlg == true && _isCommandStarted)
            {
                _isCommandSuccess[8] = true;
            }
        };

        _commandsAnswer10.started += _ =>
        {
            if (_isCommandSuccess[8] && PauseManager.Instance.PauseFlg == true && _isCommandStarted)
            {
                for (int i = 0; i < _isCommandSuccess.Length; i++)
                {
                    _isCommandSuccess[i] = false;
                }

                Debug.Log("�R�}���h����");
                _isCommandStarted = false;
                SoundManager.Instance.UseSound(_onCommandSuccessSound);
                GameManager.Instance.Player.IsGodMode = !GameManager.Instance.Player.IsGodMode;

                var isGodModeState = GameManager.Instance.Player.IsGodMode ? "�L��!" : "����!";

                _onCommandSuccessText.text = "���G���[�h" + isGodModeState;

            }
        };
    }

    public void PauseResume(bool isPause)
    {
        if(isPause)
        {

        }
        else
        {
            _onCommandSuccessText.text = "";
        }
    }

    void OnEnable()
    {
        PauseManager.Instance.SetEvent(this);    
    }

    void OnDisable()
    {
        PauseManager.Instance.RemoveEvent(this);
    }
}