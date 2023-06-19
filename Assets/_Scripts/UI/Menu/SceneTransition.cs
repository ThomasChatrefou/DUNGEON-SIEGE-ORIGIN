using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    private void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadPlayScene()
    {
        GoToScene("Arena1");
    }

    public void LoadMainMenuScene()
    {
        GoToScene("HomeMenu");
    }

    public void LoadTutorialScene()
    {
        GoToScene("Tutorial 1");
    }
}
