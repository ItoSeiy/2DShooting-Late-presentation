using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2 : MonoBehaviour, IState
{
    void IState.Method1()
    {
        Debug.Log("State2�̏�Ԃ�Method1�����s����܂���");
    }

    IState IState.Method2()
    {
        Debug.Log("State1�̏�Ԃ�Method2�����s����܂���");
        Debug.Log("State��State1�ɑJ�ڂ��܂�");
        return new Phase1();
    }
}
