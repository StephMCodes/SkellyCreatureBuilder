using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscForPause : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused; // Toggle pause state
        pauseMenu.SetActive(isPaused); // Show or hide menu
        Time.timeScale = isPaused ? 0 : 1; // Pause or resume game
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false); // Hide menu
        Time.timeScale = 1; // Resume game
    }
}

