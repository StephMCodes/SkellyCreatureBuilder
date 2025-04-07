using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryIcon : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Item Settings")]
    public List<GameObject> worldObjectPrefabs; // list of the bone prefabs to spawn from the panel

    [Header("Drag Settings")]
    public Canvas canvas;
    public float spawnZ = 0f;

    private GameObject spawnedObject;
    private RectTransform rectTransform;

    public int prefabIndex = 0;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (prefabIndex >= 0 && prefabIndex < worldObjectPrefabs.Count)
        {
            // spawning the indexed prefab
            Vector3 worldPos = ScreenToWorld(eventData.position);
            spawnedObject = Instantiate(worldObjectPrefabs[prefabIndex], worldPos, Quaternion.identity);

            // removing it from list
            worldObjectPrefabs.RemoveAt(prefabIndex);


        }
        else
        {
            Debug.LogWarning("no more prefabs");
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (spawnedObject != null)
        {
            Vector3 worldPos = ScreenToWorld(eventData.position);
            spawnedObject.transform.position = worldPos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (spawnedObject != null)
        {
            Rigidbody rb = spawnedObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }
        }

        GetComponent<CanvasGroup>().alpha = 1f;
    }

    private Vector3 ScreenToWorld(Vector2 screenPosition)
    {
        Vector3 pos = new Vector3(screenPosition.x, screenPosition.y, Mathf.Abs(Camera.main.transform.position.z - spawnZ));
        pos = Camera.main.ScreenToWorldPoint(pos);
        pos.z = spawnZ;
        return pos;
    }
}
