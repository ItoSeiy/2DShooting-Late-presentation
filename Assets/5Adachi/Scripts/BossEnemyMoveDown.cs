using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveDown : MonoBehaviour
{
    Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Down());
    }

    public IEnumerator Down()
    {
        if (transform.position.x < 0)//左にいたら
        {
            _rb.velocity = new Vector2(-7, 0);
            Debug.Log("a");
        }
        else                         //右にいたら
        {
            _rb.velocity = new Vector2(7, 0);
            Debug.Log("b");
        }

        

        while(true)//端に着くまで下がる
        {
            yield return new WaitForSeconds(0.1f);
            if (transform.position.x <= -7.5f)
            {
                Debug.Log("c");
                _rb.velocity = new Vector2(0, -3);
                break;
            }
            else if (transform.position.x >= 7.5f)
            {
                Debug.Log("d");
                _rb.velocity = new Vector2(0, -3);
                break;
            }
        }


        while (true)//反対側に着くまで移動する
        {
            yield return new WaitForSeconds(0.1f);

            if (transform.position.y <= -3 && transform.position.x <= -7.5f)
            {
                Debug.Log("e");
                _rb.velocity = new Vector2(7, 0);
                break;
            }
            else if (transform.position.y <= -3 && transform.position.x >= 7.5f)
            {
                Debug.Log("f");
                _rb.velocity = new Vector2(-7, 0);
                break;
            }
        }

        yield return new WaitForSeconds(2);

        while (true)//上に上がる
        {
            Debug.Log("g");

            yield return new WaitForSeconds(0.1f);
            _rb.velocity = new Vector2(0, 5);

            if (transform.position.y >= 3.5f)
            {
                Debug.Log("h");
                break;
            }

        }

        while(true)//いい感じの位置に
        {
            if (transform.position.x < 0)
            {
                _rb.velocity = new Vector2(3, 0);
                Debug.Log("a");
                break;
            }
            else
            {
                _rb.velocity = new Vector2(-3, 0);
                Debug.Log("b");
                break;
            }
        }
        yield return new WaitForSeconds(Random.Range(1, 3));
        _rb.velocity = new Vector2(0, 0);
    }
}
