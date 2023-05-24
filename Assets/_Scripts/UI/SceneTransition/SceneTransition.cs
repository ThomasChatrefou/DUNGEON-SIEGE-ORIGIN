using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadPlayScene()
    {
        LoadScene("MiniProto");
    }

    public void LoadMainMenuScene()
    {
        LoadScene("MainMenu");
    }
}
