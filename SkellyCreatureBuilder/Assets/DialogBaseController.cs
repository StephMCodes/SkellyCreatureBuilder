using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogBaseController : MonoBehaviour
{

    [SerializeField] GameObject DialoguePanel;
    public static bool hasBeenToBase = false;
    
    // Start is called before the first frame update
    void Start()
    {
        if (!hasBeenToBase)
        {
            DialoguePanel.gameObject.SetActive(true);
            hasBeenToBase = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
