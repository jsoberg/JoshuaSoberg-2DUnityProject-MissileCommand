using UnityEngine;
using System.Collections;

public class Level
{
    private static int CurrentLevel = 0;

    public static void NextLevel()
    {
        CurrentLevel++;
    }

    public static int GetCurrentLevel()
    {
        return CurrentLevel;
    }

    public static void Reset()
    {
        CurrentLevel = 0;
    }
}
