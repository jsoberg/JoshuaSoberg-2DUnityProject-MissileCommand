using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public Texture BackgroundTexture;

    void OnGUI()
    {
        // Load background texture.
        Rect screenRect = new Rect(0,0,Screen.width, Screen.height);
        GUI.DrawTexture(screenRect, BackgroundTexture);
    }
}
