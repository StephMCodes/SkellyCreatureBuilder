using TMPro;
using System.Collections;
using UnityEngine;
using Unity.VisualScripting;

public class DialogueUI : MonoBehaviour
{
    //Responsible for making the text appear on the dialogue UI.

    //Reference to text label.
    //The canvas should reference the text child component of dialogue box (drag and drop into textLabel)
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject testDialogue;

    //reference to dialogue box make sure to drag and drop to canvas in editor
    [SerializeField] private GameObject dialogueBox;

    private TypewriterEffect typewriterEffect;

    //reference to response handler
    private ResponseHandler responseHandler;

    private void Start()
    {
        typewriterEffect = GetComponent<TypewriterEffect>();
        responseHandler = GetComponent<ResponseHandler>();

        CloseDialogueBox(); //clean up

        ShowDialogue(testDialogue); //pass dialogue object


        //test 1
        //textLabel.text = "Hello!\nThis is my second line.";

        //test 2
        //GetComponent<TypewriterEffect>().Run("Hello again!\nThis is my second line.", textLabel);
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        //show box
        dialogueBox.SetActive(true);

        //start coroutine to wait before each of the entries of dialogue
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        //foreach (string dialogue in dialogueObject.Dialogue)
        //{
        //    yield return typewriterEffect.Run(dialogue, textLabel);
        //    //wait for input when typewriter effect is done
        //    yield return new WaitUntil(() => Input.GetKey(KeyCode.Space));
        //}
        //CloseDialogueBox();

        //the foreach becomes a for loop because if there are options
        //we dont want user to skip them by pressing spacebar
        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {

            string dialogue = dialogueObject.Dialogue[i];
            yield return typewriterEffect.Run(dialogue, textLabel);

            //check if at end of dialogue
            if (i==dialogueObject.Dialogue.Length -1 && dialogueObject.HasResponses) break;
            
                //wait for input when typewriter effect is done
                yield return new WaitUntil(() => Input.GetKey(KeyCode.Space));
        }

        //we dont want to close the box while you still have answers to give
        if (dialogueObject.HasResponses)
        {
            responseHandler.ShowResponses(dialogueObject.Responses);
        }
        else
        {
            CloseDialogueBox();
        }

    }

    //closing the dialogue box
    private void CloseDialogueBox()
    {
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }
}
