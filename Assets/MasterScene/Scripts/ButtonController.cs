using Overdose.Data;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    Button _button;
    Animator _animator = null;

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
    bool _isFirstSelectButton = false;

    [SerializeField]
    [Header("�Ăяo���{�^��")]
    GameObject[] _callNextButtons = null;

    [SerializeField]
    [Header("�ꏏ�ɏ�����Button")]
    GameObject[] _deleteButtons = null;

    [SerializeField]
    [Header("�Ăяo���p�l��")]
    GameObject[] _callPanel = null;

    [SerializeField]
    [Header("�ꏏ�ɏ����p�l��")]
    GameObject[] _deletePanel = null;

    [SerializeField]
    [Header("�Đ�����A�j���[�V����")]
    string _animStateName = null;


    [SerializeField]
    SoundType[] _onClickSounds;

    [SerializeField]
    [Header("[�X�e�[�W�I����] �X�e�[�W���������Ă��Ȃ��������ɏo���p�l��")]
    GameObject _stageSelectWarningPanel = null;

    [SerializeField]
    [Header("[�X�e�[�W�I����] �X�e�[�W�ԍ�")]
    int _stageNum;

    [SerializeField]
    [Header("[�X�e�[�W�I����] �v���C���[�ԍ�")]
    int _playerNum;

    SceneLoadCaller _sceneLoadCaller;
    private async void OnEnable()
    {
        _sceneLoadCaller = GetComponent<SceneLoadCaller>();
        _button = GetComponent<Button>();
        _animator = GetComponent<Animator>();
        if(_animStateName != null && _animator != null)
        {
            _animator.Play(_animStateName);
        }
        if (_isDelayMode)
        {
            await Task.Delay(_delay);
        }
        if(_isFirstSelectButton)
        {
            _button.Select();
        }
    }

    public void NormalSelect()
    {
        if (_onClickSounds.Any())
        {
            foreach(var sound in _onClickSounds)
            {
                SoundManager.Instance.UseSound(sound);
            }
        }
        if(_isDelete)
        {
            this.gameObject.SetActive(false);
        }
        if(_deleteButtons�@!= null)
        {
            ActiveChange(_deleteButtons, false);
        }
        if (_callNextButtons != null)
        {
            ActiveChange(_callNextButtons, true);
        }
        if(_callPanel != null)
        {
            ActiveChange(_callPanel, true);
        }
        if(_deletePanel != null)
        {
            ActiveChange(_deletePanel, false);
        }
    }

    public void StageSelect()
    {
        switch (_playerNum)
        {
            case 1:

                if(SaveDataManager.Instance.SaveData.Player1StageActives[_stageNum - 1])
                {
                    NormalSelect();
                    _sceneLoadCaller.LoadSceneString();
                }
                else if(_stageNum <= 0 || _stageNum > SaveDataManager.Instance.Player1StageConut)
                {
                    Debug.LogError($"�X�e�[�W{_stageNum}�͑��݂��܂���");
                }
                else _stageSelectWarningPanel.SetActive(true);

                break;

            case 2:

                if (SaveDataManager.Instance.SaveData.Player2StageActives[_stageNum - 1])
                {
                    NormalSelect();
                    _sceneLoadCaller.LoadSceneString(); 
                }
                else if(_stageNum <= 0 || _stageNum > SaveDataManager.Instance.Player2StageCount)
                {
                    Debug.LogError($"�X�e�[�W{_stageNum}�͑��݂��܂���");
                }
                else _stageSelectWarningPanel.SetActive(true);

                break;
            default:
                Debug.LogError($"�v���C���[{_playerNum}�͑��݂��܂���");
                break;
        }
    }

    private void ActiveChange(GameObject[] gameObjects, bool active)
    {
        foreach(var go in gameObjects)
        {
            go.SetActive(active);
        }
    }
}