using System;
using UnityEngine;

namespace Overdose.Data
{
    /// <summary>
    /// ���ʉ��̏����܂Ƃ߂��N���X
    /// </summary>
    [Serializable]
    public class SFXPoolData
    {
        public SFXData[] Data => soundPoolData;
        [SerializeField] public SFXData[] soundPoolData;
        /// <summary>
        /// �T�E���h���̏�񂪊i�[���ꂽ�N���X
        /// </summary>
        [Serializable]
        public class SFXData
        {
            public SoundType Type => type;
            public AudioSource Audio => audio;
            public int MaxCount => maxCount;

            [SerializeField]
            private string name;
            [SerializeField]
            private SoundType type;
            [SerializeField]
            private AudioSource audio;
            [SerializeField]
            private int maxCount;
        }
    }

    public enum SoundType
    {
        /// <summary>������</summary>
        None,
        /// <summary>��</summary>
        Wind,
        /// <summary>��</summary>
        Sword,
        /// <summary>�L���b�`</summary>
        Catch,
        /// <summary>����</summary>
        Tinnitus,
        /// <summary>�e</summary>
        Gun,
        /// <summary>�{�X1,2,4,5�̎��S�T�E���h</summary>
        BossKilled,
        /// <summary>�{�X�̎��S�T�E���h</summary>
        Boss3Killed,

        Click01,
        Click02,
        Click03,
        Click04,
        Click05,
        Click06,
        Click07,
        Click08,
        Click09,
        Click10,
        Novel,
        ScoreCount,
        EnemyKilled
    }
}


