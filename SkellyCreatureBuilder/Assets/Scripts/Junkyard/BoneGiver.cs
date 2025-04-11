using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;




public class BoneGiver : MonoBehaviour
{
    public GameObject[] bonePrehabs;
    public Transform spawnPoint;
    public TMP_Text spawnText;
    public Vector3 spawnAreaSize = new Vector3(2f, 0f, 2f); 


    void Start()
    {
        if (spawnText != null)
        {
            spawnText.text = "Click the Take Bones button to get bones!";
         }
    }
    public void GiveBones()
    {
        int numberOfBones = Random.Range(1, 6);

        if (spawnText != null)
        {
            spawnText.text = "You got " + numberOfBones + " bones!";
        }
        for (int i = 0; i < numberOfBones; i++)
        {
            SpawnBones();
        }

    }
    void SpawnBones()
    {
        if (bonePrehabs.Length == 0 || spawnPoint == null)
            return;

        int index = Random.Range(0, bonePrehabs.Length);
        GameObject randomBone = bonePrehabs[index];

        Vector3 randomOffset = new Vector3(
            Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f),
            0f,
            0f
        );

        Vector3 spawnPosition = spawnPoint.position + randomOffset;

        Instantiate(randomBone, spawnPosition, Quaternion.identity);
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
