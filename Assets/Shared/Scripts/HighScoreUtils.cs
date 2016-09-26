using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public static class HighScoreUtils
{
    public static readonly string HIGH_SCORES_KEY = "HighScoresKey";
    public static readonly int MAX_HIGH_SCORES = 10;

    public static Queue<HighScore> GetHighScores()
    {
        Queue<HighScore> scoresList = new Queue<HighScore>();

        string[] highScores = PlayerPrefsX.GetStringArray(HIGH_SCORES_KEY);
        foreach (string highScore in highScores) {
            string[] nameAndScore = highScore.Split(';');

            string initials = nameAndScore[0];
            int score = Int32.Parse(nameAndScore[1]);
            scoresList.Enqueue(new HighScore(initials, score));
        }

        return scoresList;
    }

    public static void AddHighScore(string initials, int score)
    {
        HighScore newScore = new HighScore(initials, score);
        Queue<HighScore> scoresQueue = GetHighScores();
        HighScore[] scoresArray = scoresQueue.ToArray();

        for (int i = 0; i < scoresArray.Length; i ++) {
            if (newScore.Score > scoresArray[i].Score) {
                scoresQueue = PushScoreOntoListForPosition(scoresQueue, newScore, i);
                PrintScoresToPlayerPrefs(scoresQueue);
                return;
            }
        }
    }

    private static Queue<HighScore> PushScoreOntoListForPosition(Queue<HighScore> scoresQueue, HighScore newScore, int position)
    {
        Queue<HighScore> newQueue = new Queue<HighScore>();
        for (int i = 0; i < position; i ++) {
            newQueue.Enqueue(scoresQueue.Dequeue());
        }
        newQueue.Enqueue(newScore);
        for (int i = position; i < scoresQueue.Count && i < MAX_HIGH_SCORES; i++) {
            newQueue.Enqueue(scoresQueue.Dequeue());
        }

        return newQueue;
    }

    private static void PrintScoresToPlayerPrefs(Queue<HighScore> scoresQueue)
    {
        string[] scores = new string[scoresQueue.Count]; 
        for (int i = 0; i < scoresQueue.Count; i++) {
            scores[i] = scoresQueue.Dequeue().Combined();
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
