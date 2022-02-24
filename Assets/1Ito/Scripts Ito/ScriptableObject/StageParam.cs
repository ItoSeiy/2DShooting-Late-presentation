using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>�X�e�[�W�̃f�[�^���i�[�����N���X</summary>
[Serializable]
public class StageParam
{
    public int BossPhaseIndex => bossPhaseIndex;
    public List<PhaseParm> PhaseParms => phaseParms;
    [SerializeField, Tooltip("�{�X�̃t�F�C�Y��Index���w�肷��(0����n�܂鐔��)")]
    private int bossPhaseIndex;
    [SerializeField]
    private List<PhaseParm> phaseParms = new List<PhaseParm>();
}


/// <summary>�t�F�C�Y�̃f�[�^���i�[�����N���X</summary>
[Serializable]
public class PhaseParm
{
    public GameObject Prefab => phasePrefab;
    public float StartTime => startTime;
    public bool UseInterval => useInterval;
    public float Interval => interval;
    public float FinishTime => finishTime;

    [SerializeField] 
    public string PhaseName = "Phase";
    [SerializeField]
    private GameObject phasePrefab;
    [SerializeField]
    private float startTime;
    [SerializeField, Tooltip("�C���^�[�o����p���ĉ��x���������邩")]
    private bool useInterval;
    [SerializeField, Tooltip("UseInteval�Ƀ`�F�b�N�������Ă��Ȃ��Ǝg�p�ł��Ȃ�")]
    private float interval;
    [SerializeField]
    private float finishTime;
}

