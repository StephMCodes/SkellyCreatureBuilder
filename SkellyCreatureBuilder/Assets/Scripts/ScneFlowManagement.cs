using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScneFlowManagement : MonoBehaviour
{
    //public static ScneFlowManagement Instance;

    //[Header("Mini game scenes")]
    //[SerializeField] private List<string> allMiniScenes;

    //[Header("Base scenes in order (Base1–Base4)")]
    //[SerializeField] private List<string> baseScenes;

    //[Header("Scene to enter before minigames")]
    //[SerializeField] private string buildCreatureScene = "BuildCreature";

    //private List<string> unusedMiniScenes = new List<string>();
    //private List<string> currentCycleScenes = new List<string>();

    //private int currentMiniSceneIndex = 0;
    //private int currentBaseIndex = 0;
    //private int cyclesCompleted = 0;
    //private bool enteringFromBuildScene = false;

    //void Start()
    //{
    //    string currentScene = SceneManager.GetActiveScene().name;

    //    if (currentScene == "Loader")
    //    {
    //        SceneManager.LoadScene("MainMenu"); 
    //        return;
    //    }

    //    if (currentScene == buildCreatureScene)
    //    {
    //        enteringFromBuildScene = true;
    //        StartNewMiniSceneCycle();
    //        return;
    //    }
    //}
    //void Awake()
    //{
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //        DontDestroyOnLoad(gameObject);
    //        ResetMiniScenePool();
    //        SceneManager.sceneLoaded += OnSceneLoaded;
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    //private void ResetMiniScenePool()
    //{
    //    unusedMiniScenes = new List<string>(allMiniScenes);
    //}

    //private void StartNewMiniSceneCycle()
    //{
    //    currentCycleScenes.Clear();

    //    for (int i = 0; i < 3 && unusedMiniScenes.Count > 0; i++)
    //    {
    //        int randIndex = Random.Range(0, unusedMiniScenes.Count);
    //        currentCycleScenes.Add(unusedMiniScenes[randIndex]);
    //        unusedMiniScenes.RemoveAt(randIndex);
    //    }

    //    if (unusedMiniScenes.Count == 0 && allMiniScenes.Count >= 3)
    //    {
    //        ResetMiniScenePool();
    //    }

    //    currentMiniSceneIndex = 0;
    //}

    //public void LoadNextScene()
    //{
    //    // Called from BuildCreature scene
    //    if (SceneManager.GetActiveScene().name == buildCreatureScene)
    //    {
    //        enteringFromBuildScene = true;
    //        StartNewMiniSceneCycle();
    //        return; // Wait for player to press button before continuing
    //    }

    //    // After BuildCreature: load next mini-game
    //    if (enteringFromBuildScene)
    //    {
    //        enteringFromBuildScene = false;

    //        if (currentMiniSceneIndex < currentCycleScenes.Count)
    //        {
    //            string miniScene = currentCycleScenes[currentMiniSceneIndex];
    //            currentMiniSceneIndex++;
    //            SceneManager.LoadScene(miniScene);
    //        }
    //        else
    //        {
    //            cyclesCompleted++;

    //            if (currentBaseIndex + 1 < baseScenes.Count)
    //            {
    //                currentBaseIndex++;
    //                SceneManager.LoadScene(baseScenes[currentBaseIndex]);
    //            }
    //            else
    //            {
    //                Debug.Log("All base scenes completed — end of game.");
    //            }
    //        }

    //        return;
    //    }

    //    // From a base scene: go to BuildCreature
    //    SceneManager.LoadScene(buildCreatureScene);
    //}

    //private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    if (scene.name == baseScenes[0])
    //    {
    //        enteringFromBuildScene = false;
    //    }
    //}
}

