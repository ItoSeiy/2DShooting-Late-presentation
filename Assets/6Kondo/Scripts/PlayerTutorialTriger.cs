using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerTutorialTriger : MonoBehaviour
{
    [SerializeField, Header("�A�C�e���̃��C���[��")] string _itemLayerName = default;
    [SerializeField, Header("�{���̃^�O��")] string _bombTag = default;
    [SerializeField, Header("�c�@�̃^�O��")] string _residueTag = default;
    [SerializeField, Header("���G�A�C�e���̃^�O��")] string _invisibleTag = default;
    [SerializeField, Header("�p���[�A�C�e���̃^�O��")] string _powerTag = default;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        string _layerName = LayerMask.LayerToName(collision.gameObject.layer);
        if (_layerName == _itemLayerName)
        {
            Tutorial.Instance.GetItemTutorial();
        }
        if (collision.tag == _bombTag)
        {
            Tutorial.Instance.BombTutorial();
        }
        if (collision.tag == _residueTag)
        {
            Tutorial.Instance.ResidueTutorial();
        }
        if (collision.tag == _invisibleTag)
        {
            Tutorial.Instance.InvisibleTutorial();
        }
        if (collision.tag == _powerTag)
        {
            Tutorial.Instance.PowerTutorial();
        }
    }
}
