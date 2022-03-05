using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PauseManager : SingletonMonoBehaviour<PauseManager>
{
    [SerializeField] InputAction _pauseButton;
    [SerializeField] Canvas _canvas;

    bool _pauseFlg = false;

    Action<bool> _onPauseResume = default;

    public void OnClick(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            PauseResume();
        }
    }

    /// <summary>
    /// �ꎞ��~�E�ĊJ��؂�ւ���
    /// </summary>
    void PauseResume()
    {
        _pauseFlg = !_pauseFlg;
        _onPauseResume(_pauseFlg);  // ����ŕϐ��ɑ�������֐���S�ČĂяo����
    }

    /// <summary>
    /// �ꎞ��~�A�ĊJ���̊֐����f���Q�[�g�ɓo�^����֐�
    /// 
    /// �ꎞ��~�������������X�N���v�g����Ăяo��
    /// 
    /// OnEnable�֐��� PauseManager.Instance.SetEvent(this); �ƋL�q�����
    /// </summary>
    /// <param name="pauseable"></param>
    public void SetEvent(IPauseable pauseable)
    {
        _onPauseResume += pauseable.PauseResume;
    }

    /// <summary>
    /// �ꎞ��~�A�ĊJ���̊֐����f���Q�[�g����o�^����������֐�
    /// 
    /// �ꎞ��~�������������X�N���v�g����Ăяo��
    /// 
    /// OnDisable�֐���PauseManager.Instance.RemoveEvent(this); �ƋL�q�����
    /// </summary>
    /// <param name="pauseable"></param>
    public void RemoveEvent(IPauseable pauseable)
    {
        _onPauseResume -= pauseable.PauseResume;
    }
}
