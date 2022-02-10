using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>�X�e�[�W�̃f�[�^���i�[�����N���X</summary>
[Serializable]
public class StageParam
{
    public List<PhaseParm> PhaseParms => phaseParms;
    [SerializeField] private List<PhaseParm> phaseParms = new List<PhaseParm>();

}


/// <summary>�t�F�C�Y�̃f�[�^���i�[�����N���X</summary>
[Serializable]
public class PhaseParm
{
    public GameObject Prefab { get => phasePrefab;}
    public float StartTime { get => startTime;}
    public float Interval { get => interval;}
    public float FinishTime { get => finishTime;}

    [SerializeField] public string PhaseName = "Phase";

    [SerializeField] private GameObject phasePrefab;
    [SerializeField] private float startTime;
    [SerializeField] private float interval;
    [SerializeField] private float finishTime;
}

