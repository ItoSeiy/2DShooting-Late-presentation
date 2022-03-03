using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>�X�e�[�W�̃f�[�^���i�[�����N���X</summary>
[Serializable]
public class StageParam
{
    public List<PhaseParm> PhaseParms => phaseParms;

    [SerializeField]
    private List<PhaseParm> phaseParms = new List<PhaseParm>();
}


/// <summary>�t�F�C�Y�̃f�[�^���i�[�����N���X</summary>
[Serializable]
public class PhaseParm
{
    public string PhaseName => PhaseName;
    public GameObject Prefab => phasePrefab;
    public bool IsBoss => _isBoss;

    [SerializeField] 
    private string phaseName = "Phase";
    [SerializeField]
    private GameObject phasePrefab;
    [SerializeField, Header("�{�X��������`�F�b�N��t����")]
    private bool _isBoss = false;
}

