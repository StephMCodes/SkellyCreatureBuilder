using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public string levelToLoad;
    public void PlayGame()
    {
        SceneManager.LoadScene(levelToLoad);
    }
    public void exitGame()
    {
        Application.Quit();
    }


    // Update is called once per frame
    void Update()
    {

    }
}
