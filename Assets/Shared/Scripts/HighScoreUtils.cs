using UnityEngine;
using System;
using System.Collections.Generic;

public static class HighScoreUtils
{
    public static readonly string HIGH_SCORES_KEY = "HighScoresKey";
    public static readonly int MAX_HIGH_SCORES = 10;

    public static SortedList<int, HighScore> GetHighScores()
    {
        SortedList<int, HighScore> scoresList = new SortedList<int, HighScore>(new SortIntDescending());

        string[] highScores = PlayerPrefsX.GetStringArray(HIGH_SCORES_KEY);
        foreach (string highScore in highScores) {
            string[] nameAndScore = highScore.Split(';');

            string initials = nameAndScore[0];
            int score = Int32.Parse(nameAndScore[1]);
            scoresList.Add(score, new HighScore(initials, score));
        }

        return scoresList;
    }

    public static void AddHighScore(string initials, int score)
    {
        HighScore newScore = new HighScore(initials, score);
        SortedList<int, HighScore> scoresQueue = GetHighScores();
        scoresQueue.Add(score, new HighScore(initials, score));
        PrintScoresToPlayerPrefs(scoresQueue);
    }

    private static void PrintScoresToPlayerPrefs(SortedList<int, HighScore> scoresQueue)
    {
        string[] scores = new string[scoresQueue.Count];
        IList<int> keys = scoresQueue.Keys;
        for (int i = 0; i < scoresQueue.Count && i < MAX_HIGH_SCORES; i ++) {
            scores[i] = scoresQueue[keys[i]].Combined();
        }

        PlayerPrefsX.SetStringArray(HIGH_SCORES_KEY, scores);
    }
}

public class HighScore
{
    public readonly string Initials;
    public readonly int Score;

    internal HighScore(string initials, int score)
    {
        this.Initials = initials;
        this.Score = score;
    }

    public string Combined()
    {
        return Initials + ";" + Score;
    }
}

public class SortIntDescending : IComparer<int>
{
    int IComparer<int>.Compare(int a, int b)
    {
        if (a > b)
            return -1;
        if (a < b)
            return 1;
        else
            return 0;
    }
}
