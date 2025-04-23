using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHiding : MonoBehaviour
{
    [Header("Assign the button to reveal")]
    public GameObject buttonToReveal;

    public void OnTriggerButtonPressed()
    {
        if (buttonToReveal != null)
        {
            buttonToReveal.SetActive(true);
        }
    }
}
