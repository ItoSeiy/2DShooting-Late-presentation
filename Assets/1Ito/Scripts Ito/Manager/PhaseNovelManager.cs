using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseNovelManager : SingletonMonoBehaviour<PhaseNovelManager>
{
    public GamePhase GamePhaseState => _gamePhaseState;
    public NovelPhase NovelePhaesState => _novelPhaseState;

    [SerializeField] NovelRenderer _beforeNovelRenderer;
    [SerializeField] NovelRenderer _winNovelRenderer;
    [SerializeField] NovelRenderer _loseNovelRenderer;

    [SerializeField] Canvas _novelCanvas;

    [SerializeField] Canvas _gameOverCanavas;
    [SerializeField] Canvas _gameClearCanvas;

    [SerializeField] Transform _generateTransform;
    [SerializeField] Transform _bossTransform;

    [SerializeField] GamePhase _gamePhaseState;
    [SerializeField] NovelPhase _novelPhaseState;

    [SerializeField] SpriteRenderer _backGround;
    [SerializeField] GameObject _backGroundParent;
    SpriteRenderer _backGroundClone = null;
    [SerializeField] Vector2 _backGroundDir = new Vector2(0, -1);
    [SerializeField] float _scrollSpeed = 0.5f;
    float _initialPosY;

    [SerializeField] StageParam _stageParam;

    private float _timer = 0;
    private float _intervalTimer = 0;

    private int _phaseIndex = default;

    private bool _isFirstTime = true;

    protected override void Awake()
    {
        base.Awake();
        SetUp();
    }

    void Start()
    {
        _initialPosY = _backGround.transform.position.y;

        _backGroundClone = Instantiate(_backGround, _backGroundParent.transform);
        _backGroundClone.transform.Translate(0f, _backGround.bounds.size.y, 0f);
    }

    private void Update()
    {
        BackGround();
        switch (_gamePhaseState)
        {
            case GamePhase.Boss:
                Debug.Log("�{�X�J�n");
                BossStage();
                break;

            case GamePhase.End:
                Debug.Log("�X�e�[�W�I��");
                break;

            default:
                EnemyGenerate();
                break;
        }
    }

    void EnemyGenerate()
    {
        _timer += Time.deltaTime;
        Debug.Log("�X�^�[�g�ҋ@");

        if (_timer >= _stageParam.PhaseParms[_phaseIndex].StartTime)
        {
            Debug.Log("�����ҋ@");
            _intervalTimer += Time.deltaTime;

            if (_intervalTimer >= _stageParam.PhaseParms[_phaseIndex].Interval || _isFirstTime)
            {
                Debug.Log("����");
                Instantiate(_stageParam.PhaseParms[_phaseIndex].Prefab).transform.position = _generateTransform.position;
                Debug.Log(_stageParam.PhaseParms[_phaseIndex].PhaseName);
                _intervalTimer = 0;
                _isFirstTime = false;
            }

            if (_timer >= _stageParam.PhaseParms[_phaseIndex].FinishTime)
            {
                Debug.Log("�����I��");
                _timer = 0;
                _isFirstTime = true;
                ChangePhase((GamePhase)_phaseIndex + 1);
            }
        }
 
    }

    void BossStage()
    {
        _timer += Time.deltaTime;
        if(_timer >= _stageParam.PhaseParms[_phaseIndex].StartTime)
        {
            SetNovel();

            if(_beforeNovelRenderer.IsNovelFinish && _isFirstTime)
            {
                _novelCanvas.gameObject.SetActive(false);
                _novelPhaseState = NovelPhase.None;
                Instantiate(_stageParam.PhaseParms[_phaseIndex].Prefab).transform.position = _generateTransform.position;
                _isFirstTime = false;
            }
        }
    }

    /// <summary>
    ///�@�X�e�[�W���ǂݍ��܂�邽�тɌĂ΂��
    /// </summary>
    public void SetUp()
    {
        _isFirstTime = true;
        _timer = 0;
        _intervalTimer = 0;

        _gamePhaseState = GamePhase.Phase01;
        _phaseIndex = (int)_gamePhaseState;

        _novelPhaseState = NovelPhase.Before;
    }
    
    void ChangePhase(GamePhase phase)
    {
        _gamePhaseState = phase;

        _phaseIndex = (int)_gamePhaseState;
    }

    void SetNovel()
    {

        if (GameManager.Instance.IsStageClear)
        {
            _novelPhaseState = NovelPhase.After;
        }

        if (GameManager.Instance.IsGameOver)
        {
            _novelPhaseState = NovelPhase.Lose;
        }

        switch (_novelPhaseState)
        {
            case NovelPhase.Before:

                if (_beforeNovelRenderer.gameObject.activeSelf == false || _novelCanvas.gameObject.activeSelf == false)
                {
                    _beforeNovelRenderer.gameObject.SetActive(true);
                    _novelCanvas.gameObject.SetActive(true);
                }
                break;

            case NovelPhase.After:

                if(_winNovelRenderer.gameObject.activeSelf == false || _novelCanvas.gameObject.activeSelf == false)
                {
                    _winNovelRenderer.gameObject.SetActive(true);
                    _novelCanvas.gameObject.SetActive(true);
                }
                break;

            case NovelPhase.Lose:

                if(_loseNovelRenderer.gameObject.activeSelf == false || _novelCanvas.gameObject.activeSelf == false)
                {
                    _loseNovelRenderer.gameObject.SetActive(true);
                    _novelCanvas.gameObject.SetActive(true);
                }

                if(_loseNovelRenderer.IsNovelFinish)
                {

                }

                break;
            default:
                return;
        }
    }

    void BackGround()
    {
        if (_gamePhaseState == GamePhase.Boss) return;

        _backGround.transform.Translate(0f, _scrollSpeed * -Time.deltaTime, 0f);
        _backGroundClone.transform.Translate(0f, _scrollSpeed * -Time.deltaTime, 0f);

        if(_backGround.transform.position.y < _initialPosY - _backGround.bounds.size.y)
        {
            _backGround.transform.Translate(0f, _backGround.bounds.size.y * 2, 0f);
        }

        if(_backGroundClone.transform.position.y < _initialPosY - _backGroundClone.size.y)
        {
            _backGroundClone.transform.Translate(0f, _backGroundClone.size.y * 2, 0f);
        }
    }
}

public enum GamePhase
{
    Phase01,
    Phase02,
    Phase03,
    Phase04,
    Phase05,
    Boss,
    End
}

public enum NovelPhase
{
    /// <summary>�퓬�O�C�x���g(�f�t�H���g)</summary>
    Before,
    /// <summary>�퓬��C�x���g</summary>
    After,
    /// <summary>�����C�x���g</summary>
    Lose,
    /// <summary>�m�x����ǂݍ��܂Ȃ����</summary>
    None
}