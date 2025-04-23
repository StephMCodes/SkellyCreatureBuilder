using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreatureBuildingScene : MonoBehaviour
{
    [SerializeField] private string nextBaseScene = "Base2";

    public void EnterGameScene()
    {
        MiniGameState.NextBaseScene = nextBaseScene;
        SceneManager.LoadScene("GameScene");
    }
}


