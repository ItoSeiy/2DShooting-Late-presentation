using System;
using UnityEngine;

[Serializable]
public class BossData
{
    /// <summary> �p�^�[���̔z����� </summary>
    [SerializeField]
    private Actions[] _actionPattern = default;

    /// <summary> �p�^�[���̎Q�� </summary>
    public Actions[] ActionPattern => _actionPattern;
}

[Serializable]
public class Actions
{
    /// <summary> �s���̔z�� </summary>
    [SerializeField]
    private BossAction[] _bossActions = default;
    /// <summary> �s���z��̎Q�� </summary>
    public BossAction[] BossActions => _bossActions;
}