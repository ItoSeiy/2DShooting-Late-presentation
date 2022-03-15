using UnityEngine;
using System;

/// <summary>�v�[������I�u�W�F�N�g���i�[�����X�N���v�^�u���I�u�W�F�N�g</summary>
[CreateAssetMenu(fileName = "PoolObjectParam")]
public class PoolObjectParamAsset : ScriptableObject
{
    public PoolObjectParam[] Params => poolObjectParams;

    [SerializeField]
    private PoolObjectParam[] poolObjectParams = default;
}

/// <summary>�v�[������I�u�W�F�N�g�̃f�[�^���i�[�����N���X</summary>
[Serializable]
public class PoolObjectParam
{
    public GameObject Prefab { get => objectPrefab;}
    public PoolObjectType Type { get => objectType;}
    public int MaxCount { get => objectMaxCount;}
    [SerializeField]
    private string Name;
    [SerializeField]
    private PoolObjectType objectType;
    [SerializeField]
    private GameObject objectPrefab;
    [SerializeField]
    private int objectMaxCount;
}

