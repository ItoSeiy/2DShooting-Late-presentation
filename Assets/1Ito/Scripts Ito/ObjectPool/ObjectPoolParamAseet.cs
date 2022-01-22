using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>�v�[������I�u�W�F�N�g���i�[�����X�N���v�^�u���I�u�W�F�N�g</summary>
[CreateAssetMenu(fileName = )]
public class ObjectPoolParamAseet : MonoBehaviour
{
    public List<PoolObjectParam> PoolObjectParams { get => poolObjectParams;}

    [SerializeField] private List<PoolObjectParam> poolObjectParams = new List<PoolObjectParam>();
}

/// <summary>�v�[������I�u�W�F�N�g�̃f�[�^���i�[�����N���X</summary>
[Serializable]
public class PoolObjectParam
{
    public GameObject ObjectPrefab { get => objectPrefab;}
    public PlayerBulletType ObjectType { get => objectType;}

    [SerializeField] private string objectName;
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private PlayerBulletType objectType;
}

