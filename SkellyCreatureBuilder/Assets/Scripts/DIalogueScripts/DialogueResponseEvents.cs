using UnityEngine;
using System;

public class DialogueResponseEvents : MonoBehaviour
{
   //this is supposed to go on the DIALOGUE CHARACTER along with Dialogue Activator
    
    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private ResponseEvent[] events;

    public DialogueObject DialogueObject => dialogueObject;

    //get of events
    public ResponseEvent[] Events => events;

    public void OnValidate()
    {
        if (dialogueObject == null) return;
        if (dialogueObject.Responses == null) return;
        if (events != null && events.Length == dialogueObject.Responses.Length) return;

        //init events array
        if (events == null)
        {
            events = new ResponseEvent[dialogueObject.Responses.Length];
        }
        else
        {
            Array.Resize(ref events, dialogueObject.Responses.Length);
        }
        
        for (int i = 0; i < dialogueObject.Responses.Length; i++)
        {
            Response response = dialogueObject.Responses[i];
            //check if event does already exist
            if (events[i] != null)
            {
                events[i].name = response.ResponseText;
                continue;
            }

            events[i] = new ResponseEvent() { name = response.ResponseText };
        }
        
    
    }
}
