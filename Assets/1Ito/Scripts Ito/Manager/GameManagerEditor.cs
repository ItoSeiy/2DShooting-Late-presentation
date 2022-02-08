using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnHeaderGUI();
        var gameManager = target as GameManager;
        var t = "�Q�[���X�^�[�g";
        EditorGUI.BeginDisabledGroup(gameManager.IsGameStart);
        if(GUILayout.Button(t))
        {
            gameManager.GameStart();
        }
        EditorGUI.EndDisabledGroup();
    }
}
