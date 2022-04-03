using UnityEngine;
using System.Linq;

/// <summary>
/// �����̎q���������I�u�W�F�N�g���I�u�W�F�N�g�v�[���ɑΉ��ł���悤�ɂ���X�N���v�g
/// </summary>
public class BulletParent : MonoBehaviour
{
    BulletBese[] _bulletsChildren = new BulletBese[99];
    Vector2[] _initialBulletChildrenPos = new Vector2[99];
    bool[] _isBulletChildrenActive;
    private void Awake()
    {
        _bulletsChildren = GetComponentsInChildren<BulletBese>();
        for (int i = 0; i < _bulletsChildren.Length; i++)
        {
            _initialBulletChildrenPos[i] = _bulletsChildren[i].transform.position;
        }
    }

    void OnDisable()
    {
        for (int i = 0; i < _bulletsChildren.Length; i++)
        {
            _bulletsChildren[i].transform.localPosition = _initialBulletChildrenPos[i];
        }
    }

    public void ChildrenBulletEnable()
    {
        foreach (var bulletsChildren in _bulletsChildren)
        {
             bulletsChildren.gameObject.SetActive(true);
        }
    }

    public bool AllBulletChildrenDisable()
    {
        var allBulletChildrenActive = _bulletsChildren.All(i => i.gameObject.activeSelf == false);
        return allBulletChildrenActive;
    }
}
