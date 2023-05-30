using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class DebugCommandMenuEditor : EditorWindow
{
    [MenuItem("Window/Debug Command Menu")]
    public static void Init()
    {
        GetWindow(typeof(DebugCommandMenuEditor));
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Debug Command Menu", EditorStyles.boldLabel);
        GUILayout.EndHorizontal();

        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Kill all enemies"))
        {
            KillAllEnemies();
        }
        GUILayout.EndHorizontal();
    }

    private void KillAllEnemies()
    {
        int nbEnemiesKilled = 0;
        foreach (GameObject go in EditorSceneManager.GetActiveScene().GetRootGameObjects())
        {
            foreach (AIBaseController aiController in go.GetComponentsInChildren<AIBaseController>())
            {
                Destroy(aiController.gameObject);
                ++nbEnemiesKilled;
            }
        }
        Debug.Log("Command Kill all enemies : " + nbEnemiesKilled + " enemies killed");
    }
}
