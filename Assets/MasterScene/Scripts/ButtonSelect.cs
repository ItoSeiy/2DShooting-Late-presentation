using Overdose.Data;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelect : MonoBehaviour
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

    private async void OnEnable()
    {
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

    public void Click()
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
    void ActiveChange(GameObject[] gameObjects, bool set)
    {
        foreach(var go in gameObjects)
        {
            go.SetActive(set);
        }
    }
}
