using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BringToLife : MonoBehaviour
{

    public Ribcage ribcage; // reference to the Ribcage script

    public GameObject monster; // reference to the root which is also Ribcage

    public Button bringToLifeButton; // reference to the bring to life button

    void Start()
    {
        Button button = GetComponent<Button>();

    }

    public void OnClick()
    {
        // Trigger the BringToLife method when the button is clicked
        if (ribcage != null)
        {
            ribcage.BringToLife(); // Start wiggling the bones
        }

        Debug.Log("monster is now alive!");
    }


}
