
using UnityEngine;
using UnityEngine.UI;


public class ClickDialogueActivator : MonoBehaviour, IInteractable
{
    //handles the call to dialogue ui interaction

    [SerializeField] private DialogueObject dialogueObject; //the convo of our character
    [SerializeField] private Player player;

    [SerializeField] private Button Forest;
    [SerializeField] private Button Yard;

    public void UpdateDialogueObject(DialogueObject dialogueObject)
    {
        this.dialogueObject = dialogueObject; //lets us change the dialogue
    }

    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        Debug.Log("Mouse is over GameObject.");
        //player.Interactable = this;
        //Interact(player);
    }

    void OnMouseDown()
    {
        if (player.DialogueUI.IsOpen == false)
            Interact(player);
        Forest.interactable = true;
        Yard.interactable = true;
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        Debug.Log("Mouse is no longer on GameObject.");
    }

    void Update()
    {
        //if (Input.GetMouseButtonDown(0) && player.DialogueUI.IsOpen == false)
        //    Interact(player);

        //if (Input.GetMouseButtonDown(1))
        //    Debug.Log("Pressed right-click.");

        //if (Input.GetMouseButtonDown(2))
        //    Debug.Log("Pressed middle-click.");
    }

    public void Interact(Player player)
    {
        if (TryGetComponent(out DialogueResponseEvents responseEvents) && responseEvents.DialogueObject == dialogueObject)
        {
            player.DialogueUI.AddResponseEvents(responseEvents.Events);
        }
        player.DialogueUI.ShowDialogue(dialogueObject);
    }

    //public void Interact(Player player)
    //{
    //    if (TryGetComponent(out DialogueResponseEvents responseEvents) && responseEvents.DialogueObject == dialogueObject)
    //    {
    //        player.DialogueUI.AddResponseEvents(responseEvents.Events);
    //    }
    //    player.DialogueUI.ShowDialogue(dialogueObject);
    //}

    public void ClickDialogue()
    {
        if (player.DialogueUI.IsOpen == false)
            Interact(player);
        Forest.interactable = true;
        Yard.interactable = true;
    }

}

