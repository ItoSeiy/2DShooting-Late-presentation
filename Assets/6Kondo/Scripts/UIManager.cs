using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class UIManager : SingletonMonoBehaviour<UIManager>
{
    [SerializeField, Header("�X�R�A�����b�����ĕω������邩")]
    float _scoreChangeInterval = default;

    [SerializeField, Header("�X�R�AUI�̃e�L�X�g")] 
    Text _scoreText;

    [SerializeField, Header("�c�@�������b�����ĕω������邩")] 
    float _residuesChangeInterval = default;

    [SerializeField, Header("�c�@���̃e�L�X�g")] 
    Text _residueText;

    [SerializeField, Header("�{���������b�����ĕω������邩")] 
    float _bombChangeInterval = default;

    [SerializeField, Header("�{�����̃e�L�X�g")]
    Text _bombText;

    [SerializeField, Header("���G�I�u�W�F�N�g�����������b�����ĕω������邩")] 
    float _invicibleObjectCountChangeInterval = default;

    [SerializeField, Header("���G�I�u�W�F�N�g�������̃e�L�X�g")] 
    Text _invicibleObjectCountText;

    [SerializeField, Header("���G�I�u�W�F�N�g�K�v���̃e�L�X�g")] 
    Text _invicibleObjectLimitText;

    [SerializeField, Header("�p���[�A�C�e�������������b�����ĕω������邩")] 
    float _powerItemCountChamgeInterval = default;

    [SerializeField, Header("�p���[�A�C�e���������̃e�L�X�g")] 
    Text _powerItemCountText;

    [SerializeField, Header("�p���[�A�C�e���K�v�������b�����ĕω������邩")] 
    float _powerItemLimitChangeInterval = default;

    [SerializeField, Header("�p���[�A�C�e���K�v���̃e�L�X�g")] 
    Text _powerItemLimitText;

    [SerializeField, Header("���݂̃��x���̃e�L�X�g")] 
    Text _levelText;

    int _initialScore;
    int _initialResidue;
    int _initialBomb;
    int _initialInvicibleObject;
    int _initialPowerItem;

    int _maxScore;
    int _maxResidue;
    int _maxBomb;
    int _maxInvicibleObject;
    int _maxPowerItem;

    int _invicibleObjectLimit;
    int _invicibleObjectCount;

    int _powerItemLimit2;
    int _powerItemLimit3;
    int _powerItemCount;

    int _level;
    const int DEFAULT = 0;
    const int LEVEL1 = 1;
    const int LEVEL2 = 2;
    const int LEVEL3 = 3;

    public void UISet()
    {
        _initialScore = GameManager.Instance.PlayerScore;
        _scoreText.text = _initialScore.ToString("00000000");

        _initialResidue = GameManager.Instance.PlayerResidueCount;
        _residueText.text = _initialResidue.ToString("00");

        _initialBomb = GameManager.Instance.PlayerBombCount;
        _bombText.text = _initialBomb.ToString("00");

        _initialInvicibleObject = GameManager.Instance.PlayerInvincibleObjectCount;
        _invicibleObjectCountText.text = _initialInvicibleObject.ToString("000");

        _maxInvicibleObject = GameManager.Instance.Player.InvicibleLimit;

        _initialPowerItem = GameManager.Instance.PlayerPowerItemCount;
        _powerItemCountText.text = _initialPowerItem.ToString("000");

        _maxScore = GameManager.Instance.Player.PlayerScoreLimit;

        _maxResidue = GameManager.Instance.Player.PlayerResidueLimit;

        _maxBomb = GameManager.Instance.Player.PlayerBombLimit;

        _maxPowerItem = GameManager.Instance.Player.PlayerPowerLimit;
        
        _powerItemLimit2 = GameManager.Instance.Player.PlayerPowerRequiredNumberLevel2;
        _powerItemLimit3 = GameManager.Instance.Player.PlayerPowerRequiredNumberLevel3;
        _powerItemLimitText.text = _powerItemLimit2.ToString("000");

        _invicibleObjectLimit = GameManager.Instance.Player.InvicibleLimit;
        _invicibleObjectLimitText.text = _invicibleObjectLimit.ToString("000");

        _level = GameManager.Instance.PlayerLevel;
        _levelText.text = _level.ToString("1");
    }

    /// <summary>
    /// ���_�̕\�����X�V����
    /// </summary>
    /// <param name="score">�ǉ�����_��</param>
    public void UIScoreChange(int score)
    {
        int tempScore = int.Parse(_scoreText.text.ToString());

        tempScore = Mathf.Min(tempScore + score, _maxScore);

        if (tempScore <= _maxScore)
        {
            DOTween.To(() => tempScore,
                x => tempScore = x,
                GameManager.Instance.PlayerScore,
                _scoreChangeInterval)
                .OnUpdate(() => _scoreText.text = tempScore.ToString("00000000"))
                .OnComplete(() => _scoreText.text = GameManager.Instance.PlayerScore.ToString("00000000"));
        }
    }
    public void UIResidueChange(int residue)
    {
        int tempResidues = int.Parse(_residueText.text.ToString());

        tempResidues = Mathf.Min(tempResidues + residue, _maxResidue);

        if (tempResidues <= _maxResidue)
        {
            DOTween.To(() => tempResidues,
                x => tempResidues = x,
                GameManager.Instance.PlayerResidueCount,
                _residuesChangeInterval)
                .OnUpdate(() => _residueText.text = tempResidues.ToString("00"))
                .OnComplete(() => _residueText.text = GameManager.Instance.PlayerResidueCount.ToString("00"));
        }
    }
    public void UIBombChange(int bomb)
    {
        int tempBomb = int.Parse(_bombText.text.ToString());

        tempBomb = Mathf.Min(tempBomb + bomb, _maxBomb);

        if (tempBomb <= _maxBomb)
        {
            DOTween.To(() => tempBomb,
                x => tempBomb = x,
                GameManager.Instance.PlayerBombCount,
                _bombChangeInterval)
                .OnUpdate(() => _bombText.text = tempBomb.ToString("00"))
                .OnComplete(() => _bombText.text = GameManager.Instance.PlayerBombCount.ToString("00"));
        }
    }
    public void UIInvisibleCountChange(int invisible)
    {
        int tempInvisible = int.Parse(_invicibleObjectCountText.text.ToString());

        tempInvisible = Mathf.Min(tempInvisible + invisible, _maxInvicibleObject);

        if (tempInvisible <= _maxInvicibleObject)
        {
            DOTween.To(() => tempInvisible,
                x => tempInvisible = x,
                GameManager.Instance.PlayerInvincibleObjectCount,
                _invicibleObjectCountChangeInterval)
                .OnUpdate(() => _invicibleObjectCountText.text = tempInvisible.ToString("000"))
                .OnComplete(() => _invicibleObjectCountText.text = GameManager.Instance.PlayerInvincibleObjectCount.ToString("000"));
        }
    }
    public void UIPowerLimitChange(int power)
    {
        switch(GameManager.Instance.PlayerLevel)
        {
            case LEVEL1:
                int tempPower = int.Parse(_powerItemLimitText.text.ToString());

                tempPower = Mathf.Min(tempPower + power, _powerItemLimit2);
                DOTween.To(() => tempPower,
                    x => tempPower = x,
                    _powerItemLimit2,
                    _powerItemLimitChangeInterval)
                    .OnUpdate(() => _powerItemLimitText.text = tempPower.ToString("000"))
                    .OnComplete(() => _powerItemLimitText.text = _powerItemLimit2.ToString("000"));
                UILevelChange();
                break;
            case LEVEL2:
                int tempPower2 = int.Parse(_powerItemLimitText.text.ToString());

                tempPower2 = Mathf.Min(tempPower2 + power, _powerItemLimit3);
                DOTween.To(() => tempPower2,
                    x => tempPower2 = x,
                    _powerItemLimit3,
                    _powerItemLimitChangeInterval)
                    .OnUpdate(() => _powerItemLimitText.text = tempPower2.ToString("000"))
                    .OnComplete(() => _powerItemLimitText.text = _powerItemLimit3.ToString("000"));
                UILevelChange();
                break;
            case LEVEL3:
                int tempPower3 = int.Parse(_powerItemLimitText.text.ToString());

                tempPower3 = Mathf.Min(tempPower3 + power, _maxPowerItem);
                DOTween.To(() => tempPower3,
                    x => tempPower3 = x,
                    _maxPowerItem,
                    _powerItemLimitChangeInterval)
                    .OnUpdate(() => _powerItemLimitText.text = tempPower3.ToString("000"))
                    .OnComplete(() => _powerItemLimitText.text = _maxPowerItem.ToString("000"));
                UILevelChange();
                break;
        }
    }
    public void UIPowerCountChange(int power)
    {
        int tempPower = int.Parse(_powerItemCountText.text.ToString());

        tempPower = Mathf.Min(tempPower + power, _maxPowerItem);
        
        if(tempPower <= DEFAULT)
        {
            //�n���ꂽ�l�����̐��������ꍇ�͌��Z���������Ƃ������ƂȂ̂ŕ\������l�𐳂̐��ɂ���
            tempPower *= -1;
        }
        if (tempPower <= _maxPowerItem)
        {
            DOTween.To(() => tempPower,
                x => tempPower = x,
                GameManager.Instance.PlayerPowerItemCount,
                _powerItemCountChamgeInterval)
                .OnUpdate(() => _powerItemCountText.text = tempPower.ToString("000"))
                .OnComplete(() => _powerItemCountText.text = GameManager.Instance.PlayerPowerItemCount.ToString("000"));
        }
    }
    void UILevelChange()
    {
        _levelText.text = GameManager.Instance.PlayerLevel.ToString("0");
    }
}
