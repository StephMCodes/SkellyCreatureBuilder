using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScneFlowManagement : MonoBehaviour
{
    [Header("Mini game scenes")]
    [SerializeField] private List<string> allMiniScenes;

    [Header("Base scenes in order (Base1–Base4)")]
    [SerializeField] private List<string> baseScenes;

    [Header("Scene to enter before minigames")]
    [SerializeField] private string buildCreatureScene = "BuildCreature";

    private List<string> unusedMiniScenes = new List<string>();
    private List<string> currentCycleScenes = new List<string>();

    private int currentMiniSceneIndex = 0;
    private int currentBaseIndex = 0;
    private int cyclesCompleted = 0;
    private bool enteringFromBuildScene = false;

    public static ScneFlowManagement Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            ResetMiniScenePool();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void ResetMiniScenePool()
    {
        unusedMiniScenes = new List<string>(allMiniScenes);
    }

    void StartNewMiniSceneCycle()
    {
        currentCycleScenes.Clear();

        for (int i = 0; i < 3 && unusedMiniScenes.Count > 0; i++)
        {
            int randIndex = Random.Range(0, unusedMiniScenes.Count);
            currentCycleScenes.Add(unusedMiniScenes[randIndex]);
            unusedMiniScenes.RemoveAt(randIndex);
        }

        if (unusedMiniScenes.Count == 0 && allMiniScenes.Count >= 3)
        {
            ResetMiniScenePool();
        }

        currentMiniSceneIndex = 0;
    }

    public void LoadNextScene()
    {
        if (SceneManager.GetActiveScene().name == buildCreatureScene)
        {
            enteringFromBuildScene = true;
            StartNewMiniSceneCycle();
            LoadNextScene(); // immediately go to first mini-game
            return;
        }

        if (enteringFromBuildScene)
        {
            enteringFromBuildScene = false;
            if (currentMiniSceneIndex < currentCycleScenes.Count)
            {
                string miniScene = currentCycleScenes[currentMiniSceneIndex];
                currentMiniSceneIndex++;
                SceneManager.LoadScene(miniScene);
            }
            else
            {
                cyclesCompleted++;

                if (currentBaseIndex + 1 < baseScenes.Count)
                {
                    currentBaseIndex++;
                    SceneManager.LoadScene(baseScenes[currentBaseIndex]);
                }
                else
                {
                    Debug.Log("All bases complete — end of game.");
                }
            }
            return;
        }

        // From a base scene: go to BuildCreature next
        SceneManager.LoadScene(buildCreatureScene);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == baseScenes[0])
        {
            enteringFromBuildScene = false;
        }
    }
}
