using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1 : MonoBehaviour, IState
{
    void IState.Method1()
    {
        Debug.Log("Phase1�̏�Ԃ�Method1�����s����܂���");
    }

    IState IState.Method2()
    {
        Debug.Log("Phase1�̏�Ԃ�Method2�����s����܂���");
        Debug.Log("Phase��Phase2�ɑJ�ڂ��܂�");
        return GetComponent<Phase2>();
    }
}
