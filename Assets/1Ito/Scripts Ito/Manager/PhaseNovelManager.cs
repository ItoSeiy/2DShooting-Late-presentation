using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Overdose.Novel;
using System.Threading.Tasks;

public class PhaseNovelManager : SingletonMonoBehaviour<PhaseNovelManager>
{
    public NovelPhase NovelePhaesState => _novelPhase;

    [SerializeField, Header("�m�x���O�ɑ҂���")]
    float _novelWaitTime = 8f;

    /// <summary>�X�v���b�h�V�[�g�V�[�g��ǂݍ��ރX�N���v�g(�퓬�O)</summary>
    [SerializeField]
    GSSReader _beforeGSSReader;
    /// <summary>�m�x���������o���A���䂷��X�N���v�g(�퓬�O)</summary>
    [SerializeField]
    NovelRenderer _beforeNovelRenderer;

    /// <summary>�X�v���b�h�V�[�g�V�[�g��ǂݍ��ރX�N���v�g(������)</summary>
    [SerializeField] 
    GSSReader _winGSSReader;
    /// <summary>�m�x���������o���A���䂷��X�N���v�g(�퓬�O)</summary>
    [SerializeField] 
    NovelRenderer _winNovelRenderer;

    /// <summary>�X�v���b�h�V�[�g�V�[�g��ǂݍ��ރX�N���v�g(�s�k��)</summary>
    [SerializeField] 
    GSSReader _loseGSSReader;
    /// <summary>�m�x���������o���A���䂷��X�N���v�g(�s�k��)</summary>
    [SerializeField] 
    NovelRenderer _loseNovelRenderer;

    /// <summary>�m�x�����f���o���L�����o�X</summary>
    [SerializeField]
    Canvas _novelCanvas;

    /// <summary>�Q�[���I�[�o�[���ɏo��UI</summary>
    [SerializeField]
    Canvas _gameOverCanavas;
    /// <summary>�Q�[���N���A���ɏo��UI</summary>
    [SerializeField]
    Canvas _gameClearCanvas;

    /// <summary>��ɃX�R�A�Ȃǂ��\������Ă���L�����o�X</summary>
    [SerializeField] 
    Canvas _uiCanvas;

    /// <summary>���u�G�̏o���ꏊ</summary>
    [SerializeField] 
    Transform _enemyGeneretePos;
    /// <summary>�{�X�̏o���ʒu</summary>
    [SerializeField] 
    Transform _bossGeneretePos;

    /// <summary>�m�x���̏��</summary>
    [SerializeField]
    NovelPhase _novelPhase = NovelPhase.None;

    /// <summary>�w�i</summary>
    [SerializeField] 
    SpriteRenderer _backGround;
    /// <summary�w�i�̃R�s�[</summary>
    SpriteRenderer 
    _backGroundClone = null;
    
    /// <summary>�w�i�̐e�I�u�W�F�N�g</summary>
    [SerializeField]
    GameObject _backGroundParent;
    /// <summary>�w�i�̒P�ʃx�N�g��</summary>
    [SerializeField]
    Vector2 _backGroundDir = Vector2.down;
    /// <summary>�w�i�̃X�N���[������X�s�[�h</summary>
    [SerializeField] float _scrollSpeed = 0.5f;
    /// <summary>�w�i��Y���W�̏����ʒu</summary>
    float _bgInitialPosY;

    /// <summary>�X�e�[�W�̃f�[�^</summary>
    [SerializeField] 
    StageParam _stageParam;

    private int _phaseIndex = default;
    private int _loopCount = default;
    private bool _isNovelFirstTime = true;
    private float _timer = default;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        _bgInitialPosY = _backGround.transform.position.y;

        _backGroundClone = Instantiate(_backGround, _backGroundParent.transform);
        _backGroundClone.transform.Translate(0f, _backGround.bounds.size.y, 0f);

