using UnityEngine;
using System.Threading.Tasks;

public class ObjectActiveChange : MonoBehaviour
{
    [SerializeField]
    [Header("�I�u�W�F�N�g�̃A�N�e�B�u��ύX���鎞��(�~���b)")]
    int _activeChangeTime = 1500;

    [SerializeField]
    [Header("�ύX��̃I�u�W�F�N�g�̃Z�b�g�A�N�e�B�u")]
    bool _active = false;

    async private void OnEnable()
    {
        await Task.Delay(_activeChangeTime);
        gameObject.SetActive(_active);
    }
}
