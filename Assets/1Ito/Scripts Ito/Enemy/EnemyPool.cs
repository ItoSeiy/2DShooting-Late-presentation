using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool Instance => _instance;
    static EnemyPool _instance;

    [SerializeField] GameObject[] _enemies = null;
    List<EnemyParameter> _pool = new List<EnemyParameter>();

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

    public void CreatePool()
    {
        if (_poolObjCountIndex >= _poolObjectMaxCount.Length)
        {
            Debug.Log("���ׂĂ�Enemy�𐶐����܂����B");
            return;
        }

        for (int i = 0; i < _poolObjectMaxCount[_poolObjCountIndex]; i++)
        {
            var enemy = Instantiate(_enemies[_poolObjCountIndex], this.transform);
            enemy.SetActive(false);
            _pool.Add(new EnemyParameter { Object = enemy, Type = (EnemyType)_poolObjCountIndex});
        }

        _poolObjCountIndex++;
        CreatePool();
    }

    /// <summary>
    /// Enemy�𐶐����������ɌĂяo���֐�
    /// </summary>
    /// <param name="position">�̈ʒu���w�肷��</param>
    /// <param name="enemyType">��������Enemy�̎��</param>
    /// <returns></returns>
    public GameObject UseEnemy(Vector2 position, EnemyType enemyType)
    {
        foreach (var pool in _pool)
        {
            if (pool.Object.activeSelf == false && pool.Type == enemyType)
            {
                pool.Object.transform.position = position;
                pool.Object.SetActive(true);
                return pool.Object;
            }
        }

        var newBullet = Instantiate(_enemies[(int)enemyType], this.transform);
        newBullet.transform.position = position;
        newBullet.SetActive(true);
        _pool.Add(new EnemyParameter { Object = newBullet, Type = enemyType });
        return newBullet;
    }

    public class EnemyParameter
    {
        public GameObject Object { get; set; }
        public EnemyType Type { get; set; }
    }
}

public enum EnemyType
{
    Straight,
}

