using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseNovelManager : SingletonMonoBehaviour<PhaseNovelManager>
{
    public NovelPhase NovelePhaesState => _novelPhase;

    /// <summary>スプレッドシートシートを読み込むスクリプト(戦闘前)</summary>
    [SerializeField]
    GSSReader _beforeGSSReader;
    /// <summary>ノベルを書き出し、制御するスクリプト(戦闘前)</summary>
    [SerializeField]
    NovelRenderer _beforeNovelRenderer;

    /// <summary>スプレッドシートシートを読み込むスクリプト(勝利時)</summary>
    [SerializeField] 
    GSSReader _winGSSReader;
    /// <summary>ノベルを書き出し、制御するスクリプト(戦闘前)</summary>
    [SerializeField] 
    NovelRenderer _winNovelRenderer;

    /// <summary>スプレッドシートシートを読み込むスクリプト(敗北時)</summary>
    [SerializeField] 
    GSSReader _loseGSSReader;
    /// <summary>ノベルを書き出し、制御するスクリプト(敗北時)</summary>
    [SerializeField] 
    NovelRenderer _loseNovelRenderer;

    /// <summary>ノベルを映し出すキャンバス</summary>
    [SerializeField]
    Canvas _novelCanvas;

    /// <summary>ゲームオーバー時に出すUI</summary>
    [SerializeField]
    Canvas _gameOverCanavas;
    /// <summary>ゲームクリア時に出すUI</summary>
    [SerializeField]
    Canvas _gameClearCanvas;

    /// <summary>常にスコアなどが表示されているキャンバス</summary>
    [SerializeField] 
    Canvas _uiCanvas;

    /// <summary>モブ敵の出現場所</summary>
    [SerializeField] 
    Transform _enemyGeneretePos;
    /// <summary>ボスの出現位置</summary>
    [SerializeField] 
    Transform _bossGeneretePos;

    /// <summary>ノベルの状態</summary>
    [SerializeField]
    NovelPhase _novelPhase = NovelPhase.None;

    /// <summary>背景</summary>
    [SerializeField] 
    SpriteRenderer _backGround;
    /// <summary背景のコピー</summary>
    SpriteRenderer 
    _backGroundClone = null;
    
    /// <summary>背景の親オブジェクト</summary>
    [SerializeField]
    GameObject _backGroundParent;
    /// <summary>背景の単位ベクトル</summary>
    [SerializeField]
    Vector2 _backGroundDir = Vector2.down;
    /// <summary>背景のスクロールするスピード</summary>
    [SerializeField] float _scrollSpeed = 0.5f;
    /// <summary>背景のY座標の初期位置</summary>
    float _bgInitialPosY;

    /// <summary>ステージのデータ</summary>
    [SerializeField] 
    StageParam _stageParam;

    private int _phaseIndex = default;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        _bgInitialPosY = _backGround.transform.position.y;

        _backGroundClone = Instantiate(_backGround, _backGroundParent.transform);
        _backGroundClone.transform.Translate(0f, _backGround.bounds.size.y, 0f);

        const bool ISLOOP = false;
        EnemyGenerate(ISLOOP);
    }

    private void Update()
    {
        switch (_stageParam.PhaseParms[_phaseIndex].IsBoss)
        {
            case true:
                Debug.Log("ボス開始");
                Novel();
                break;

            case false:
                BackGround();

                //ゲームオーバーを判定する
                if(GameManager.Instance.IsGameOver)
                {
                    //ゲームオーバUI表示
                    _gameOverCanavas.gameObject.SetActive(true);
                    //UIキャンバス
                    _uiCanvas.gameObject.SetActive(false);
                }
                break;
        }
    }

    void EnemyGenerate(bool isLoop)
    {
        //ボスのフェイズだったらノベルを生成してから
        if (_stageParam.PhaseParms[_phaseIndex].IsBoss) return;

        //通常のモブ敵の処理
        Instantiate(_stageParam.PhaseParms[_phaseIndex].Prefab).transform.position = _enemyGeneretePos.position;

        //ループをする場合はインデックスをカウントアップしない
        if (isLoop) return;
        _phaseIndex++;
    }

    void Novel<T, T2, T3>(T gssReader, T ) where T : GSSReader where T2 : NovelRenderer where T3 : Canvas
    {
        //ゲームクリアであればノベルのフェイズを変える
        if (GameManager.Instance.IsStageClear) _novelPhase = NovelPhase.Win;
        //ゲームオーバーであればノベルのフェイズを変える
        if (GameManager.Instance.IsGameOver) _novelPhase = NovelPhase.Lose;

        if(T.gameObject.)
        {

        }
    }

    /// <summary>
    /// ノベルの処理を行う
    /// ボスの生成を行う
    /// 勝利,敗北等のUIの表示も行う
    /// </summary>
    void Novel()
    {
        //ゲームクリアであればノベルのフェイズを変える
        if (GameManager.Instance.IsStageClear) _novelPhase = NovelPhase.Win;
        //ゲームオーバーであればノベルのフェイズを変える
        if (GameManager.Instance.IsGameOver) _novelPhase = NovelPhase.Lose;

        switch (_novelPhase)
        {
            case NovelPhase.Before://戦闘前のノベル

                if (_beforeGSSReader.gameObject.activeSelf == false)
                {   
                    //ノベル関係のスクリプトがアタッチされているオブジェクトを有効化する
                    _beforeGSSReader.gameObject.SetActive(true);
                }

                if (!_beforeGSSReader.IsLoading)
                {
                    //ノベルのデータのロードが終わったら
                    _novelCanvas.gameObject.SetActive(true);
                    _uiCanvas.gameObject.SetActive(false);
                }

                if(_beforeNovelRenderer.IsNovelFinish)
                {
                    //ノベルの書き出しがすべて終わったら
                    //ノベル関係のスクリプトがアタッチされているオブジェクトを無効化する
                    _beforeGSSReader.gameObject.SetActive(false);
                    _novelCanvas.gameObject.SetActive(false);

                    _uiCanvas.gameObject.SetActive(true);
                    _novelPhase = NovelPhase.None;
                }
                break;

            case NovelPhase.Win://勝利後のノベル

                if (_winGSSReader.gameObject.activeSelf == false)
                {
                    //ノベル関係のスクリプトがアタッチされているオブジェクトを有効化する
                    _winGSSReader.gameObject.SetActive(true);
                }

                if(!_winGSSReader.IsLoading)
                {
                    //ノベルのデータのロードが終わったら
                    _novelCanvas.gameObject.SetActive(true);
                    _uiCanvas.gameObject.SetActive(false);
                }

                if(_winNovelRenderer.IsNovelFinish)
                {
                    //ノベルの書き出しがすべて終わったら
                    //ノベル関係のスクリプトがアタッチされているオブジェクトを無効化する
                    _winGSSReader.gameObject.SetActive(false);
                    _novelCanvas.gameObject.SetActive(false);

                    _gameClearCanvas.gameObject.SetActive(true);
                }

                break;

            case NovelPhase.Lose://敗北後のノベル

                if(_loseGSSReader.gameObject.activeSelf == false)
                {
                    //ノベル関係のスクリプトがアタッチされているオブジェクトを有効化する
                    _loseGSSReader.gameObject.SetActive(true);
                }

                if (!_loseGSSReader.IsLoading)
                {
                    //ノベルのデータのロードが終わったら
                    _novelCanvas.gameObject.SetActive(true);
                    _uiCanvas.gameObject.SetActive(false);
                }

                if(_loseNovelRenderer.IsNovelFinish)
                {
                    //ノベルの書き出しがすべて終わったら
                    //ノベル関係のスクリプトがアタッチされているオブジェクトを無効化する
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
    /// 背景の処理を行う
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

public enum NovelPhase
{
    /// <summary>ノベルを読み込まない状態</summary>
    None,
    /// <summary>戦闘前ノベル</summary>
    Before,
    /// <summary>戦闘後ノベル</summary>
    Win,
    /// <summary>負けノベル</summary>
    Lose
}