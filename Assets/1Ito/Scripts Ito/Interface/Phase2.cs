using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2 : MonoBehaviour, IState
{
    void IState.Method1()
    {
        Debug.Log("Phase2�̏�Ԃ�Method1�����s����܂���");
    }

    IState IState.Method2()
    {
        Debug.Log("Phase2�̏�Ԃ�Method2�����s����܂���");
        Debug.Log("Phase2��Phase1�ɑJ�ڂ��܂�");
        return GetComponent<Phase1>();
    }
}
