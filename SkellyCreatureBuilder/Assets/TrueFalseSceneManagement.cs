using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrueFalseSceneManagement : MonoBehaviour
{
    public string levelToLoad;  
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene(levelToLoad);
        // Load the specified scene when the game starts
        //SceneManager.LoadScene(levelToLoad);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
