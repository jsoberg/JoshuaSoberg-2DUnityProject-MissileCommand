using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class NewHighScoreInput : MonoBehaviour
{
    public AudioSource AccessDeniedSound;

    public void Start()
    {
        GetComponent<InputField>().onValidateInput += delegate (string input, int charIndex, char addedChar) { return OnInputChanged("" + addedChar); };
    }

    private char MyValidate(char charToValidate)
    {
        //Checks if a dollar sign is entered....
        if (charToValidate == '$')
        {
            // ... if it is change it to an empty character.
            charToValidate = '\0';
        }
        return charToValidate;
    }

    public char OnInputChanged(string addedText)
    {
        int result;
        if(int.TryParse(addedText, out result)) {
            return '\0';
        }

        if (addedText != addedText.ToUpper()) {
            return (addedText.ToUpper())[0];
        }
        return addedText[0];
    }

    void OnGUI()
    {
        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return) {
            OnEnterNewHighScore();
        }
    }

    public void OnEnterNewHighScore()
    {
        string initials = GetComponent<InputField>().text;
        if (initials == null || initials.Length == 0) {
            AccessDeniedSound.Play();
            return;
        }

        HUDInventoryAndScoreController scoreController = (HUDInventoryAndScoreController)Object.FindObjectOfType(typeof(HUDInventoryAndScoreController));
        int score = scoreController.GetCurrentScore();
        HighScoreUtils.AddHighScore(initials, score);

        SceneManager.LoadScene("HighScoreScene");
    }
}
