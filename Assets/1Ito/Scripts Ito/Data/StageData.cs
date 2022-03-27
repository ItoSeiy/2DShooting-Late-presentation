using System;
using UnityEngine;

namespace Overdose.Data
{
    /// <summary>�X�e�[�W�̃f�[�^���i�[�����N���X</summary>
    [CreateAssetMenu(fileName = "StageData")]
    public class StageData : ScriptableObject
    {
        public string SceneName => _sceneName;
        public AudioClip NormalBGM => _normalBGM;
        public AudioClip BossBGM => _bossBGM;
        public int PlayerNum => _playerNum;
        public int StageNum => _stageNum;
        public PhaseData[] PhaseParms => _phaseParms;

        [SerializeField]
        private string _sceneName;

        [SerializeField]
        private AudioClip _normalBGM;
        [SerializeField]
        private AudioClip _bossBGM;

        [SerializeField]
        private int _playerNum;
        [SerializeField]
        private int _stageNum;

        [SerializeField]
        private PhaseData[] _phaseParms = default;

    }


    /// <summary>�t�F�C�Y�̃f�[�^���i�[�����N���X</summary>
    [Serializable]
    public class PhaseData
    {
        public string PhaseName => PhaseName;
        public GameObject Prefab => _phasePrefab;
        public int LoopTime => _loopTime; 
        public bool IsBoss => _isBoss;

        [SerializeField] 
        private string _phaseName = "Phase";
        [SerializeField]
        private GameObject _phasePrefab;
        [SerializeField] 
        [Header("���̃v���n�u�̐��������[�v���郂�[�h�ł������牽�񐶐����邩")]
        private int _loopTime;
        [SerializeField] 
        [Header("�{�X��������`�F�b�N��t����")]
        private bool _isBoss = false;
    }
}


