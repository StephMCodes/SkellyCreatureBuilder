using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBones : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        BoneDetector.mental = 0;
        BoneDetector.strength = 0;
        BoneDetector.speed = 0;
        Debug.Log("Bones reset to zero.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
