using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Battle1Controller : MonoBehaviour
{
    public string levelToLoad;

    public void BacktoBase()
    {
        SceneManager.LoadScene(levelToLoad);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
