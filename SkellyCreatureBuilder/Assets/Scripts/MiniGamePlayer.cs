using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGamePlayer : MonoBehaviour
{
    private static int current = 0;

    public void PlayNextMiniGame()
    {
        Debug.Log($"PlayNextMiniGame: current = {current}, list count = {MiniGameRandomizer.SelectedMiniGames.Count}");

        Debug.Log(" MiniGame List: " + string.Join(", ", MiniGameRandomizer.SelectedMiniGames));

        if (current < MiniGameRandomizer.SelectedMiniGames.Count)
        {
            string sceneToLoad = MiniGameRandomizer.SelectedMiniGames[current];
            current++;
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            current = 0;
            SceneManager.LoadScene(MiniGameState.NextBaseScene);
        }
    }
}
