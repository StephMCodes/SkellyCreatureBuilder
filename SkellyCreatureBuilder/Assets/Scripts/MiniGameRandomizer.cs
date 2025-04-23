using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameRandomizer : MonoBehaviour
{
    [SerializeField] private List<string> miniGamePool;
    [SerializeField] private MiniGamePlayer miniGamePlayer;

    public static List<string> SelectedMiniGames { get; private set; } = new List<string>();

    public void StartAndPlaySequence()
    {
        SelectedMiniGames.Clear();
        List<string> tempPool = new List<string>(miniGamePool);

        for (int i = 0; i < 3 && tempPool.Count > 0; i++)
        {
            int index = Random.Range(0, tempPool.Count);
            SelectedMiniGames.Add(tempPool[index]);
            tempPool.RemoveAt(index);
        }

        Debug.Log("Selected: " + string.Join(", ", SelectedMiniGames));

        // Now start the first minigame
        miniGamePlayer.PlayNextMiniGame();
    }
}
