using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [System.Serializable]
    public class ItemData
    {
        public Sprite itemSprite;
        public GameObject prefabToSpawn;

        [HideInInspector]
        public GameObject uiObject; 
    }

    public List<ItemData> itemsToAdd = new List<ItemData>();

    public Button dropButton;
    public Transform hotbar;
    public GameObject itemUIPrefab;
    public float dropInterval = 0.5f;
    public Transform spawnPoint;

    private LinkedList<ItemData> inventoryItems = new LinkedList<ItemData>();
    private bool isDropping = false;

    void Start()
    {
        foreach (var item in itemsToAdd)
        {
            AddItem(item);
        }
    }

    void AddItem(ItemData item)
    {
        if (item.itemSprite == null)
        {
            Debug.LogError("Sprite is null, unable to assign to UI.");
            return;
        }

        GameObject newUI = Instantiate(itemUIPrefab, hotbar);
        newUI.GetComponent<Image>().sprite = item.itemSprite;

        item.uiObject = newUI; 
        inventoryItems.AddLast(item);
    }

    public void StartDropping()
    {
        if (!isDropping)
            StartCoroutine(DropItemsOneByOne());
    }

    IEnumerator DropItemsOneByOne()
    {
        isDropping = true;

        while (inventoryItems.Count > 0)
        {
            ItemData item = inventoryItems.First.Value;
            inventoryItems.RemoveFirst();

            if (item.uiObject != null)
                Destroy(item.uiObject);

            if (item.prefabToSpawn != null)
                Instantiate(item.prefabToSpawn, spawnPoint.position, Quaternion.identity);

            yield return new WaitForSeconds(dropInterval);
        }

        isDropping = false;
    }
}
