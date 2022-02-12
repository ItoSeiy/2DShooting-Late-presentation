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
    [SerializeField] int _testScore;

    void Awake()
    {
    }

    void Update()
    {

    }

    /// <summary>
    /// ���_�����Z���A�\�����X�V����
    /// </summary>
    /// <param name="score">�ǉ�����_��</param>
    public void UIScoreChange(int score, int maxScore)
    {
        int tempScore = int.Parse(_scoreText.ToString());

        score = Mathf.Min(tempScore + score, maxScore);

        if (score <= maxScore)
        {
            DOTween.To(() => score,
                x => score = x,
                score,
                _scoreChangeInterval)
                .OnComplete(() => _scoreText.text = _testScore.ToString());
        }

    }
}
