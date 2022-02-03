using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;

[RequireComponent(typeof(GSSReader))]
public class NovelRenderer : MonoBehaviour
{
    public bool NovelFinish { get; private set; }
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

    const int MainTextColumn = 1;
    const int NameTextColumn = 0;

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
        if (_currentCharNum < _datas[_ggsRow][MainTextColumn].Length)
        {
            if (_isClick)//�N���b�N���ꂽ��e�L�X�g���΂�
            {
                _textInterval = 0;
            }

            _isClick = false;

            if (_datas[_ggsRow][MainTextColumn][_currentCharNum] == '&' && _isCommandFirstTime)
            {
                //�R�}���h���͂����o����
                Command();
                _isCommandFirstTime = false;
            }
            else
            {
                //�e�L�X�g���o�͂���
                DisplayText();
            }
        }
        else//�e�L�X�g���Ō�܂œǂݍ��܂ꂽ��
        {
            //�e�L�X�g�̑�����߂�
            _textInterval = _oldTextInterval;
            _isDisplaying = false;
            NextRow();//�s�̓Y�������J�E���g�A�b�v
        }
    }

    void NextRow()
    {
        if(_isClick)
        {
            _ggsRow++;
            _currentCharNum = 0;
            _mainText.text = "";

            if (CheckNovelFinish()) return;

            if (_datas[_ggsRow][MainTextColumn][_currentCharNum] == '&')
            {
                Command();
            }
            _isClick = false;
        }
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

        switch (_datas[_ggsRow][NameTextColumn])
        {
            case "���ʉ�":
                _textAudioSource.mute = true;
                _nameText.text = "";
                break;
            default:
                _textAudioSource.mute = false;
                _nameText.text = _datas[_ggsRow][NameTextColumn];
                break;
        }

        while(_isDisplaying)
        {
            _textAudioSource.Play();

            if (_currentCharNum == _datas[_ggsRow][MainTextColumn].Length) yield break;

            _mainText.text += _datas[_ggsRow][MainTextColumn][_currentCharNum];
            _currentCharNum++;
            yield return new WaitForSeconds(_textInterval);
        }
    }

    void Command()
    {
        string[] command = _datas[_ggsRow][MainTextColumn].Split(' ');
        switch (command[0])
        {
            case "&MainCharacterAnim":
                Debug.Log("���C���L�����A�j���[�V�����A�j���[�V����" + command[1]);
                _characterAnimator.Play(command[1]);
                break;
            case "&BossAnim":
                Debug.Log("�{�X�A�j���[�V����" + command[1]);
                _bossAnimator.Play(command[1]);
                break;
            case "&Sound":
                System.Enum.TryParse(command[1], out _soundType);
                SoundManager.Instance.UseSound(_soundType);
                break;
            default:
                Debug.LogError(command[0] + command[1] + "�Ƃ����R�}���h�͖����ł�");
                break;
        }
        _ggsRow++;

        if (CheckNovelFinish()) return;

        if (_datas[_ggsRow][MainTextColumn][_currentCharNum] == '&')
        {
            Command();
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
            NovelFinish = true;
            _mainText.text = "";
            _nameText.text = "";
            return true;
        }
        else
        {
            NovelFinish = false;
            return false;
        }
    }
}

public enum NovelPhase
{
    Before,
    After,
    Lose
}
