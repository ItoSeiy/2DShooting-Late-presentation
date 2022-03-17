using Overdose.Data;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class SuperAttackPrison : BossAttackAction
{
    /// <summary>����</summary>
    Vector3 _dir;  
    /// <summary>�E���͈̔�</summary>
    bool _rightRange;
    /// <summary>�����͈̔�</summary>
    bool _leftRange;
    /// <summary>�㑤�͈̔�</summary>
    bool _upperRange;
    /// <summary>�����͈̔�</summary>
    bool _downRange;
    /// <summary>�^�C�}�[</summary>
    float _timer = 0f;
    /// <summary>������</summary>
    float _horizontalDir = 0f;
    /// <summary>�c����</summary>
    float _verticalDir = 0f;
    /// <summary>�ʏ펞�̔�_���[�W�̊�����ۑ�����</summary>
    float _saveDamageTakenRation = 1f;
    /// <summary>�e�̌����ڂ̎��</summary>
    int _firstPattern = 0;
    /// <summary>�e�̌����ڂ̎��</summary>
    int _secondPattern = 0;
    /// <summary>�e�̌����ڂ̎��</summary>
    int _thirdPattern = 0;
    /// <summary>��]����</summary>
    bool _rotDir = false;
    /// <summary>�K�E�O�Ɉړ�����|�W�V����</summary>
    [SerializeField, Header("�K�E�O�Ɉړ�����|�W�V����")] Vector2 _superAttackPosition = new Vector2(0f, 4f);
    /// <summary>�o���b�g�𔭎˂���|�W�V����</summary>
    [SerializeField, Header("Bullet�𔭎˂���|�W�V����")] Transform[] _muzzles = null;
    /// <summary>���x</summary>
    [SerializeField, Header("�X�s�[�h")] float _speed = 4f;  
    /// <summary>�K�E�Z�ҋ@����</summary>
    [SerializeField, Header("�K�E�Z�ҋ@����")] float _waitTime = 5f;
    /// <summary>�K�E�Z��������</summary>
    [SerializeField, Header("�K�E�Z��������")] float _activationTime = 30f;
    /// <summary>�U���p�x</summary>
    [SerializeField, Header("�U���p�x(�b)")] private float _attackInterval = 0.6f;
    /// <summary>�}�Y���̊p�x�Ԋu</summary>
    [SerializeField, Header("�}�Y���̊p�x�Ԋu")] float _angleInterval = 3f;
    /// <summary>���˂���e��ݒ�ł���</summary>
    [SerializeField, Header("���˂���e�̐ݒ�")] PoolObjectType[] _bullet;
    /// <summary>��]���n�߂鎞��</summary>
    [SerializeField,Header("��]���n�߂鎞��")] float _timeLimit = 5f;
    /// <summary>����]�̌��E</summary>
    [SerializeField, Header("����]�̌��E")] float _leftRotLimit = 270f;
    /// <summary>�E��]�̌��E</summary>
    [SerializeField, Header("�E��]�̌��E")] float _rightRotLimit = 180f;
    /// <summary>��_���[�W�̊���</summary>
    [SerializeField, Header("��_���[�W�̊���"), Range(0, 1)] float _damageTakenRationRange = 0.5f;
    /// <summary>�{�X�̕K�E�Z�̃^�C�����C��</summary>
    [SerializeField, Header("�{�X�̕K�E�Z�̃^�C�����C��")] PlayableDirector _Introduction = null;
    /// <summary>�U�����̉�</summary>
    [SerializeField, Header("�U�����̉�")] SoundType _superAttack;
    /// <summary>�^�C�����C������������</summary>
    [SerializeField,Header("�^�C�����C������������")]�@float _introductionStopTime = 3f;
    /// <summary>�C���l</summary>
    const float PLAYER_POS_OFFSET = 0.5f;
    /// <summary>����񐔂̐���</summary>
    const float JUDGMENT_TIME = 1 / 60f;
    /// <summary>���Z�b�g�^�C�}�[</summary>
    const float RESET_TIME = 0f;
   
    public override System.Action ActinoEnd { get; set; }
   
    public override void Enter(BossController contlloer)
    {
        contlloer.ItemDrop();
        //�ʏ펞�̔�_���[�W�̊�����ۑ�����
        _saveDamageTakenRation = contlloer.DamageTakenRation;
        //��_���[�W�̊�����ύX����
        contlloer.DamageTakenRation = _damageTakenRationRange;
        StartCoroutine(Prison(contlloer));//�R���[�`���𔭓�
    }

    public override void ManagedUpdate(BossController contlloer)
    {
        _timer += Time.deltaTime;//�^�C�}�[

        if (_timer >= _activationTime)
        {
            ActinoEnd?.Invoke();
        }
    }

    public override void Exit(BossController contlloer)
    {
        //��_���[�W�̊������������ɖ߂�
        contlloer.DamageTakenRation = _saveDamageTakenRation;
        StopAllCoroutines();
    }

    /// <summary>player������߂�player����������ɒe�𔭎˂���</summary>
    IEnumerator Prison(BossController controller)
    {
        _timer = RESET_TIME;//�^�C�����Z�b�g

        //�K�E����Ƃ���BOSS�͕��O�ɂ���0�A�x��2���̈ʒu(��)�ɁA�ړ�����
        while (true)
        {
            yield return new WaitForSeconds(JUDGMENT_TIME);//����񐔂̐���
            //������
            _horizontalDir = _superAttackPosition.x - controller.transform.position.x;
            //�c����
            _verticalDir = _superAttackPosition.y - controller.transform.position.y;
            //���͈̔͂̏�����      
            _rightRange = controller.transform.position.x < _superAttackPosition.x + PLAYER_POS_OFFSET;
            _leftRange = controller.transform.position.x > _superAttackPosition.x - PLAYER_POS_OFFSET;
            //�c�͈̔͂̏�����
            _upperRange = controller.transform.position.y < _superAttackPosition.y + PLAYER_POS_OFFSET;
            _downRange = controller.transform.position.y > _superAttackPosition.y - PLAYER_POS_OFFSET;
            //�s�������|�W�V�����Ɉړ�����
            //�߂�������
            if (_rightRange && _leftRange && _upperRange && _downRange)
            {
                Debug.Log("���ʂ�" + _rightRange + _leftRange + _upperRange + _downRange);
                //�X���[�Y�Ɉړ�
                controller.Rb.velocity = new Vector2(_horizontalDir, _verticalDir) * _speed;
            }
            //����������
            else
            {
                Debug.Log("���ʂ�" + _rightRange + _leftRange + _upperRange + _downRange);
                //���肵�Ĉړ�
                controller.Rb.velocity = new Vector2(_horizontalDir, _verticalDir).normalized * _speed;
            }

            //���b�o������
            if (_timer >= _waitTime)
            {
                Debug.Log("stop");
                controller.Rb.velocity = Vector2.zero;//��~
                controller.transform.position = _superAttackPosition;//�{�X�̈ʒu���C��
                break;//�I���
            }
        }

        _timer = RESET_TIME;//�^�C�����Z�b�g
        _secondPattern = Random.Range(0, _bullet.Length);
        _thirdPattern = Random.Range(0, _bullet.Length);

        if(_Introduction)
        {
            _Introduction.gameObject.SetActive(true);
        }
        

        //�K�E�Z����
        while (true)
        {
            //�U�����̃T�E���h
            SoundManager.Instance.UseSound(_superAttack);

            if (_timer >= _introductionStopTime)
            {
                _Introduction.gameObject.SetActive(false);
            }

            //���ԂɂȂ������]
            if(_timer >= _timeLimit)//5f
            {
                
                Vector3 localAngle = _muzzles[1].localEulerAngles;// ���[�J�����W����Ɏ擾
                Debug.Log(localAngle.z);

                //�����܂ŗ�����t��]
                if(localAngle.z >= _leftRotLimit)
                {
                    _rotDir = true;
                    Debug.Log("R");
                }
                else if(localAngle.z <= _rightRotLimit)
                {
                    _rotDir = false;
                    Debug.Log("L");
                }
                if (!_rotDir)
                {
                    localAngle.z += _angleInterval;// �p�x��ݒ�
                    _muzzles[1].localEulerAngles = localAngle;//��]����
                }
                else if(_rotDir)
                {
                    localAngle.z -= _angleInterval;// �p�x��ݒ�
                    _muzzles[1].localEulerAngles = localAngle;//��]����
                }
            }
            //�e�̌����ڂ�ς���
            _firstPattern = Random.Range(0, _bullet.Length);
            //�����}�Y��

            //�}�Y������]����

            //�^�[�Q�b�g�i�v���C���[�j�̕������v�Z
            _dir = (GameManager.Instance.Player.transform.position - _muzzles[0].transform.position);
            //�^�[�Q�b�g�i�v���C���[�j�̕����ɉ�]
            _muzzles[0].transform.rotation = Quaternion.FromToRotation(Vector3.up, _dir);

            //�e���}�Y��0�̌����ɍ��킹�Ēe�𔭎�
            ObjectPool.Instance.UseObject(_muzzles[0].position, _bullet[_firstPattern]).transform.rotation = _muzzles[0].rotation;

            //�����Ȃ��}�Y��

            //�e���}�Y��1�̌����ɍ��킹�Ēe�𔭎ˁi�����}�Y���̒e���E���j
            ObjectPool.Instance.UseObject(_muzzles[1].position, _bullet[_secondPattern]).transform.rotation = _muzzles[1].rotation;
            
            //�e���}�Y��2�̌����ɍ��킹�Ēe�𔭎ˁi�����}�Y����荶���j
            ObjectPool.Instance.UseObject(_muzzles[2].position, _bullet[_thirdPattern]).transform.rotation = _muzzles[2].rotation;

            yield return new WaitForSeconds(_attackInterval);//�U���p�x

            //���b�o������
            if (_timer >= _activationTime)
            {
                break;//�I��
            }
        }
        yield break;//�I��
    }

}
