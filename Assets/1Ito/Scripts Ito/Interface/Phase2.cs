using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2 : MonoBehaviour, IState
{
    void IState.Method1()
    {
        Debug.Log("State2の状態でMethod1が実行されました");
    }

    IState IState.Method2()
    {
        Debug.Log("State1の状態でMethod2が実行されました");
        Debug.Log("StateがState1に遷移します");
        return new Phase1();
    }
}
