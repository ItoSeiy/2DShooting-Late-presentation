using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseNovelManager : SingletonMonoBehaviour<PhaseNovelManager>
{

    public NovelPhase NovelePhaesState => _novelPhaseState;

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
    Transform _generateTransform;
    /// <summary>�{�X�̏o���ʒu</summary>
    [SerializeField] 
    Transform _bossgenerateTransform;

    /// <summary>�m�x���̏��</summary>
    [SerializeField]
    NovelPhase _novelPhaseState = NovelPhase.None;

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

    private bool _isGenerateFirstTime = true;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        _bgInitialPosY = _backGround.transform.position.y;

        _backGroundClone = Instantiate(_backGround, _backGroundParent.transform);
        _backGroundClone.transform.Translate(0f, _backGround.bounds.size.y, 0f);

        EnemyGenerate();
    }

    private void Update()
    {
        switch (_stageParam.PhaseParms[_phaseIndex].IsBoss)
        {
            case true:
                Debug.Log("�{�X�J�n");
                _novelPhaseState = NovelPhase.Before;

                break;

            case false:
                BackGround();

                //�Q�[���I�[�o�[�𔻒肷��
                if(GameManager.Instance.IsGameOver)
                {
                    //�Q�[���I�[�oUI�\��
                    _gameOverCanavas.gameObject.SetActive(true);
                    //UI�L�����o�X
                    _uiCanvas.gameObject.SetActive(false);
                    return;
                }
                break;
        }
    }

    void EnemyGenerate()
    {

    }

    /// <summary>
    /// �{�X�X�e�[�W�̏���
    /// </summary>
    void BossStage()
    {
        if (GameManager.Instance.IsStageClear)
        {
            _novelPhaseState = NovelPhase.Win;
        }

        if (GameManager.Instance.IsGameOver)
        {
            _novelPhaseState = NovelPhase.Lose;
        }
    }


    void Novel(NovelPhase novelPhase)
    {
        switch (novelPhase)
        {
            case NovelPhase.Before:

                if (_beforeNovelRenderer.gameObject.activeSelf == false)
                {
                    _beforeNovelRenderer.gameObject.SetActive(true);
                }

                if (!_beforeGSSReader.IsLoading)
                {
                    _novelCanvas.gameObject.SetActive(true);
                }

                if(_beforeNovelRenderer.IsNovelFinish)
                {
                    _novelCanvas.gameObject.SetActive(false);
                    _beforeNovelRenderer.gameObject.SetActive(false);
                    _uiCanvas.gameObject.SetActive(false);
                    _novelPhaseState = NovelPhase.None;
                    Instantiate(_stageParam.PhaseParms[_phaseIndex].Prefab).transform.position = _bossgenerateTransform.position;
                }

                break;

            case NovelPhase.Win:

                if(_winNovelRenderer.gameObject.activeSelf == false)
                {
                    _winNovelRenderer.gameObject.SetActive(true);
                    _novelCanvas.gameObject.SetActive(true);
                }

                if(!_winGSSReader.IsLoading)
                {
                    _novelCanvas.gameObject.SetActive(true);
                }

                if(_winNovelRenderer.IsNovelFinish)
                {
                    _novelCanvas.gameObject.SetActive(false);
                    _winNovelRenderer.gameObject.SetActive(false);
                    _uiCanvas.gameObject.SetActive(false);
                    _gameClearCanvas.gameObject.SetActive(true);
                }

                break;

            case NovelPhase.Lose:

                if(_loseNovelRenderer.gameObject.activeSelf == false)
                {
                    _loseNovelRenderer.gameObject.SetActive(true);
                }

                if (!_loseGSSReader.IsLoading)
                {
                    _novelCanvas.gameObject.SetActive(true);
                }

                if(_loseNovelRenderer.IsNovelFinish)
                {
                    _novelCanvas.gameObject.SetActive(false);
                    _loseNovelRenderer.gameObject.SetActive(false);
                    _uiCanvas.gameObject.SetActive(false);
                    _gameOverCanavas.gameObject.SetActive(true);
                }

                break;
        }
    }

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

public enum NovelPhase
{
    /// <summary>�m�x����ǂݍ��܂Ȃ����</summary>
    None,
    /// <summary>�퓬�O�m�x��</summary>
    Before,
    /// <summary>�퓬��m�x��</summary>
    Win,
    /// <summary>�����m�x��</summary>
    Lose
}