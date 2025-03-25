using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneSpawner : MonoBehaviour
{
    public GameObject[] iconPrefabs; // Assign icon prefabs in Inspector
    public float spawnRate = 0.5f;
    public float gravityScale = 1f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnIcon), 0, spawnRate);
    }

    void SpawnIcon()
    {
        if (iconPrefabs.Length == 0) return;

        // Spawn at BoneSpawner's position
        Vector3 spawnPosition = transform.position;

        GameObject icon = Instantiate(iconPrefabs[Random.Range(0, iconPrefabs.Length)], spawnPosition, Quaternion.identity);

        // Add Rigidbody2D for falling effect
        Rigidbody2D rb = icon.GetComponent<Rigidbody2D>();
        if (rb == null)
            rb = icon.AddComponent<Rigidbody2D>();

        rb.gravityScale = gravityScale;
    }
}
