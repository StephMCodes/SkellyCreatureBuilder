using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneSpawner : MonoBehaviour
{
    public GameObject[] iconPrefabs; // Assign icon prefabs in Inspector
    public float spawnRate = 0.5f;
    public float gravityScale = 1f;

    [Header("Spawn Area Settings")]
    public Vector2 areaSize = new Vector2(5f, 2f); // Width and height of the spawn area

    void Start()
    {
        InvokeRepeating(nameof(SpawnIcon), 0, spawnRate);
    }

    void SpawnIcon()
    {
        if (iconPrefabs.Length == 0) return;

        // Random offset within the spawn area
        Vector2 randomOffset = new Vector2(
            Random.Range(-areaSize.x / 2f, areaSize.x / 2f),
            Random.Range(-areaSize.y / 2f, areaSize.y / 2f)
        );

        // Final spawn position in world space
        Vector3 spawnPosition = transform.position + (Vector3)randomOffset;

        GameObject icon = Instantiate(iconPrefabs[Random.Range(0, iconPrefabs.Length)], spawnPosition, Quaternion.identity);

        // Add Rigidbody2D for falling effect
        Rigidbody2D rb = icon.GetComponent<Rigidbody2D>();
        if (rb == null)
            rb = icon.AddComponent<Rigidbody2D>();

        rb.gravityScale = gravityScale;
    }

    // Optional: Draw the spawn area in Scene view
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, areaSize);
    }
}
