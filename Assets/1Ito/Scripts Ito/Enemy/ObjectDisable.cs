using UnityEngine;
using System.Threading.Tasks;

public class ObjectDisable : MonoBehaviour
{
    [SerializeField, Header("�I�u�W�F�N�g���A�N�e�B�u�����鎞��(�~���b)")]
    int _disableTime = 1500;
    async private void OnEnable()
    {
        await Task.Delay(_disableTime);
        gameObject.SetActive(false);
    }
}
