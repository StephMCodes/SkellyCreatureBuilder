using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGamePlayer : MonoBehaviour
{
    private int current = 0;

    public void PlayNextMiniGame()
    {
        Debug.Log(" MiniGame List: " + string.Join(", ", MiniGameRandomizer.SelectedMiniGames));

        if (current < MiniGameRandomizer.SelectedMiniGames.Count)
        {
            string sceneToLoad = MiniGameRandomizer.SelectedMiniGames[current];
            current++;
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            SceneManager.LoadScene(MiniGameState.NextBaseScene);
        }
    }
}
