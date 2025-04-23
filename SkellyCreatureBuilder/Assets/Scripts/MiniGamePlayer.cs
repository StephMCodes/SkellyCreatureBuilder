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
            Debug.Log("All mini-games done. Going to: " + MiniGameState.NextBaseScene);
            SceneManager.LoadScene(MiniGameState.NextBaseScene);
        }
    }
}
