using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CoreGameOverMenu : MonoBehaviour
{
    public void TryAgainClick()
    {
        Level.Reset();
        SceneManager.LoadScene("GameScene");
    }

    public void MainMenuClick()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void ExitClick()
    {
        Application.Quit();
    }
}
