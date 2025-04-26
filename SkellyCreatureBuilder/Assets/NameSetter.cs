using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NameSetter : MonoBehaviour
{
    public TMP_InputField inputField; 
    public TextMeshProUGUI nameTagText; 
    public Button bringToLifeButton;

    void Start()
    {
        nameTagText.gameObject.SetActive(false);

        bringToLifeButton.onClick.AddListener(SetName);
    }

    void SetName()
    {
        string enteredName = inputField.text;
        if (!string.IsNullOrEmpty(enteredName))
        {
            nameTagText.text = enteredName;
            nameTagText.gameObject.SetActive(true);
        }
    }
}
