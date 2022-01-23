using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>�v�[������I�u�W�F�N�g���i�[�����X�N���v�^�u���I�u�W�F�N�g</summary>
[CreateAssetMenu(fileName = "PoolObjectParam")]
public class PoolObjectParamAsset : ScriptableObject
{
    public List<PoolObjectParam> Params { get => poolObjectParams;}

    [SerializeField] private List<PoolObjectParam> poolObjectParams = new List<PoolObjectParam>();
}

/// <summary>�v�[������I�u�W�F�N�g�̃f�[�^���i�[�����N���X</summary>
[Serializable]
public class PoolObjectParam
{
    public GameObject Prefab { get => objectPrefab;}
    public PlayerBulletType Type { get => objectType;}
    public int MaxCount { get => objectMaxCount;}

    [SerializeField] private string objectName;
    [SerializeField] private PlayerBulletType objectType;
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private int objectMaxCount;
}

