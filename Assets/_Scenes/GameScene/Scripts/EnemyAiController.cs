using UnityEngine;
using System.Collections;

public class EnemyAiController : LevelChangeListener
{
    public Rigidbody2D EnemyMissilePrefab;

    private readonly System.Random MyRandom = new System.Random();

    private bool IsActive;
    private int NumMissilesLeft;
    private int FramesSinceLastMissile;
    private int FramesToNextMissile;

    void Start()
    {

    }

    public void SetActive(bool active)
    {
        IsActive = active;
        if (!active) {
            FramesSinceLastMissile = 0;
        }
    }

    void Update()
    {
        if (!IsActive) {
            return;
        }

        if (NumMissilesLeft == 0) {
            // Now that we have no missiles left, check to see if all enemy missiles have fallen to move to the next level.
            if (IsLevelOver()) {
                LevelController controller = (LevelController) Object.FindObjectOfType(typeof(LevelController));
                controller.NextLevel();
            }
            return;
        }

        FramesSinceLastMissile++;
        if (FramesSinceLastMissile >= FramesToNextMissile) {
            CreateNewEnemyMissile();
            FramesSinceLastMissile = 0;
            FramesToNextMissile = GetNewFramesToNextMissile();
        }
    }

    private bool IsLevelOver()
    {
        Object[] enemyObjects = Object.FindObjectsOfType(typeof(Enemy));

        return (NumMissilesLeft == 0 && enemyObjects.Length == 0);
    }

    private int GetNewFramesToNextMissile()
    {
        int level = Level.GetCurrentLevel();
        switch (Difficulty.GetDifficultyLevel())
        {
            case Difficulty.Level.Easy:
                return 150 - (MyRandom.Next(2) * (level - 1));
            case Difficulty.Level.Normal:
                return 100 - (MyRandom.Next(2) * (level - 1));
            case Difficulty.Level.Hard:
                return 75 - (MyRandom.Next(5) * (level - 1));
            default:
                throw new System.Exception("Unkown difficulty level");
        }
    }

    private void CreateNewEnemyMissile()
    {
        Vector2 start = GetPositionWithRandomX(50);
        Rigidbody2D missileClone = (Rigidbody2D)Instantiate(EnemyMissilePrefab, start, Quaternion.identity);
        Vector2 end = GetPositionWithRandomX(-50);
        missileClone.GetComponent<MissileController>().TargetPosition = end;

        NumMissilesLeft--;
    }

    private Vector2 GetPositionWithRandomX(int yVal)
    {
        int startX = MyRandom.Next(200) - 100;
        return new Vector2(startX, yVal);
    }

    public override void OnLevelEnded()
    {
        SetActive(false);
    }

    public override void NewLevelStarted(int level)
    {
        NumMissilesLeft = GetNumMissilesForLevel(level);
        FramesToNextMissile = GetNewFramesToNextMissile();
        SetActive(true);
    }

    public int GetNumMissilesForLevel(int level)
    {
        switch (Difficulty.GetDifficultyLevel()) {
            case Difficulty.Level.Easy:
                return 30 + (3 * (level - 1));
            case Difficulty.Level.Normal:
                return 35 + (4 * (level - 1));
            case Difficulty.Level.Hard:
                return 26 + (5 * (level - 1));
            default:
                throw new System.SystemException("Unkown difficulty level");
        }
    }
}
