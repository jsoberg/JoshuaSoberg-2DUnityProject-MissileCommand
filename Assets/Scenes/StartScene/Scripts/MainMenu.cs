using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour 
{
    public Text GameNameText;

    public Button StartGameButton;
    public Button OptionsButton;
    public Button ExitButton;

	void Start () 
    {
        GameNameText = GameNameText.GetComponent<Text>();

        StartGameButton = StartGameButton.GetComponent<Button>();
        OptionsButton = OptionsButton.GetComponent<Button>();
        ExitButton = ExitButton.GetComponent<Button>();
	}
	
    public void StartGameButtonClick()
    {
        Application.LoadLevel(1);
    }

    public void OptionsButtonClick()
    {
        // Stub.
    }

    public void ExitButtonClick()
    {
        Application.Quit();
    }
}
