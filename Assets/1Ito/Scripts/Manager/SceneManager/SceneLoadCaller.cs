using UnityEngine;

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
        SceneLoder.Instance.LoadScene(SceneLoder.Instance.ActiveSceneName);
    }
}
