using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelController : MonoBehaviour
{
    private const string LEVEL_TEXT = "LEVEL {0}";

    public Text LevelText;

    public GameObject PointInfoGroup;
    public Text LevelCompleteText;
    public Text CitiesLeft;
    public Text PointsPerCity;
    public Text MissilesLeft;
    public Text PointsPerMissile;

    public EnemyAiController EnemyAiController;
    public AudioSource AirRaidSiren;

	void Start ()
    {
        EnemyAiController = EnemyAiController.GetComponent<EnemyAiController>();
        NextLevel();
	}

    public void NextLevel()
    {
        Level.NextLevel();

        // Get missiles currently left before informing of level ending.
        int missilesLeft = GetMissilesLeft();
        InformLevelEnded();
        StartCoroutine(ShowPointsTextInAndOut(4f, missilesLeft));
    }

    private int GetMissilesLeft()
    {
        HUDInventoryAndScoreController controller = (HUDInventoryAndScoreController)Object.FindObjectOfType(typeof(HUDInventoryAndScoreController));
        return controller.GetNumMissilesLeft();
    }

    public void InformLevelEnded()
    {
        Object[] listeners = Object.FindObjectsOfType(typeof(LevelChangeListener));

        foreach (LevelChangeListener l in listeners) {
            l.OnLevelEnded();
        }
    }

    void Update ()
    {
	
	}

    public IEnumerator ShowPointsTextInAndOut(float t, int missilesLeft)
    {
        // Don't show for first level.
        if (Level.GetCurrentLevel() != 1) {

            int lastLevel = Level.GetCurrentLevel() - 1;
            LevelCompleteText.text = "LEVEL " + lastLevel + " COMPLETE";

            HUDInventoryAndScoreController controller =
                (HUDInventoryAndScoreController)Object.FindObjectOfType(typeof(HUDInventoryAndScoreController));

            int citiesNotDestroyed = CitiesNotDestroyed();
            CitiesLeft.text = "" + citiesNotDestroyed;
            int pointsPerCity = GetPointsPerCity();
            PointsPerCity.text = pointsPerCity + " POINTS";
            // Add cities score
            controller.AddScore(citiesNotDestroyed * pointsPerCity);

            MissilesLeft.text = "" + missilesLeft;
            int pointsPerMissile = GetPointsPerMissile();
            PointsPerMissile.text = pointsPerMissile + " POINTS";
            // Add missiles score
            controller.AddScore(missilesLeft * pointsPerMissile);

            PointInfoGroup.SetActive(true);
            yield return new WaitForSeconds(t);
            PointInfoGroup.SetActive(false);
        }
        yield return null;

        int newLevel = Level.GetCurrentLevel();
        LevelText.gameObject.SetActive(true);
        LevelText.text = string.Format(LEVEL_TEXT, newLevel);

        AirRaidSiren.Play();
        StartCoroutine(FadeLevelTextInAndOut(2f));
    }

    private int GetPointsPerCity()
    {
        switch (Difficulty.GetDifficultyLevel()) {
            case Difficulty.Level.Easy:
                return 400;
            case Difficulty.Level.Normal:
                return 600;
            case Difficulty.Level.Hard:
                return 800;
            default:
                throw new System.SystemException("Unkown difficulty level");
        }
    }

    private int GetPointsPerMissile()
    {
        switch (Difficulty.GetDifficultyLevel())
        {
            case Difficulty.Level.Easy:
                return 20;
            case Difficulty.Level.Normal:
                return 40;
            case Difficulty.Level.Hard:
                return 100;
            default:
                throw new System.SystemException("Unkown difficulty level");
        }
    }

    private int CitiesNotDestroyed()
    {
        int i = 0;
        Object[] cities = Object.FindObjectsOfType(typeof(CityController));
        foreach (CityController city in cities) {
            if (!city.IsDestroyed) {
                i ++;
            }
        }

        return i;
    }

    public IEnumerator FadeLevelTextInAndOut(float t)
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

        LevelText.gameObject.SetActive(false);
        InformNextLevel();
    }

    public void InformNextLevel()
    {
        Object[] listeners = Object.FindObjectsOfType(typeof(LevelChangeListener));

        foreach (LevelChangeListener l in listeners) {
            l.OnNewLevelStarted(Level.GetCurrentLevel());
        }
    }

    public void InformGameOver()
    {
        Object[] listeners = Object.FindObjectsOfType(typeof(LevelChangeListener));

        foreach (LevelChangeListener l in listeners) {
            l.OnGameOver();
        }
    }
}

public abstract class LevelChangeListener : MonoBehaviour
{
    public abstract void OnLevelEnded();

    public abstract void OnNewLevelStarted(int level);

    public abstract void OnGameOver();
}