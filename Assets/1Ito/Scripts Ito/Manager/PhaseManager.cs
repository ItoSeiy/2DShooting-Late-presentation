using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    private IState _state;

    private void Start()
    {
        _state = GetComponent<Phase1>();
        ExcuteMethod2();
        CurrentState();
        ExcuteMethod1();
    }

    // ���݂�State��Ԃ��܂��B
    public void CurrentState()
    {
        Debug.Log($"���݂�State��{_state.GetType().Name}�ł�");
    }

    // State��ύX���܂��B
    public void ChangeState(IState state)
    {
        _state = state;
        Debug.Log($"State��{ _state.GetType().Name}�ɕύX����܂���");
    }

    // ���ꂼ���State�ɂ�����Method�����s���܂��B
    public void ExcuteMethod1()
    {
        _state.Method1();
    }

    public void ExcuteMethod2()
    {
        _state = _state.Method2();
    }
}
