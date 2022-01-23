using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>�X�e�[�W�̃f�[�^���i�[�����X�N���v�^�u���I�u�W�F�N�g�I�u�W�F�N�g</summary>
[CreateAssetMenu(fileName = "PhaseParam")]
public class StageParamAsset : ScriptableObject
{
    //public List<StageParam> StageParams { get => stageParams; }

    [SerializeField] public List<StageParam> stageParams = new List<StageParam>();
}


/// <summary>�X�e�[�W�̃f�[�^���i�[�����N���X</summary>
[Serializable]
public class StageParam
{
    [SerializeField] string StageName = "Stage";

    [SerializeField] public List<PhaseParm> phaseParms = new List<PhaseParm>();
}


/// <summary>�t�F�C�Y�̃f�[�^���i�[�����N���X</summary>
[Serializable]
public class PhaseParm
{
    public GameObject Prefab { get => phasePrefab;}
    public float BoforeInterval { get => boforeInterval;}
    public float Interval { get => interval;}
    public float AfterInterval { get => afterInterval;}

    [SerializeField] private string PhaseName = "Phase";

    [SerializeField] private GameObject phasePrefab;
    [SerializeField] private float boforeInterval;
    [SerializeField] private float interval;
    [SerializeField] private float afterInterval;
}

