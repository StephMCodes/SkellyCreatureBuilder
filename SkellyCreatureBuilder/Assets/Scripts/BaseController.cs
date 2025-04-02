using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseController : MonoBehaviour
{
    public string levelToLoad;

    public void OpenBattle1()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void TowerUI() 
    {

    }

    public void GraveyardUI()
    {

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
