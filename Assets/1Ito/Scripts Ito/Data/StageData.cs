using System.Collections.Generic;
using UnityEngine;
using System;

namespace Overdose.Data
{
    /// <summary>�X�e�[�W�̃f�[�^���i�[�����N���X</summary>
    [Serializable]
    public class StageData
    {
        public PhaseData[] PhaseParms => phaseParms;
        public AudioClip NormalBGM => _normalBGM;
        public AudioClip BossBGM => _bossBGM;

        [SerializeField]
        private PhaseData[] phaseParms = default;

        [SerializeField]
        private AudioClip _normalBGM;
        [SerializeField]
        private AudioClip _bossBGM;
    }


    /// <summary>�t�F�C�Y�̃f�[�^���i�[�����N���X</summary>
    [Serializable]
    public class PhaseData
    {
        public string PhaseName => PhaseName;
        public GameObject Prefab => phasePrefab;
        public int LoopTime => loopTime; 
        public bool IsBoss => isBoss;

        [SerializeField] 
        private string phaseName = "Phase";
        [SerializeField]
        private GameObject phasePrefab;
        [SerializeField, Header("���̃v���n�u�̐��������[�v���郂�[�h�ł������牽�񐶐����邩")]
        private int loopTime;
        [SerializeField, Header("�{�X��������`�F�b�N��t����")]
        private bool isBoss = false;
    }
}


