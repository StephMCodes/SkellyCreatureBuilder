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
        public GameObject uiObject; // created at runtime
    }

    public List<ItemData> itemsToAdd = new List<ItemData>();

    public Button dropButton;
    public Transform hotbar;
    public GameObject itemUIPrefab;
    public float dropInterval = 0.5f;
    public Transform spawnPoint;

    private LinkedList<ItemData> inventoryItems = new LinkedList<ItemData>();
    private bool isDropping = false;

    public int randomItemsToAdd = 3; 


    void Start()
    {
        if (itemsToAdd.Count > 0)
        {
            for (int i = 0; i < randomItemsToAdd; i++)
            {
                ItemData randomItem = itemsToAdd[Random.Range(0, itemsToAdd.Count)];

                // copy so u dont modify original
                ItemData newItem = new ItemData();
                newItem.prefabToSpawn = randomItem.prefabToSpawn;
                newItem.itemSprite = randomItem.itemSprite;

                AddItem(newItem);
            }
        }


        foreach (var bonePrefab in BoneTransfer.bonesToTransfer)
        {
            if (bonePrefab == null) continue;

            ItemData newItem = new ItemData();
            newItem.prefabToSpawn = bonePrefab;


            SpriteRenderer sr = bonePrefab.GetComponent<SpriteRenderer>();
            if (sr != null)
                newItem.itemSprite = sr.sprite;
            else
                Debug.LogWarning("Bone prefab " + bonePrefab.name + " has no SpriteRenderer!");

            AddItem(newItem);
        }

        // clear transferred bones after taking them
        BoneTransfer.bonesToTransfer.Clear();



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

        item.uiObject = newUI; // link the UI object to the item
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
