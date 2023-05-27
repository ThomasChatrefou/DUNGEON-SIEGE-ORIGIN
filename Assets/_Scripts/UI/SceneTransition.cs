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
        GoToScene("MiniProto");
    }

    public void LoadMainMenuScene()
    {
        GoToScene("HomeMenu");
    }
}
