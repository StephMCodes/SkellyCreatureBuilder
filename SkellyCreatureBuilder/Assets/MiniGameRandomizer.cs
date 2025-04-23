using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameRandomizer : MonoBehaviour
{
    // available game scene 
    [SerializeField] private List<string> miniGames;

    // stores the 3 selected mini game names
    public static List<string> SelectedMiniGames { get; private set; } = new List<string>();

    // called when the player clicks  button
    public void StartMinigameSequence()
    {
        RandomizeMiniGames();

        Debug.Log(" Mini-game sequence started!");
        Debug.Log("Selected MiniGames: " + string.Join(", ", SelectedMiniGames));
    }

    // picks 3 random mini-game scenes without repeating
    private void RandomizeMiniGames()
    {
        SelectedMiniGames.Clear();

        List<string> tempPool = new List<string>(miniGames);

        for (int i = 0; i < 3 && tempPool.Count > 0; i++)
        {
            int index = Random.Range(0, tempPool.Count);
            SelectedMiniGames.Add(tempPool[index]);
            tempPool.RemoveAt(index);
        }
    }
}
