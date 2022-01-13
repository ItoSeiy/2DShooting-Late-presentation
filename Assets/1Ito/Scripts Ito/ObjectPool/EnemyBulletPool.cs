using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EnemyBullet�̃v�[�����Ǘ�����X�N���v�g
/// </summary>
public class EnemyBulletPool : MonoBehaviour
{
    public static EnemyBulletPool Instance => _instance;
    static EnemyBulletPool _instance;

    [SerializeField] GameObject[] _bullets = null;
    List<BulletParameter> _pool = new List<BulletParameter>();

    [SerializeField] int[] _poolObjectMaxCount = default;
    int _poolObjCountIndex = 0;

    private void Awake()
    {
        _instance = this;
        _poolObjCountIndex = 0;
        CreatePool();
        foreach (var pool in _pool)
        {
            Debug.Log($"�I�u�W�F�N�g��:{pool.Object.name} ���:{pool.Type}");
        }
    }

    private void CreatePool()
    {
        if (_poolObjCountIndex >= _poolObjectMaxCount.Length)
        {
            Debug.Log("���ׂĂ�EnmeyBullet�𐶐����܂����B");
            return;
        }

        for (int i = 0; i < _poolObjectMaxCount[_poolObjCountIndex]; i++)
        {
            var bullet = Instantiate(_bullets[_poolObjCountIndex], this.transform);
            bullet.SetActive(false);
            _pool.Add(new BulletParameter { Object = bullet, Type = (EnemyBulletType)_poolObjCountIndex });
        }

        _poolObjCountIndex++;
        CreatePool();
    }

    /// <summary>
    /// Bullet���g�������Ƃ��ɌĂяo���֐�
    /// </summary>
    /// <param name="position">Bullet�̈ʒu���w�肷��</param>
    /// <param name="bulletType">���˂���Bullet�̎��</param>
    /// <returns></returns>
    public GameObject UseBullet(Vector2 position, EnemyBulletType bulletType)
    {
        foreach (var pool in _pool)
        {
            if (pool.Object.activeSelf == false && pool.Type == bulletType)
            {
                pool.Object.transform.position = position;
                pool.Object.SetActive(true);
                return pool.Object;
            }
        }

        var newBullet = Instantiate(_bullets[(int)bulletType], this.transform);
        newBullet.transform.position = position;
        newBullet.SetActive(true);
        _pool.Add(new BulletParameter { Object = newBullet, Type = bulletType });
        return newBullet;
    }

    private class BulletParameter
    {
        public GameObject Object { get; set; }
        public EnemyBulletType Type { get; set; }
    }
}

public enum EnemyBulletType
{
    Example,
    Example2
}
