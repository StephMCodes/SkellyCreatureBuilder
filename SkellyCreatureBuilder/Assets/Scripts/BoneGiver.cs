using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;




public class BoneGiver : MonoBehaviour
{
    public GameObject[] bonePrefabs;
    public Transform spawnPoint;
    public TMP_Text spawnText;
    // Start is called before the first frame update
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
        if (bonePrefabs.Length == 0 || spawnPoint == null)
            return;
        int index = Random.Range(0, bonePrefabs.Length);
        GameObject randomBone = bonePrefabs[index];

        Instantiate(randomBone, spawnPoint.position, Quaternion.identity);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
