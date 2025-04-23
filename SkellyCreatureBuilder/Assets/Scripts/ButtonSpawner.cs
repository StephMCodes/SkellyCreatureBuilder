using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSpawner : MonoBehaviour
{
    public GameObject buttonPrefab; // assign your button prefab in inspector
    public Transform parentTransform; // where to spawn the new button (like a panel)

    public void OnOriginalButtonClicked()
    {
        Instantiate(buttonPrefab, parentTransform);
    }
}
