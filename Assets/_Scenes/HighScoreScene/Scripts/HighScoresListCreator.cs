using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HighScoresListCreator : MonoBehaviour
{
    private const string DEFAULT = "NO HIGH SCORES ACHIEVED";
    private const string HIGH_SCORE_FORMAT = "{0}. {1} - {2}\n";

	void Start ()
    {
        SetHighScoresList();
	}
	
    private void SetHighScoresList()
    {
        string text = "";
        SortedList<int, HighScore> highScores = HighScoreUtils.GetHighScores();
        int count = highScores.Count;
        IList<int> keys = highScores.Keys;
        for (int i = 0; i < count && i < HighScoreUtils.MAX_HIGH_SCORES; i ++) {
            int nextEntryNum = (i + 1);
            HighScore current = highScores[keys[i]];
            text += string.Format(HIGH_SCORE_FORMAT, nextEntryNum, current.Initials, current.Score);
        }

        GetComponent<Text>().text = (text != null) ? text : DEFAULT;
    }
}
