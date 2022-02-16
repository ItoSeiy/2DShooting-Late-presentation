using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveThunder : MonoBehaviour
{
    /// <summary>形状や大きさの概念を持った物質</summary>
    Rigidbody2D _rb;
    /// <summary>水平、横方向</summary>
    const float HORIZONTAL = 1f;
    /// <summary>垂直、縦方向</summary>
    float _vertical = 1f;
    /// <summary>スピード</summary>
    [SerializeField, Header("スピード")] float _speed;
    /// <summary>方向</summary>
    Vector2 _dir;
    /// <summary>中央位置</summary>
    float _middlePos = 0;
    /// <summary>判定の際に待ってほしい時間</summary>
    const float DUDGMENT_TIME = 0.1f;
    /// <summary>停止時間</summary>
    [SerializeField, Header("停止時間")] float _stopTime = 2f;
    /// <summary>右限</summary>
    [SerializeField, Header("右限")] float _rightLimit = 7.5f;
    /// <summary>左限</summary>
    [SerializeField, Header("左限")] float _leftLimit = -7.5f;
    /// <summary>上限</summary>
    [SerializeField, Header("上限")] float _upperLimit = 4f;
    /// <summary>下限</summary>
    [SerializeField, Header("下限")] float _lowerLimit = -4f;
    /// <summary>逆の動き</summary>
    const float REVERSE_MOVEMENT = -1f;
    /// <summary>現在のパターン</summary>
    int _pattern = 0;
    /// <summary>最初に左にいるパターン</summary>
    const int PATTERN1 = 1;
    /// <summary>最初に右にいるパターン</summary>
    const int PATTERN2 = 2;
    /// <summary>タイマーのリセット用</summary>
    const float TIMER_RESET = 0f;
    /// <summary>時間</summary>
    float _timer = 0f;
    /// <summary>時間制限,上下移動を逆にする時間<summary>
    [SerializeField, Header("上下移動を逆にする時間")] float _timeLimit = 0.5f;
    /// <summary>正常位置に軌道修正する</summary>
    bool _fix = false;
    /// <summary>上に上がる</summary>
    const float MOVEUP = 1f;
    /// <summary>下にサガる</summary>
    const float MOVEDOWN = -1f;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Thunder());
        _fix = true;
    }
    void Update()
    {
        _rb.velocity = _dir.normalized * _speed;//その方向に移動
        _timer += Time.deltaTime;//時間
    }

    /// <summary>
    /// 端に一直線に移動した後、反対側に着くまでジグザグ移動する
    /// </summary>
    IEnumerator Thunder()
    {
        if (transform.position.x >= _middlePos)//画面右側にいたら
        {
            Debug.Log("left");
            _dir = Vector2.left;//左に移動
        }
        else//左側にいたら
        {
            Debug.Log("right");
            _dir = Vector2.right;//右に移動
        }

        //端についたら停止
        while (true)
        {
            yield return new WaitForSeconds(DUDGMENT_TIME);//判定回数の制御

            if (transform.position.x <= _leftLimit)//左についたら
            {
                Debug.Log("a");
                _pattern = PATTERN1;//パターン1に切り替え
                _dir = Vector2.zero;//停止
                yield return new WaitForSeconds(_stopTime);//停止時間
                break;
            }
            else if (transform.position.x >= _rightLimit)//右についたら
            {
                Debug.Log("a");
                _pattern = PATTERN2;//パターン2に切り替え
                _dir = Vector2.zero;//停止
                yield return new WaitForSeconds(_stopTime);//停止時間
                break;
            }
        }

        _timer = TIMER_RESET;//タイムをリセット

        //ジグザクする動き

        //左から右にジグザグ動く
        while (true && _pattern == PATTERN1)
        {
            Debug.Log("1");
            yield return new WaitForSeconds(DUDGMENT_TIME);//判定回数の制御

            if (transform.position.x <= _rightLimit)//端についていないなら繰り返す
            {
                _dir = new Vector2(HORIZONTAL, _vertical);//右上or右下に動きながら

                if (_timer >= _timeLimit)//制限時間になったら
                {
                    _timer = TIMER_RESET;//タイムをリセット
                    _vertical *= REVERSE_MOVEMENT;//上下の動きを逆にする                   
                }

                //画面外に行きそうになったら１度だけ軌道修正する
                else if (transform.position.y >= _upperLimit && _fix)
                {
                    _vertical = MOVEDOWN;//下にサガる動きにする   
                    _timer = TIMER_RESET;//タイムをリセット
                    _fix = false;//使えないようにする
                    Debug.Log("3");
                }
                else if (transform.position.y <= _lowerLimit && _fix)
                {
                    _vertical = MOVEUP;//上の動きにする   
                    _timer = TIMER_RESET;//タイムをリセット
                    _fix = false;//使えないようにする
                    Debug.Log("4");
                }
            }
            else
            {
                _dir = Vector2.zero;//停止
                break;
            }
        }

        //右から左にジグザグ動く
        while (true && _pattern == PATTERN2)
        {
            Debug.Log("2");
            yield return new WaitForSeconds(DUDGMENT_TIME);//判定回数の制御

            if (transform.position.x >= _leftLimit)//端についていないなら繰り返す
            {
                _dir = new Vector2(-HORIZONTAL, _vertical);//左上or左下に動きながら

                if (_timer >= _timeLimit)//制限時間になったら
                {
                    Debug.Log("4");
                    _timer = TIMER_RESET;//タイムをリセット
                    _vertical *= REVERSE_MOVEMENT;//上下の動きを逆にする   
                }

                //画面外に行きそうになったら１度だけ軌道修正する
                else if (transform.position.y >= _upperLimit && _fix)
                {
                    _vertical = MOVEDOWN;//下にサガる動きにする   
                    _timer = TIMER_RESET;//タイムをリセット
                    _fix = false;//使えないようにする
                    Debug.Log("3");
                }
                else if (transform.position.y <= _lowerLimit && _fix)
                {
                    _vertical = MOVEUP;//上に上がる動きにする   
                    _timer = TIMER_RESET;//タイムをリセット
                    _fix = false;//使えないようにする
                    Debug.Log("3");
                }
            }
            else//端についたら
            {
                _dir = Vector2.zero;//停止                
                break;
            }
        }

        _fix = true;//使えるようにする
    }
}
