using UnityEngine;

namespace Overdose.Calculation
{
    /// <summary>
    /// �v�Z������N���X
    /// </summary>
    public static class Calculator
    {
        /// <summary>
        /// �n���ꂽ�m����bool true ��Ԃ�
        /// </summary>
        /// <param name="probability">true��Ԃ��m��  min -> 0 max -> 100</param>
        /// <returns></returns>
        public static bool RandomBool(int probability)
        {
            if(probability < 0 || probability > 100)
            {
                Debug.LogWarning($"Calculator.RandomBool�ɓn���ꂽ�m���̒l���傫�����͏������\��������܂�\n�n���ꂽ�m��{probability}");
            }

            if(Random.Range(0, 100) < probability)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}