using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ViewHighScores : MonoBehaviour
{
    public void ViewHighScoresClick()
    {
        SceneManager.LoadScene("HighScoreScene");
    }
}
