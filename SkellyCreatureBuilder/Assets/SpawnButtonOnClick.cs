using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnButtonOnClick : MonoBehaviour
{
    public GameObject buttonPrefab; // assign in Inspector
    public Transform parentCanvas;  // assign in Inspector

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(SpawnNewButton);
    }

    void SpawnNewButton()
    {
        GameObject newButton = Instantiate(buttonPrefab, parentCanvas);

        RectTransform rt = newButton.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0.5f, 0f);
        rt.anchorMax = new Vector2(0.5f, 0f);
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = new Vector2(0f, 50f);
    }
}
