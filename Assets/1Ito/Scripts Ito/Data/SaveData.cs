using System;
using System.Linq;

namespace Overdose.Data
{
    [Serializable]
    public class SaveData
    {
        public SaveData(int player1StageCount, int player2StageCount)
        {
            Player1StageActives = new bool[player1StageCount];
            Player2StageActives = new bool[player2StageCount];

            Player1StageActives[0] = true;
            Player2StageActives[0] = true;
        }

        /// <summary>
        /// �v���C���[1�̃X�e�[�W�̉�����
        /// �C���f�b�N�X 0 -> �X�e�[�W1
        /// �C���f�b�N�X 1 -> �X�e�[�W2
        /// �C���f�b�N�X 4 -> �X�e�[�W5
        /// </summary>
        public bool[] Player1StageActives;

        /// <summary>
        /// �v���C���[2�̃X�e�[�W�̉�����
        /// �C���f�b�N�X 0 -> �X�e�[�W1
        /// �C���f�b�N�X 1 -> �X�e�[�W2
        /// �C���f�b�N�X 4 -> �X�e�[�W5
        /// </summary>
        public bool[] Player2StageActives;

    }
}