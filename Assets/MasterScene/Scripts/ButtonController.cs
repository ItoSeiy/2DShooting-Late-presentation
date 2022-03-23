using Overdose.Data;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonController : MonoBehaviour
{
    [SerializeField]
    [Header("�N���b�N�����Ƃ��Ɏ������������ǂ���")]
    bool _isDelete = true;

    [SerializeField]
    [Header("�Z���N�g��x�点�郂�[�h")]
    bool _isDelayMode = false;

    [SerializeField]
    [Header("�x�点��b���i�~���b�j")]
    int _delay = 1500;

    [SerializeField]
    [Header("�ŏ��ɃZ���N�g����邩�ǂ���")]
    bool _isFirstSelectMode = false;

    [SerializeField]
    [Header("�Ăяo���I�u�W�F�N�g")]
    GameObject[] _callNextButtons = null;

    [SerializeField]
    [Header("�I�����ɏ�����I�u�W�F�N�g")]
    GameObject[] _deleteButtons = null;

    [SerializeField]
    [Header("�Đ�����A�j���[�V����")]
    string _animStateName = null;

    [SerializeField]
    [Header("�ʏ�̃{�^����")]
    SoundType[] _onClickSounds;

    [SerializeField]
    [Header("�X�e�[�W�I��p�Ɏg�p����{�^�����ǂ���")]
    bool _isStageSelectMode = false;

    [SerializeField]
    [Header("[�X�e�[�W�I����]�������Ă��Ȃ��X�e�[�W��I�������Ƃ��̉�")]
    SoundType[] _onClickSoundsUnknownStage;

    [SerializeField]
    [Header("[�X�e�[�W�I����] �������Ă��Ȃ��X�e�[�W��I��������o���p�l��")]
    GameObject _unknownStageSelectWarningPanel = null;

    [SerializeField]
    [Header("[�X�e�[�W�I����]�X�e�[�W�����̃{�^���̐F")]
    Color _buttonTargetColor = Color.white;

    [SerializeField]
    [Header("[�X�e�[�W�I����]�X�e�[�W�����̃e�L�X�g�̐F")]
    Color _textTargetColor; 

    [SerializeField]
    [Header("[�X�e�[�W�I����]�X�e�[�W��������ꂽ���̃{�^���̕ϐF�̎���")]
    float _fadeTime = 1f;

    [SerializeField]
    [Header("[�X�e�[�W�I����] �X�e�[�W�ԍ�")]
    int _stageNum = 0;

    [SerializeField]
    [Header("[�X�e�[�W�I����] �v���C���[�ԍ�")]
    int _playerNum = 0;

    Button _button;
    Animator _animator = null;
    Text _buttonText;

    SceneLoadCaller _sceneLoadCaller;

    private async void OnEnable()
    {
        _sceneLoadCaller = GetComponent<SceneLoadCaller>();
        _button = GetComponent<Button>();
        _animator = GetComponent<Animator>();
        _buttonText = GetComponentInChildren<Text>();

        if(_animStateName != null && _animator != null)
        {
            _animator.Play(_animStateName);
        }
        if (_isDelayMode)
        {
            await Task.Delay(_delay);
        }
        if(_isFirstSelectMode)
        {
            _button.Select();
        }
        if(_isStageSelectMode)
        {
            CheckButtonState();
        }
    }

    public void NormalSelect()
    {
        if (_onClickSounds != null)
        {
            UseSounds(_onClickSounds);
        }
        if(_isDelete)
        {
            this.gameObject.SetActive(false);
        }
        if(_deleteButtons�@!= null)
        {
            ObjectsActiveChange(_deleteButtons, false);
        }
        if (_callNextButtons != null)
        {
            ObjectsActiveChange(_callNextButtons, true);
        }

        _sceneLoadCaller.LoadSceneString();
    }

    public void StageSelect()
    {
        switch (_playerNum)
        {
            case 1:

                if (SaveDataManager.Instance.SaveData.Player1StageActives[_stageNum - 1] == true)
                {
                    NormalSelect();
                }
                else if (_stageNum <= 0 || _stageNum > SaveDataManager.Instance.Player1StageConut)
                {
                    Debug.LogError($"�X�e�[�W{_stageNum}�͑��݂��܂���");
                }
                else
                {
                    UseSounds(_onClickSoundsUnknownStage);
                    _unknownStageSelectWarningPanel.SetActive(true);
                }

                break;

            case 2:

                if (SaveDataManager.Instance.SaveData.Player2StageActives[_stageNum - 1] == true)
                {
                    NormalSelect();
                }
                else if (_stageNum <= 0 || _stageNum > SaveDataManager.Instance.Player2StageCount)
                {
                    Debug.LogError($"�X�e�[�W{_stageNum}�͑��݂��܂���");
                }
                else
                {
                    UseSounds(_onClickSoundsUnknownStage);
                    _unknownStageSelectWarningPanel.SetActive(true);
                }

                break;
            default:
                Debug.LogError($"�v���C���[{_playerNum}�͑��݂��܂���");
                break;
        }
    }

    private void CheckButtonState()
    {
        switch(_playerNum)
        {
            case 1:

                if (SaveDataManager.Instance.SaveData.Player1StageActives[_stageNum - 1] == true)
                {
                    DoButtonColor();
                }
                else if (_stageNum <= 0 || _stageNum > SaveDataManager.Instance.Player1StageConut)
                {
                    Debug.LogError($"�X�e�[�W{_stageNum}�͑��݂��܂���");
                }

                break;

            case 2:

                if (SaveDataManager.Instance.SaveData.Player2StageActives[_stageNum - 1] == true)
                {
                    DoButtonColor();
                }
                else if (_stageNum <= 0 || _stageNum > SaveDataManager.Instance.Player2StageCount)
                {
                    Debug.LogError($"�X�e�[�W{_stageNum}�͑��݂��܂���");
                }

                break;
            default:
                Debug.LogError($"�v���C���[{_playerNum}�͑��݂��܂���");
                break;
        }
    }

    private void DoButtonColor()
    {
        var color = _button.colors.normalColor;

        DOTween.To(() => color,
            c => color = c,
            _buttonTargetColor,
            _fadeTime)
            .OnUpdate(() => _button.colors = new ColorBlock { normalColor = color })
            .OnComplete(() => _button.colors = new ColorBlock { normalColor = _buttonTargetColor });
    }

    private void DoTextColor()
    {
        DOTween.To(() => _buttonText.color,
            c => _buttonText.color = c,
            _textTargetColor,
            _fadeTime)
            .OnComplete(() => _buttonText.color = _textTargetColor);
    }

    private void ObjectsActiveChange(GameObject[] gameObjects, bool active) => Array.ForEach(gameObjects, x => x.SetActive(active));

    private void UseSounds(SoundType[] soundTypes) => Array.ForEach(soundTypes, x => SoundManager.Instance.UseSound(x));
}