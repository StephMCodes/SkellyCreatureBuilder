using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreatureBuildingScene : MonoBehaviour
{
    [SerializeField] private string nextBaseScene = "Base2";
    [SerializeField] private string currentBase = "Base1"; // Add this field

    public void EnterGameScene()
    {
        MiniGameState.NextBaseScene = nextBaseScene;
        MiniGameState.CurrentBase = currentBase; // Store which base this run is from
        MiniGamePlayer.ResetCycle();
        SceneManager.LoadScene("GameScene");
    }
}


