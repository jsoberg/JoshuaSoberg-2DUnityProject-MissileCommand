using UnityEngine;
using System.Collections;

public static class Difficulty
{
    private const string DIFFICULTY_KEY = "PlayerDifficulty";

    public enum Level {
        Easy = 0,
        Normal = 1,
        Hard = 2
    }

    public static Level GetDifficultyLevel()
    {
        return (Level) PlayerPrefs.GetInt(DIFFICULTY_KEY, (int) Level.Normal);
    }

    public static void SetDifficultyLevel(Level newLevel)
    {
        PlayerPrefs.SetInt(DIFFICULTY_KEY, (int) newLevel);
    }
}
