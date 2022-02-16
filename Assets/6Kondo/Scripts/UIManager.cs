using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class UIManager : SingletonMonoBehaviour<UIManager>
{
    [SerializeField, Header("���b�����ĕω������邩")] float _scoreChangeInterval = default;
    [SerializeField] Text _scoreText;
    
    [SerializeField] int _score;
    int _maxScore;
    int _maxResidue;

    public void Start()
    {
        _maxScore = GameManager.Instance.PlayerScoreLimit;
        _maxResidue = GameManager.Instance.PlayerResidueLimit;
    }

    public void TestScore()
    {
        UIScoreChange(_score);
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
                score,
                _scoreChangeInterval)
                .OnUpdate(() => _scoreText.text = tempScore.ToString("00000000"))
                .OnComplete(() => _scoreText.text = GameManager.Instance.PlayerScore.ToString("00000000"));
        }
    }
    public void UIResidueChange(int Residue)
    {
        
    }
}
