using UnityEngine;
using System;
using System.Collections;

public class EnemyAiController : MonoBehaviour
{
    private const int FRAMES_TO_NEXT_MISSILE = 100;

    public Rigidbody2D EnemyMissilePrefab;

    private bool IsActive;
    private int FramesSinceLastMissile;
    private readonly System.Random MyRandom = new System.Random();

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

        FramesSinceLastMissile++;
        if (FramesSinceLastMissile >= FRAMES_TO_NEXT_MISSILE)
        {
            CreateNewEnemyMissile();
            FramesSinceLastMissile = 0;
        }
    }

    private void CreateNewEnemyMissile()
    {
        Vector2 start = GetPositionWithRandomX(50);
        Rigidbody2D missileClone = (Rigidbody2D)Instantiate(EnemyMissilePrefab, start, Quaternion.identity);
        Vector2 end = GetPositionWithRandomX(-50);
        missileClone.GetComponent<MissileController>().TargetPosition = end;
    }

    private Vector2 GetPositionWithRandomX(int yVal)
    {
        int startX = MyRandom.Next(200) - 100;
        return new Vector2(startX, yVal);
    }
}
