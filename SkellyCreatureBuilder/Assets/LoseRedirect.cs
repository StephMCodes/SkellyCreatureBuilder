using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class LoseRedirect : MonoBehaviour
{
    public void SendPlayerToCorrectFailBase()
    {
        string fromBase = MiniGameState.CurrentBase;

        if (MiniGameState.FailBaseLookup.TryGetValue(fromBase, out string failBase))
        {
            Debug.Log("Redirecting to fail base: " + failBase);

            MiniGameState.NextBaseScene = failBase;
            MiniGamePlayer.ResetCycle();
            SceneManager.LoadScene(failBase);
        }
        else
        {
            Debug.LogError("No fail base defined for: " + fromBase);
        }
    }

}

