using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NewHighScoreEffects : MonoBehaviour
{
	void Start ()
    {
        StartCoroutine(FadeTextInAndOut(.5f));
	}

    public IEnumerator FadeTextInAndOut(float t)
    {
        Text NewHighScoreText = GetComponent<Text>();

        NewHighScoreText.color = new Color(NewHighScoreText.color.r, NewHighScoreText.color.g, NewHighScoreText.color.b, 0);
        while (NewHighScoreText.color.a < 1.0f)
        {
            NewHighScoreText.color = new Color(NewHighScoreText.color.r, NewHighScoreText.color.g, NewHighScoreText.color.b, NewHighScoreText.color.a + (Time.deltaTime / t));
            yield return null;
        }

        yield return new WaitForSeconds(.5f);

        while (NewHighScoreText.color.a > 0f)
        {
            NewHighScoreText.color = new Color(NewHighScoreText.color.r, NewHighScoreText.color.g, NewHighScoreText.color.b, NewHighScoreText.color.a - (Time.deltaTime / t));
            yield return null;
        }

        StartCoroutine(FadeTextInAndOut(2f));
    }
}
