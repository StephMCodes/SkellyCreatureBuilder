using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGamePlayer : MonoBehaviour
{
    private int current = 0;

    public void PlayNextMiniGame()
    {
        if (current < MiniGameRandomizer.SelectedMiniGames.Count)
        {
            string sceneToLoad = MiniGameRandomizer.SelectedMiniGames[current];
            current++;
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            // use the globally stored scene name for the next base
            SceneManager.LoadScene(MiniGameState.NextBaseScene);
        }
    }
}
