using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour 
{
    public enum DelayedButtonAction
    {
        Start,
        Options,
        Exit,
        None
    };

    public Text GameNameText;
    public Button StartGameButton;
    public Button OptionsButton;
    public Button ExitButton;

	public AudioSource BackgroundMusic;
    public AudioSource ButtonClickSound;

    private DelayedButtonAction CurrentDelayedButtonAction = DelayedButtonAction.None;

	void Start () 
    {
        GameNameText = GameNameText.GetComponent<Text>();

        StartGameButton = StartGameButton.GetComponent<Button>();
        OptionsButton = OptionsButton.GetComponent<Button>();
        ExitButton = ExitButton.GetComponent<Button>();
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
                case DelayedButtonAction.Options:
                    // Stub
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
}
