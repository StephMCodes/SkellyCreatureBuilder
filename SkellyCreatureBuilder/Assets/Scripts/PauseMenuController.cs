using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public string levelToLoad;
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturntoMainMenu()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
