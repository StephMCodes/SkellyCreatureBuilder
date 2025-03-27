using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    [SerializeField][TextArea] private string[] dialogue; //elements of array are dialogue box texts

    //we want the dialogueui to access this but not write to it
    public string[] Dialogue => dialogue; //returns private dialogue string array
}
