using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour 
{
    public enum DelayedButtonAction
    {
        Start,
        HighScores,
        Options,
        Exit,
        None
    };

    public Text GameNameText;
    public Button StartGameButton;
    public Button HighScoresButton;
    public Button OptionsButton;
    public Button ExitButton;

    public OptionsMenuOverlay OptionsMenu;

	public AudioSource BackgroundMusic;
    public AudioSource ButtonClickSound;

    private DelayedButtonAction CurrentDelayedButtonAction = DelayedButtonAction.None;

	void Start () 
    {

	}

    void Update()
    {
        if (CurrentDelayedButtonAction != DelayedButtonAction.None && !ButtonClickSound.isPlaying)
        {
			bool shouldContinuePlayBackground = false;
            switch (CurrentDelayedButtonAction) {
			    case DelayedButtonAction.Start:
					SceneManager.LoadScene("GameScene");
                    break;
                case DelayedButtonAction.HighScores:
                    SceneManager.LoadScene("HighScoreScene");
                    break;
                case DelayedButtonAction.Options:
                    OptionsMenu.Open();
                    shouldContinuePlayBackground = true;
                    break;
                case DelayedButtonAction.Exit:
                    Application.Quit();
                    break;

            }

			CurrentDelayedButtonAction = DelayedButtonAction.None;
			if (shouldContinuePlayBackground) {
				BackgroundMusic.Play ();
			}
        }
    }
	
    public void StartGameButtonClick()
    {
        CurrentDelayedButtonAction = DelayedButtonAction.Start;
        AnyButtonClicked();
    }

    public void HighScoresButtonClick()
    {
        CurrentDelayedButtonAction = DelayedButtonAction.HighScores;
        AnyButtonClicked();
    }

    public void OptionsButtonClick()
    {
        CurrentDelayedButtonAction = DelayedButtonAction.Options;
        AnyButtonClicked();
    }

    public void ExitButtonClick()
    {
        CurrentDelayedButtonAction = DelayedButtonAction.Exit;
        AnyButtonClicked();
    }

    private void AnyButtonClicked()
    {
		BackgroundMusic.Pause();
        ButtonClickSound.Play();
    }

    public void SetButtonsActive(bool active)
    {
        StartGameButton.gameObject.SetActive(active);
        HighScoresButton.gameObject.SetActive(active);
        OptionsButton.gameObject.SetActive(active);
        ExitButton.gameObject.SetActive(active);
    }
}
