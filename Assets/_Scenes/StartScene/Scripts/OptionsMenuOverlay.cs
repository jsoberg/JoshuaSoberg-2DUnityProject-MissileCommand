using UnityEngine;
using System.Collections;

public class OptionsMenuOverlay : MonoBehaviour
{
    public MainMenu MainMenu;

    private Rect WindowRect = new Rect((Screen.width - 300) / 2, (Screen.height - 100) / 2, 300, 300);
    private bool ShowWindow = false;
    private Difficulty.Level CurrentLevel;

    void Start()
    {
        MainMenu = MainMenu.GetComponent<MainMenu>();
    }

    public void Open()
    {
        MainMenu.SetButtonsActive(false);
        ShowWindow = true;
        CurrentLevel = Difficulty.GetDifficultyLevel();
    }

    void OnGUI()
    {
        if (ShowWindow) {
            WindowRect = GUI.Window(0, WindowRect, DialogWindow, "Choose Difficulty");
        }
    }

    void DialogWindow(int windowID)
    {
        SetColorForDifficulty(Difficulty.Level.Easy);
        if (GUI.Button(new Rect(5, 30, WindowRect.width - 10, 80), "Easy"))
        {
            DifficultySelected(Difficulty.Level.Easy);
        }

        SetColorForDifficulty(Difficulty.Level.Normal);
        if (GUI.Button(new Rect(5, 120, WindowRect.width - 10, 80), "Normal"))
        {
            DifficultySelected(Difficulty.Level.Normal);
        }

        SetColorForDifficulty(Difficulty.Level.Hard);
        if (GUI.Button(new Rect(5, 210, WindowRect.width - 10, 80), "Hard"))
        {
            DifficultySelected(Difficulty.Level.Hard);
        }
    }

    private void SetColorForDifficulty(Difficulty.Level level)
    {
        if (level == CurrentLevel) {
            GUI.color = Color.red;
        } else {
            GUI.color = Color.white;
        }
    }

    private void DifficultySelected(Difficulty.Level newLevel)
    {
        Difficulty.SetDifficultyLevel(newLevel);
        ShowWindow = false;
        MainMenu.SetButtonsActive(true);
    }
}