        //���u�G�̏��񐶐�
        Instantiate(_stageParam.PhaseParms[_phaseIndex].Prefab).transform.position = _enemyGeneretePos.position;
    }

    void Update()
    {
        switch (_stageParam.PhaseParms[_phaseIndex].IsBoss)
        {
            case true:
                _timer += Time.deltaTime;

                if (_timer <= _novelWaitTime)
                {
                    CheckGameOver();
                    return;
                }

                Debug.Log("�{�X�J�n");
                Novel();
                break;

            case false:
                BackGround();
                CheckGameOver();
                break;
        }
    }

    /// <summary>
    /// �Q�[���I�[�o�[�𔻒肷��
    /// </summary>
    void CheckGameOver()
    {
        if (GameManager.Instance.IsGameOver)
        {
            //�Q�[���I�[�oUI�\��
            _gameOverCanavas.gameObject.SetActive(true);
            //UI�L�����o�X
            _uiCanvas.gameObject.SetActive(false);
            //�v���C���[�𓮂����Ȃ��悤�ɂ���
            GameManager.Instance.Player.CanMove = false;
        }
    }

    /// <summary>
    /// ���̃t�F�C�Y�̃v���n�u�𐶐�����
    /// </summary>
    /// <param name="isLoop">���݂̃t�F�C�Y�̃v���n�u��������x�������邩�ǂ���</param>
    public void EnemyGenerate(bool isLoop)
    {
        //�Q�[���I�[�o�[���͎��s���Ȃ�
        if (GameManager.Instance.IsGameOver) return;

        //�{�X�̃t�F�C�Y��������m�x�����Đ����Ă��琶�����邽�ߒe��
        if (_stageParam.PhaseParms[_phaseIndex].IsBoss) return;


        //���[�v������ꍇ�̓C���f�b�N�X���J�E���g�A�b�v���Ȃ�
        if (isLoop)
        {
            _loopCount++;
            Debug.Log("���[�v����" + _loopCount + "���");
            //���[�v����ׂ��񐔂𒴂�����t�F�C�Y�̃C���f�b�N�X���J�E���g�A�b�v����
            if(_loopCount >= _stageParam.PhaseParms[_phaseIndex].LoopTime)
            {
                Debug.Log("�J�E���g�A�b�v");
                _phaseIndex++;
            }
        }
        else
        {
            _phaseIndex++;
            Debug.Log("���[�v���Ȃ�" + _phaseIndex + "�C���f�b�N�X");
        }

        Instantiate(_stageParam.PhaseParms[_phaseIndex].Prefab).transform.position = _enemyGeneretePos.position;
    }

    /// <summary>
    /// �m�x���̏������s��
    /// �{�X�̐������s��
    /// ����,�s�k����UI�̕\�����s��
    /// </summary>
  �@void Novel()
    {
        //�m�x���̏�����s���Ƀm�x���̃t�F�C�Y��퓬�O�C�x���g�ɃZ�b�g����
        if(_isNovelFirstTime)
        {
            _novelPhase = NovelPhase.Before;
            _isNovelFirstTime = false;
        }
        //�Q�[���N���A�ł���΃m�x���̃t�F�C�Y��ς���
        if (GameManager.Instance.IsStageClear) _novelPhase = NovelPhase.Win;
        //�Q�[���I�[�o�[�ł���΃m�x���̃t�F�C�Y��ς���
        if (GameManager.Instance.IsGameOver) _novelPhase = NovelPhase.Lose;

        switch (_novelPhase)
        {
            case NovelPhase.Before://�퓬�O�̃m�x��

                if (_beforeGSSReader.gameObject.activeSelf == false)
                {   
                    //�m�x���֌W�̃X�N���v�g���A�^�b�`����Ă���I�u�W�F�N�g��L��������
                    _beforeGSSReader.gameObject.SetActive(true);
                }

                if (!_beforeGSSReader.IsLoading)
                {
                    //�m�x���̃f�[�^�̃��[�h���I�������
                    _novelCanvas.gameObject.SetActive(true);
                    _uiCanvas.gameObject.SetActive(false);
                }

                if(_beforeNovelRenderer.IsNovelFinish)
                {
                    //�m�x���̏����o�������ׂďI�������
                    //�m�x���֌W�̃X�N���v�g���A�^�b�`����Ă���I�u�W�F�N�g�𖳌�������
                    _beforeGSSReader.gameObject.SetActive(false);
                    _novelCanvas.gameObject.SetActive(false);

                    _uiCanvas.gameObject.SetActive(true);
                    _novelPhase = NovelPhase.None;
                }
                break;

            case NovelPhase.Win://������̃m�x��

                if (_winGSSReader.gameObject.activeSelf == false)
                {
                    //�m�x���֌W�̃X�N���v�g���A�^�b�`����Ă���I�u�W�F�N�g��L��������
                    _winGSSReader.gameObject.SetActive(true);
                }

                if(!_winGSSReader.IsLoading)
                {
                    //�m�x���̃f�[�^�̃��[�h���I�������
                    _novelCanvas.gameObject.SetActive(true);
                    _uiCanvas.gameObject.SetActive(false);
                }

                if(_winNovelRenderer.IsNovelFinish)
                {
                    //�m�x���̏����o�������ׂďI�������
                    //�m�x���֌W�̃X�N���v�g���A�^�b�`����Ă���I�u�W�F�N�g�𖳌�������
                    _winGSSReader.gameObject.SetActive(false);
                    _novelCanvas.gameObject.SetActive(false);

                    _gameClearCanvas.gameObject.SetActive(true);
                }

                break;

            case NovelPhase.Lose://�s�k��̃m�x��

                if(_loseGSSReader.gameObject.activeSelf == false)
                {
                    //�m�x���֌W�̃X�N���v�g���A�^�b�`����Ă���I�u�W�F�N�g��L��������
                    _loseGSSReader.gameObject.SetActive(true);
                }

                if (!_loseGSSReader.IsLoading)
                {
                    //�m�x���̃f�[�^�̃��[�h���I�������
                    _novelCanvas.gameObject.SetActive(true);
                    _uiCanvas.gameObject.SetActive(false);
                }

                if(_loseNovelRenderer.IsNovelFinish)
                {
                    //�m�x���̏����o�������ׂďI�������
                    //�m�x���֌W�̃X�N���v�g���A�^�b�`����Ă���I�u�W�F�N�g�𖳌�������
                    _loseGSSReader.gameObject.SetActive(false);
                    _novelCanvas.gameObject.SetActive(false);

                    _gameOverCanavas.gameObject.SetActive(true);
                }

                break;

            default:
                break;
        }
    }

    /// <summary>
    /// �w�i�̏������s��
    /// </summary>
    void BackGround()
    {
        _backGround.transform.Translate(0f, _scrollSpeed * -Time.deltaTime, 0f);
        _backGroundClone.transform.Translate(0f, _scrollSpeed * -Time.deltaTime, 0f);

        if(_backGround.transform.position.y < _bgInitialPosY - _backGround.bounds.size.y)
        {
            _backGround.transform.Translate(0f, _backGround.bounds.size.y * 2, 0f);
        }

        if(_backGroundClone.transform.position.y < _bgInitialPosY - _backGroundClone.size.y)
        {
            _backGroundClone.transform.Translate(0f, _backGroundClone.size.y * 2, 0f);
        }
    }
}