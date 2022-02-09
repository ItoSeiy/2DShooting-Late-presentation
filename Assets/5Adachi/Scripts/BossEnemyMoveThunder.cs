using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveThunder : MonoBehaviour
{
    Rigidbody2D _rb;
    /// <summary></summary>
    int _thunder = 0;
     float _xMove = 5f;
     float _yMove = 6f;
    /// <summary>スピード</summary>
    [SerializeField,Header("スピード")] float _speed;
    /// <summary>方向</summary>
    Vector2 _dir;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Thunder());
    }
    void Update()
    {
        //transform.position = new Vector2(Mathf.Clamp(transform.position.x, -8f, 8f), (Mathf.Clamp(transform.position.y, -4f, 4f)));
        _thunder++;
        _rb.velocity = _dir.normalized * _speed;
    }
    IEnumerator Thunder()
    {
            yield return new WaitForSeconds(0.1f);
            if (transform.position.x >= 0)//画面右側にいたら
            {
                Debug.Log("left");
                _rb.velocity = Vector2.left;//左に移動
            }
            else//左側にいたら
            {
                Debug.Log("right");
                _rb.velocity = Vector2.right;//右に移動
            }
        
        while (true)
        {   
            yield return new WaitForSeconds(0.1f);
            if (transform.position.x <= -7.5f)
            {
                Debug.Log("nice!");
                _rb.velocity = Vector2.zero;
                break;
            }
            else if (transform.position.x >= 7.5f)
            {
                Debug.Log("nice!");
                _rb.velocity = Vector2.zero;
                break;
            }
        }

    
        Debug.Log("dayone");
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            _thunder++;
            if (transform.position.x <= 7.5f)
            {
                _rb.velocity = new Vector2(_xMove, _yMove);
                if (_thunder >= 10)
                {
                    _thunder = 0;
                    _rb.velocity = new Vector2(_xMove, _yMove);
                    _yMove *= -1;
                }
            }
            else
            {
                _rb.velocity = Vector2.zero;
                _xMove *= -1;
            }
        }
    }
}
