using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Battle3Controller : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private string[] sceneNames =
        {
        "TriviaMemory",
        "TrueFalseMemory"
        };
    public void LoadRandomScene()
    {
        int randomIndex = Random.Range(0, sceneNames.Length);
        SceneManager.LoadScene(sceneNames[randomIndex]);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
