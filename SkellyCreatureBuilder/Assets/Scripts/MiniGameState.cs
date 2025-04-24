using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameState : MonoBehaviour
{
    // this holds the name of the next base scene after minigames
    public static string NextBaseScene = "Base2"; // default value can be overridden
    public static string CurrentBase = "Base1"; // added to track where player came from

    // Map of which fail scene matches which base
    public static Dictionary<string, string> FailBaseLookup = new Dictionary<string, string>
    {
        { "Base1", "BaseLoss" },
        { "Base2", "BaseLoss2" },
        { "Base3", "BassLoss3" }
    };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
