using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private DialogueUI dialogueUI;
    
    //getter for dialogue ui
    public DialogueUI DialogueUI => dialogueUI;

    public IInteractable interactable { get;set; } //can be set and read from outside

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ////example from video
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    interactable?.Interact(this); //null propagation
        //}
        
    }
}
