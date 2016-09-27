using UnityEngine;
using System.Collections;

public class EnemyAiController : LevelChangeListener
{
    public Rigidbody2D EnemyMissilePrefab;
    public Rigidbody2D EnemyMIRVPrefab;

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
                return 100 - (MyRandom.Next(3) * (level - 1));
            case Difficulty.Level.Hard:
                return 75 - (MyRandom.Next(5) * (level - 1));
            default:
                throw new System.Exception("Unkown difficulty level");
        }
    }

    public void RapidDeployMissilesFromPosition(int numMissiles, Vector2 startPosition)
    {
        for (int i = 0; i < numMissiles; i ++) {
            Rigidbody2D missileClone = (Rigidbody2D)Instantiate(EnemyMissilePrefab, startPosition, Quaternion.identity);
            Vector2 end = ChooseTargetPositionForMissile();
            missileClone.GetComponent<MissileController>().TargetPosition = end;
        }
    }

    private void CreateNewEnemyMissile()
    {
        Rigidbody2D missileClone = InstantiateNewMissileForDifficulty();
        Vector2 end = ChooseTargetPositionForMissile();
        missileClone.GetComponent<MissileController>().TargetPosition = end;

        NumMissilesLeft--;
    }

    private Rigidbody2D InstantiateNewMissileForDifficulty()
    {
        switch (Difficulty.GetDifficultyLevel()) {
            case Difficulty.Level.Easy:
                return ChooseNewMissileWithMirvProbability(.02d);
            case Difficulty.Level.Normal:
                return ChooseNewMissileWithMirvProbability(.05d);
            case Difficulty.Level.Hard:
                return ChooseNewMissileWithMirvProbability(.1d);
            default:
                throw new System.SystemException("Unkown difficulty level");
        }
    }

    private Rigidbody2D ChooseNewMissileWithMirvProbability(double probabilityMirv)
    {
        double chance = MyRandom.NextDouble();
        if (chance < probabilityMirv) {
            return (Rigidbody2D) Instantiate(EnemyMIRVPrefab, GetPositionWithRandomX(50), Quaternion.identity);
        } else {
            return (Rigidbody2D) Instantiate(EnemyMissilePrefab, GetPositionWithRandomX(50), Quaternion.identity);
        }
    }

    private Vector2 GetPositionWithRandomX(int yVal)
    {
        int startX = MyRandom.Next(200) - 100;
        return new Vector2(startX, yVal);
    }

    private Vector2 ChooseTargetPositionForMissile()
    {
        switch (Difficulty.GetDifficultyLevel()) {
            case Difficulty.Level.Easy:
                return DecidePathForMissile(50, 30, 20, .5d);
            case Difficulty.Level.Normal:
                return DecidePathForMissile(15, 35, 50, .2d);
            case Difficulty.Level.Hard:
                return DecidePathForMissile(1, 35, 64, .06d);
            default:
                throw new System.SystemException("Unkown difficulty level");
        }
    }

    // Send in proportion of decision type (* 100) and the percentage that we choose a destroyed city/silo instead of active, and we will decide the path for the missile in a roulette-wheel fashion.
    // e.g. If we want 10% random, 20% toward silo, and 70% toward city, with a 20% chance we will choose a destroyed city/silo: (10, 20, 70, .2)
    private Vector2 DecidePathForMissile(int random, int towardSilo, int towardCity, double chanceDestroyed)
    {
        int wheel = MyRandom.Next(random + towardSilo + towardCity) + 1;
        if (wheel < random) {
            return GetPositionWithRandomX(-50);
        } else if (wheel < towardSilo) {
            return ChooseSiloDestination(chanceDestroyed);
        } else {
            return ChooseCityDestination(chanceDestroyed);
        }
    }

    private Vector2 ChooseSiloDestination(double chanceDestroyed)
    {
        SiloController[] silos = (SiloController[])Object.FindObjectsOfType(typeof(SiloController));

        SiloController chosenSilo = silos[MyRandom.Next(silos.Length)];
        for (int tries = 0; tries < silos.Length; tries++) {
            double chance = MyRandom.NextDouble();
            if (chance < chanceDestroyed && chosenSilo.IsDestroyed) {
                return chosenSilo.gameObject.transform.position;
            } else if (!chosenSilo.IsDestroyed) {
                return chosenSilo.gameObject.transform.position;
            }

            // Try again to see if we can fit the specifications.
            chosenSilo = silos[MyRandom.Next(silos.Length)];
        }
        return chosenSilo.gameObject.transform.position;
    }

    private Vector2 ChooseCityDestination(double chanceDestroyed)
    {
        CityController[] cities = (CityController[]) Object.FindObjectsOfType(typeof(CityController));

        CityController chosenCity = cities[MyRandom.Next(cities.Length)];
        for (int tries = 0; tries < cities.Length; tries ++) {
            double chance = MyRandom.NextDouble();
            if (chance < chanceDestroyed && chosenCity.IsDestroyed) {
                return chosenCity.gameObject.transform.position;
            } else if (!chosenCity.IsDestroyed) {
                return chosenCity.gameObject.transform.position;
            }

            // Try again to see if we can fit the specifications.
            chosenCity = cities[MyRandom.Next(cities.Length)];
        }
        return chosenCity.gameObject.transform.position;
    }

    public override void OnLevelEnded()
    {
        SetActive(false);
    }

    public override void OnNewLevelStarted(int level)
    {
        NumMissilesLeft = GetNumMissilesForLevel(level);
        FramesToNextMissile = GetNewFramesToNextMissile();
        SetActive(true);
    }

    public int GetNumMissilesForLevel(int level)
    {
        switch (Difficulty.GetDifficultyLevel()) {
            case Difficulty.Level.Easy:
                return 1 + (3 * (level - 1));
            case Difficulty.Level.Normal:
                return 25 + (4 * (level - 1));
            case Difficulty.Level.Hard:
                return 30 + (5 * (level - 1));
            default:
                throw new System.SystemException("Unkown difficulty level");
        }
    }

    public override void OnGameOver()
    {
        SetActive(false);
    }
}
