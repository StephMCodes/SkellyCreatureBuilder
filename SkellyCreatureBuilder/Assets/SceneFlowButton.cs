using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFlowButton : MonoBehaviour
{
    public void OnSceneFlowButtonPressed()
    {
        if (ScneFlowManagement.Instance != null)
            ScneFlowManagement.Instance.LoadNextScene();
        else
            Debug.LogError("SceneFlowManager not found in the scene.");
    }
}
