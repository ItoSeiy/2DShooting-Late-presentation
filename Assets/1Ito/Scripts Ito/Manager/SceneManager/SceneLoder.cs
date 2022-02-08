using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// �V���O���g���p�^�[���̃V�[����J�ڂ�����X�N���v�g
/// </summary>
public class SceneLoder : SingletonMonoBehaviour<SceneLoder>
{
    [SerializeField] Image _fadeImage = null;
    [SerializeField] float _fadeTime = 1;
    float _timer = 0;
    bool _isLoad = false;
    /// <summary>
    /// �V�[�������[�h����֐�
    /// </summary>
    /// <param name="sceneName">�J�ڂ������V�[���̖��O</param>
    public void LoadScene (string sceneName)
    {
        if (_isLoad) return;
        StartCoroutine(Fade(sceneName));
    }

    IEnumerator Fade(string sceneName)
    {
        _isLoad = true;
        Color c;
        while (_fadeImage.color.a < 1)
        {
            c = _fadeImage.color;
            _timer += Time.deltaTime;
            c.a = _timer / _fadeTime;
            _fadeImage.color = c;
            yield return new WaitForEndOfFrame();
        }

        yield return SceneManager.LoadSceneAsync(sceneName);

        while (_fadeImage.color.a >  0)
        {
            c = _fadeImage.color;
            _timer -= Time.deltaTime;
            c.a = _timer / _fadeTime;
            _fadeImage.color = c;
            yield return new WaitForEndOfFrame();
        }
        GameManager.Instance.GameStart();
        _isLoad = false; 
    }
}
