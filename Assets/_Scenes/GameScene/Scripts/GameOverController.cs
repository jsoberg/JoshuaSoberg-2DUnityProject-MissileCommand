using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverController : MonoBehaviour
{
    public Text GameOverText;

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
    }
}
