using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private DialogueUI dialogueUI;
    
    //getter for dialogue ui
    public DialogueUI DialogueUI => dialogueUI;

    public IInteractable Interactable { get;set; } //can be set and read from outside

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlayerGetsDialogue ()
    {
        Interactable?.Interact(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueUI == null)
        {
            Debug.Log("DialogueUI is null in Player script!");
            return;
        }

        if (dialogueUI.IsOpen) return;
        
        
        
        ////example from video
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    interactable?.Interact(this); //null propagation
        //}
        
    }
}
