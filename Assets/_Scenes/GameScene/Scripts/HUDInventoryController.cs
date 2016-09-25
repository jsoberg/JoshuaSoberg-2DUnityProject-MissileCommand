using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class HUDInventoryController : LevelChangeListener
{
    // HUD UI Objects
    public GameObject MissilesLeftIcon;
    public Text MissilesLeftText;
    public Text PointsText;
    public AudioSource AccessDeniedSound;

    // Misc Inventory items
    private int NumMissilesLeft;

    // Current Score
    private int CurrentScore;

    void Start()
    {

    }

    void Update()
    {

    }

    void OnGUI()
    {
        Vector2 lowerLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 currentMissileTextPosition = MissilesLeftIcon.transform.position;
        float halfIconWidthPlusOffset = (MissilesLeftIcon.GetComponent<Renderer>().bounds.size.x / 2) + 4;
        MissilesLeftIcon.transform.position = new Vector3(lowerLeft.x + halfIconWidthPlusOffset, currentMissileTextPosition.y, currentMissileTextPosition.z);

        Vector2 lowerRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));
        Vector3 currentPointsTextPosition = PointsText.transform.position;
        float quarterTextWidth = (PointsText.GetComponent<RectTransform>().rect.width / 4);
        PointsText.transform.position = new Vector3(lowerRight.x - quarterTextWidth, currentPointsTextPosition.y, currentPointsTextPosition.z);
    }

    public override void OnGameOver()
    {
        
    }

    public override void OnLevelEnded()
    {
        NumMissilesLeft = 0;
        MissilesLeftIcon.SetActive(false);
    }

    public override void OnNewLevelStarted(int level)
    {
        NumMissilesLeft = GetNumMissilesForLevel(level);
        UpdateMissileInventoryHud();
        MissilesLeftIcon.SetActive(true);
        UpdateScoreHud();
    }

    // ========================================
    // Missile Inventory
    // ========================================

    public bool CanFireMissile()
    {
        return NumMissilesLeft > 0;
    }

    public void MissileFired()
    {
        NumMissilesLeft--;
        UpdateMissileInventoryHud();
    }

    public void MissileFireAttempted()
    {
        UpdateMissileInventoryHud();
    }

    public void UpdateMissileInventoryHud()
    {
        MissilesLeftText.text = "" + NumMissilesLeft;
        if (NumMissilesLeft == 0)
        {
            MissilesLeftIcon.GetComponent<SpriteRenderer>().color = Color.red;
            MissilesLeftText.color = Color.red;
            AccessDeniedSound.Play();
        }
        else
        {
            MissilesLeftIcon.GetComponent<SpriteRenderer>().color = Color.white;
            MissilesLeftText.color = Color.white;
        }
    }

    private int GetNumMissilesForLevel(int level)
    {
        switch (Difficulty.GetDifficultyLevel())
        {
            case Difficulty.Level.Easy:
                return 50 + (2 * (level - 1));
            case Difficulty.Level.Normal:
                return 40 + (2 * (level - 1));
            case Difficulty.Level.Hard:
                return 30 + (2 * (level - 1));
            default:
                throw new SystemException("Unkown difficulty level");
        }
    }

    // ========================================
    // Score
    // ========================================

    public void AddScore(int points)
    {
        CurrentScore += points;
        UpdateScoreHud();
    }

    public void UpdateScoreHud()
    {
        PointsText.text = "" + CurrentScore;
    }
}