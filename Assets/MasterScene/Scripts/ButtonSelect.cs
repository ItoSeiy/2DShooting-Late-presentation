using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class ButtonSelect : MonoBehaviour
{
    Button _button;

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
    [Header("�Ăяo���ׂ��{�^�������邩�ǂ���")]
    bool _isMustCallNextButton = false;

    [SerializeField]
    [Header("�{�^���̌Ăяo����x�点��b���i�~���b�j")]
    int _delayCallButton = 500;

    [SerializeField]
    [Header("�Ăяo���{�^��")]
    GameObject[] _callNextButtons;

    [SerializeField]
    [Header("�ꏏ�ɏ����{�^�������邩�ǂ���")]
    bool _isMustDeleteButton = false;

    [SerializeField]
    [Header("�ꏏ�ɏ�����Button")]
    GameObject[] _deleteButtons;

    private async void OnEnable()
    {
        _button = GetComponent<Button>();
        if (_isDelayMode)
        {
            await Task.Delay(_delay);
        }
        if(_isFirstSelectButton)
        {
            _button.Select();
        }
    }

    public async void Click()
    {
        if(_isDelete)
        {
            this.gameObject.SetActive(false);
        }
        if(_isMustDeleteButton)
        {
            foreach(var button in _deleteButtons)
            {
                button.SetActive(false);
            }
        }
        if (_isMustCallNextButton)
        {
            foreach(var button in _callNextButtons)
            {
                await Task.Delay(_delayCallButton);
                button.SetActive(true);
            }
        }
    }
}
