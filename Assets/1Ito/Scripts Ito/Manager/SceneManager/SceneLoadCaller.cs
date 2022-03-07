using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadCaller : MonoBehaviour
{
    [SerializeField] string _sceneName = null;

    public void LoadSceneString()
    {
        SceneLoder.Instance.LoadScene(_sceneName);
    }

    /// <summary>
    /// ���݊J���Ă���V�[�������[�h����
    /// </summary>
    public void LoadSameScene()
    {
        SceneLoder.Instance.LoadScene(SceneManager.GetActiveScene().name);
    }
}
