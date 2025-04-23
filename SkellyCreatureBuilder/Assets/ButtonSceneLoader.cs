using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonSceneLoader : MonoBehaviour
{
    public string sceneName; // Set the target scene name in the Inspector

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(LoadScene);
    }

    void LoadScene()
    {
        SceneManager.LoadScene("Battle1");
    }
}
