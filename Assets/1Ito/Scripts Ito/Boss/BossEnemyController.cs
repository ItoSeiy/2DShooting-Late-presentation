using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyController : EnemyBase
{
    /// <summary> �G�̍s���f�[�^ </summary>
    public BossData Data => _data;
    /// <summary> �A�j���[�^�[ </summary>
    public Animator Animator { get; private set; } = default;

    [SerializeField]
    Transform[] _muzzles = null;
    /// <summary> �{�X�s���f�[�^ </summary>
    [SerializeField]
    BossData _data = null;

    /// <summary> ���݂̍s�� </summary>
    private BossAction _currentBossAction = default;
    /// <summary> �Ō�ɍs�����s���p�^�[���C���f�b�N�X </summary>
    private int _lastPattern = -1;
    /// <summary> �v���C���[�Q�Ɩ{�� </summary>
    private GameObject _player = default;
    /// <summary> �G�̃��f�� </summary>
    public GameObject Model { get => gameObject; }

    public GameObject PlayerObj
    {
        get
        {
            if (_player == null)
            {
                Debug.LogWarning("player�^�O���������I�u�W�F�N�g������܂���B\n�ǉ����Ă�������");
                return null;
            }
            return _player;
        }
    }

    protected override void Attack()
    {
        //ObjectPool.Instance(_);
    }

    protected override void OnGetDamage()
    {
        throw new System.NotImplementedException();
    }


}
