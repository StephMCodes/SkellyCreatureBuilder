using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriviaSceneController : MonoBehaviour
{
    [SerializeField]
    private string[] sceneNames =
       {
        "TrueFalseMemory"
        };
    public void LoadRandomScene()
    {
        int randomIndex = Random.Range(0, sceneNames.Length);
        SceneManager.LoadScene(sceneNames[randomIndex]);
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
