using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class SkullHealthSystem : MonoBehaviour
{
    [Header("Skull Icons")]
    [SerializeField] private Image[] skullIcons; // Skull images in the UI

    [Header("No Skulls Warning")]
    [SerializeField] private TMP_Text noSkullsText; // Text: "No skulls/mental detected"

    public void UpdateSkullUI(int skullCount)
    {
        // Enable/disable skull icons based on how many you have
        for (int i = 0; i < skullIcons.Length; i++)
        {
            skullIcons[i].enabled = i < skullCount;
        }

        // Show or hide the warning text
        if (noSkullsText != null)
            noSkullsText.gameObject.SetActive(skullCount == 0);
    }
}
