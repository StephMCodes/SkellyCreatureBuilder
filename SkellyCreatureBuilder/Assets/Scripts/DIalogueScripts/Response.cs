using UnityEngine;
[System.Serializable] //necessary to serialize responses field in inspector!
public class Response
{
    //text to show on the response box ui
    [SerializeField] private string responseText;
    //pointer (reference) to a dialogue object
    [SerializeField] private DialogueObject dialogueObject;

    //getters
    public string ResponseText => responseText;
    public DialogueObject DialogueObject => dialogueObject;

}
