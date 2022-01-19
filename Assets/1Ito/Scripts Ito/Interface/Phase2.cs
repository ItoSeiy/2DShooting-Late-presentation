using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2 : MonoBehaviour, IState
{
    void IState.Method1()
    {
        Debug.Log("Phase2の状態でMethod1が実行されました");
    }

    IState IState.Method2()
    {
        Debug.Log("Phase2の状態でMethod2が実行されました");
        Debug.Log("Phase2がPhase1に遷移します");
        return GetComponent<Phase1>();
    }
}
