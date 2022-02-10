using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;

[RequireComponent(typeof(GSSReader))]
public class NovelRenderer : MonoBehaviour
{
    public bool IsNovelFinish { get; private set; }
    [SerializeField] Text _mainText = null;
    [SerializeField] Text _nameText = null;
    [SerializeField, Range(0f, 0.5f)] float _textInterval = 0.1f;
    float _oldTextInterval;
    [SerializeField] Animator _characterAnimator;
    [SerializeField] Animator _bossAnimator;
    [SerializeField] AudioSource _textAudioSource;
    [SerializeField] GSSReader _gssReader;
    string[][] _datas = null;

    int _ggsRow = 0;
    int _currentCharNum = 0;

    bool _isDisplaying = false;
    bool _isClick = false;
    bool _isCommandFirstTime = true;

    SoundType _soundType;

    const int NAME_TEXT_COLUMN = 0;
    const int MAIN_TEXT_COLUMN = 1;
    const int ACTION_TEXT_COLUMN = 2;

    private void Start()
    {
        _gssReader.Reload();
        _mainText.text = "";
        _oldTextInterval = _textInterval;
    }

    private void Update()
    {
        if (_gssReader.IsLoading || CheckNovelFinish()) return;
        ControllText();
    }
    public void OnGSSLoadEnd()
    {
        _datas = _gssReader.Datas;
    }

    public void ControllText()
    {
        //�e�L�X�g���Ō�܂œǂݍ��܂�Ă��Ȃ�������
        if (_currentCharNum < _datas[_ggsRow][MAIN_TEXT_COLUMN].Length)
        {

            if (_isClick)//�N���b�N���ꂽ��e�L�X�g���΂�
            {
                _textInterval = 0;
                _isClick = false;
            }

            if (_datas[_ggsRow][MAIN_TEXT_COLUMN][_currentCharNum] == '&' && _isCommandFirstTime)
            {
                //�R�}���h���͂����o����
                Command();
                _isCommandFirstTime = false;
            }
            else
            {
                DisplayText();
            }
        }
        else//�e�L�X�g���Ō�܂œǂݍ��܂ꂽ��
        {
            if (_isClick)
            {
                NextRow();//�s�̓Y�������J�E���g�A�b�v
                _isClick = false;
            }
        }
    }

    void NextRow()
    {
        _ggsRow++;
        _textInterval = _oldTextInterval;
        _currentCharNum = 0;
        _mainText.text = "";
        _nameText.text = "";

        _isCommandFirstTime = true;
        _isDisplaying = false;
    }

    void Command()
    {
        string command = _datas[_ggsRow][MAIN_TEXT_COLUMN];
        string action = _datas[_ggsRow][ACTION_TEXT_COLUMN];
        switch (command)
        {
            case "&MainCharacterAnim":
                Debug.Log("���C���L�����A�j���[�V�����A�j���[�V����" + action);
                _characterAnimator.Play(action);
                break;
            case "&BossAnim":
                Debug.Log("�{�X�A�j���[�V����" + action);
                _bossAnimator.Play(action);
                break;
            case "&Sound":
                System.Enum.TryParse(action, out _soundType);
                SoundManager.Instance.UseSound(_soundType);
                break;
            default:
                Debug.LogError(command + action + "�Ƃ����R�}���h�͖����ł�");
                break;
        }
        NextRow();
    }

    void DisplayText()
    {
        //�o�͈͂�s�ɂ���x�̂ݎ��s����
        if (_isDisplaying) return;
        StartCoroutine(MoveText());
    }

    IEnumerator MoveText()
    {
        _isDisplaying = true;

        switch (_datas[_ggsRow][NAME_TEXT_COLUMN])
        {
            case "���ʉ�":
                _textAudioSource.mute = true;
                _nameText.text = "";
                break;
            default:
                _textAudioSource.mute = false;
                _nameText.text = _datas[_ggsRow][NAME_TEXT_COLUMN];
                break;
        }

        while(_isDisplaying)
        {
            _textAudioSource.Play();

            if (_currentCharNum == _datas[_ggsRow][MAIN_TEXT_COLUMN].Length) yield break;

            _mainText.text += _datas[_ggsRow][MAIN_TEXT_COLUMN][_currentCharNum];
            _currentCharNum++;
            yield return new WaitForSeconds(_textInterval);
        }
    }


    public void OnClick(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            _isClick = true;
        }

        if(context.canceled)
        {
            _isClick = false;
        }
    }

    bool CheckNovelFinish()
    {
        if (_ggsRow >= _datas.Length)
        {
            Debug.Log("���ׂẴV�i���I��ǂݍ���");
            IsNovelFinish = true;
            _mainText.text = "";
            _nameText.text = "";
            return true;
        }
        else
        {
            IsNovelFinish = false;
            return false;
        }
    }
}
