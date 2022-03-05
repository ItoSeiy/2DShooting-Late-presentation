using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PauseManager : SingletonMonoBehaviour<PauseManager>
{
    [SerializeField] RectTransform _pauseUi;

    public bool PauseFlg { get; set; } = false;

    Action<bool> _onPauseResume = default;

    public void OnPauseResume(InputAction.CallbackContext context)
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
        PauseFlg = !PauseFlg;
        _onPauseResume(PauseFlg);  // ����ŕϐ��ɑ�������֐���S�ČĂяo����
        _pauseUi.gameObject.SetActive(PauseFlg);
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
