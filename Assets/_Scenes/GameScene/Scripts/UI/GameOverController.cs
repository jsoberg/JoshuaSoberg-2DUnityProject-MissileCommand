using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameOverController : MonoBehaviour
{
    public Text GameOverText;

    public GameObject NewHighScoreOptions;
    public Button ViewHighScoresButton;
    public GameObject CoreMenu;

    private bool IsGameOver;

	void Start ()
    {
	
	}

	void Update ()
    {
	
	}

    public void ImportantObjectDestroyed()
    {
        if (AllCitiesDestroyed() || AllSilosDestroyed()) {
            GameOver();
        }
    }

    private bool AllCitiesDestroyed()
    {
        Object[] cities = Object.FindObjectsOfType(typeof(CityController));
        foreach (CityController city in cities) {
            if (!city.IsDestroyed) {
                return false;
            }
        }

        return true;
    }

    private bool AllSilosDestroyed()
    {
        Object[] silos = Object.FindObjectsOfType(typeof(SiloController));
        foreach (SiloController silo in silos) {
            if (!silo.IsDestroyed) {
                return false;
            }
        }

        return true;
    }

    private void GameOver()
    {
        // Game is already over.
        if (IsGameOver) {
            return;
        }

        IsGameOver = true;
        ((LevelController) Object.FindObjectOfType(typeof(LevelController))).InformGameOver();
        GameOverText.gameObject.SetActive(true);
        StartCoroutine(FadeGameOverTextIn(3f));
    }

    public IEnumerator FadeGameOverTextIn(float t)
    {
        GameOverText.color = new Color(GameOverText.color.r, GameOverText.color.g, GameOverText.color.b, 0);
        while (GameOverText.color.a < 1.0f)
        {
            GameOverText.color = new Color(GameOverText.color.r, GameOverText.color.g, GameOverText.color.b, GameOverText.color.a + (Time.deltaTime / t));
            yield return null;
        }

        ShowMenu();
    }

    private void ShowMenu()
    {
        if (IsNewHighScore()) {
            NewHighScoreOptions.SetActive(true);
        } else {
            ViewHighScoresButton.gameObject.SetActive(true);
        }

        CoreMenu.SetActive(true);
    }

    private bool IsNewHighScore()
    {
        HUDInventoryAndScoreController scoreController = (HUDInventoryAndScoreController) Object.FindObjectOfType(typeof(HUDInventoryAndScoreController));
        int score = scoreController.GetCurrentScore();
        SortedList<int, HighScore> highScores = HighScoreUtils.GetHighScores();
        if (highScores.Count < 10) {
            return true;
        }

        foreach (HighScore highScore in highScores.Values) {
            if (score >= highScore.Score) {
                return true;
            }
        }

        return false;
    }
}
