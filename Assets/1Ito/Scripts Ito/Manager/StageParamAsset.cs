using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary></summary>
[CreateAssetMenu(fileName = "PhaseParam")]
public class StageParamAsset : ScriptableObject
{
    [SerializeField] public List<StageParam> StageParams = new List<StageParam>();
}


/// <summary>�t�F�C�Y�̃f�[�^���i�[�����N���X</summary>
[System.Serializable]
public class StageParam
{
    public List<PhaseParm> PhaseParms { get => phaseParms; set => phaseParms = value; }

    [SerializeField] string Stage = "Stage";

    [SerializeField] private List<PhaseParm> phaseParms = new List<PhaseParm>();
}


/// <summary>�t�F�C�Y���̂��̂̃f�[�^���i�[�����N���X</summary>
[System.Serializable]
public class PhaseParm
{
    public GameObject PhasePrefab { get => phasePrefab;}
    public float BoforeInterval { get => boforeInterval;}
    public float Interval { get => interval;}
    public float AfterInterval { get => afterInterval;}

    [SerializeField] private string Phase = "Phase";

    [SerializeField] private GameObject phasePrefab;
    [SerializeField] private float boforeInterval;
    [SerializeField] private float interval;
    [SerializeField] private float afterInterval;
}

