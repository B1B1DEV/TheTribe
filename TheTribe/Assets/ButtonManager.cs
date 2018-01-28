using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{

    public void LoadGameScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    public void LoadCredits()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Credits");
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
