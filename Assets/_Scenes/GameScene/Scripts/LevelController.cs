using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelController : MonoBehaviour
{
    private const string LEVEL_TEXT = "LEVEL {0}";

    public Text LevelText;
    public EnemyAiController EnemyAiController;

	void Start ()
    {
        EnemyAiController = EnemyAiController.GetComponent<EnemyAiController>();
        NextLevel();
	}

    private void NextLevel()
    {
        Level.NextLevel();
        int newLevel = Level.GetCurrentLevel();
        LevelText.text = string.Format(LEVEL_TEXT, newLevel);

        StartCoroutine(FadeTextLevelTextInAndOut(1f));
    }

    // Update is called once per frame
    void Update ()
    {
	
	}

    public IEnumerator FadeTextLevelTextInAndOut(float t)
    {
        LevelText.color = new Color(LevelText.color.r, LevelText.color.g, LevelText.color.b, 0);
        while (LevelText.color.a < 1.0f) {
            LevelText.color = new Color(LevelText.color.r, LevelText.color.g, LevelText.color.b, LevelText.color.a + (Time.deltaTime / t));
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        while (LevelText.color.a > 0f) {
            LevelText.color = new Color(LevelText.color.r, LevelText.color.g, LevelText.color.b, LevelText.color.a - (Time.deltaTime / t));
            yield return null;
        }

        EnemyAiController.SetActive(true);
    }
}
