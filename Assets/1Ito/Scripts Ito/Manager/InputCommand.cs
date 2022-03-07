using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Threading.Tasks;

public class InputCommand : MonoBehaviour
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

    [SerializeField,Header("�R�}���h�����������ۂ̉�")]
    SoundType _onCommandSuccessSound = SoundType.Sword;

    [SerializeField]
    Text _onCommandSuccessText;
    [SerializeField, Header("�e�L�X�g�����܂�����(�~���b)")]
    int _textDisableTime;

    public async void OnInputCommand(InputAction.CallbackContext context)
    {
        if (context.action == _commandsAnswer1 && PauseManager.Instance.PauseFlg == true)
        {
            Debug.Log("�R�}���h�J�n");
            _isCommandSuccess[0] = true;
        }
        else if(context.action == _commandsAnswer2 && _isCommandSuccess[0] && PauseManager.Instance.PauseFlg == true)
        {
            _isCommandSuccess[1] = true;
        }
        else if (context.action == _commandsAnswer3 && _isCommandSuccess[1] && PauseManager.Instance.PauseFlg == true)
        {
            _isCommandSuccess[2] = true;
        }
        else if (context.action == _commandsAnswer4 && _isCommandSuccess[2] && PauseManager.Instance.PauseFlg == true)
        {
            _isCommandSuccess[3] = true;
        }
        else if (context.action == _commandsAnswer5 && _isCommandSuccess[3] && PauseManager.Instance.PauseFlg == true)
        {
            _isCommandSuccess[4] = true;
        }
        else if (context.action == _commandsAnswer6 && _isCommandSuccess[4] && PauseManager.Instance.PauseFlg == true)
        {
            _isCommandSuccess[5] = true;
        }
        else if (context.action == _commandsAnswer7 && _isCommandSuccess[5] && PauseManager.Instance.PauseFlg == true)
        {
            _isCommandSuccess[6] = true;
        }
        else if (context.action == _commandsAnswer8 && _isCommandSuccess[6] && PauseManager.Instance.PauseFlg == true)
        {
            _isCommandSuccess[7] = true;
        }
        else if (context.action == _commandsAnswer9 && _isCommandSuccess[7] && PauseManager.Instance.PauseFlg == true)
        {
            _isCommandSuccess[8] = true;
        }
        else if (context.action == _commandsAnswer10 && _isCommandSuccess[8] && PauseManager.Instance.PauseFlg == true)
        {
            Debug.Log("�R�}���h����");
            SoundManager.Instance.UseSound(_onCommandSuccessSound);
            GameManager.Instance.Player.IsGodMode = !GameManager.Instance.Player.IsGodMode;

            var isGodModeState = GameManager.Instance.Player.IsGodMode ? "�L��!" : "����!";

            _onCommandSuccessText.text = "���G���[�h" + isGodModeState;
            await Task.Delay(_textDisableTime);
            _onCommandSuccessText.text = "";
        }
    }
}