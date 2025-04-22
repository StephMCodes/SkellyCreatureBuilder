using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrapsTutoScript : MonoBehaviour
{
    //[SerializeField] private Button Forest;
    [SerializeField] private Button Ascend;

    [SerializeField] private DialogueObject dialogueObject; //the convo of our character
    [SerializeField] private Player player;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Interact(Player player)
    {
        if (TryGetComponent(out DialogueResponseEvents responseEvents) && responseEvents.DialogueObject == dialogueObject)
        {
            player.DialogueUI.AddResponseEvents(responseEvents.Events);
        }
        player.DialogueUI.ShowDialogue(dialogueObject);
    }
    public void ClickDialogue()
    {
        if (player.DialogueUI.IsOpen == false)
            Interact(player);
        Ascend.gameObject.SetActive(true);  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
