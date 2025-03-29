using UnityEditor.Build.Player;
using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable
{
    //handles the call to dialogue ui interaction

    [SerializeField] private DialogueObject dialogueObject; //the convo of our character
    public void Interact(Player player)
    {
        player.DialogueUI.ShowDialogue(dialogueObject);
    }

}
