using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrapsTutoScript : MonoBehaviour
{
    [SerializeField] private Button Forest;
    [SerializeField] private Button Yard;

    // Start is called before the first frame update
    void Start()
    {
        
    }

   public void ActivateBase()
    {
        Forest.interactable = true;
        Yard.interactable = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
