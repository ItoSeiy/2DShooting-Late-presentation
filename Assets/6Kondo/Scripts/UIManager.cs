using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : SingletonMonoBehaviour<UIManager>
{
    [SerializeField, Header("���b�����ĕω������邩")] float _scoreChangeInterval = default;
    [SerializeField] Text _scoreText;
    [SerializeField] float _testScore;
    [SerializeField] int _maxScore = 999999999;
    int _score;

    void Update()
    {

    }

    /// <summary>
    /// ���_�����Z���A�\�����X�V����
    /// </summary>
    /// <param name="score">�ǉ�����_��</param>
    public void UIScoreChange(int score)
    {
        _score = Mathf.Min(_score + score, _maxScore);

        if (_score <= _maxScore)
        {
            DOTween.To(() => _score,
                x => _score = x,
                _score,
                _scoreChangeInterval);
        }

    }
}
