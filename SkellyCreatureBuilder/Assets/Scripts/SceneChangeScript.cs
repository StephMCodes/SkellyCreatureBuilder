using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeScript : MonoBehaviour
{
    public string levelToLoad;
    public void ChangeScene()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
