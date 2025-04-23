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

    //what to pass into for dialogue. this is for testing purposes (ep.5 removal)
    [SerializeField] private DialogueObject testDialogue;

    //reference to dialogue box make sure to drag and drop to canvas in editor
    [SerializeField] private GameObject dialogueBox;


    public bool IsOpen { get; private set; } //only dialogue ui can set true or false. other scripts have readonly access

    private TypewriterEffect typewriterEffect;

    //reference to response handler
    private ResponseHandler responseHandler;

    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        responseHandler.AddResponseEvents(responseEvents);
    }
    private void Start()
    {
        typewriterEffect = GetComponent<TypewriterEffect>();
        responseHandler = GetComponent<ResponseHandler>();
        //if (textLabel == null) Debug.LogError("TextLabel is NULL!");
        //if (typewriterEffect == null) Debug.LogError("TypewriterEffect is NULL!");
        //if (responseHandler == null) Debug.LogError("ResponseHandler is NULL!");
        CloseDialogueBox(); //clean up

        //method to make it appear on screen
        //ShowDialogue(testDialogue); //passing dialogue object


        //test 1
        //textLabel.text = "Hello!\nThis is my second line.";

        //test 2
        //GetComponent<TypewriterEffect>().Run("Hello again!\nThis is my second line.", textLabel);
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        //show box
        dialogueBox.SetActive(true);

        IsOpen = true;

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

            //yield return typewriterEffect.Run(dialogue, textLabel);
            yield return RunTypingEffect(dialogue);

            textLabel.text = dialogue; //we pulled it out of the typewriter effect earlier
            
            //check if at end of dialogue
            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;

            //add extra stop in case you press space so it doesnt jump to the yield return space
            yield return null;
            
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
            //Debug.LogError("Either responseHandler or dialogueObject.Responses is null!");
            CloseDialogueBox();
        }

    }

    private IEnumerator RunTypingEffect(string dialogue)
    {
        //Debug.Log("Running typing effect...");
        //Debug.Log("TypewriterEffect is: " + typewriterEffect);
       // Debug.Log("TextLabel is: " + textLabel);

        typewriterEffect.Run(dialogue, textLabel);
        while (typewriterEffect.IsRunning)
        {
            yield return null; //wait one frame
            if (Input.GetKeyDown(KeyCode.LeftShift)) //space does not work
            {
                typewriterEffect.Stop();
            }
        }
    }

    //closing the dialogue box
    public void CloseDialogueBox()
    {
        dialogueBox.SetActive(false);
        IsOpen = false;
        textLabel.text = string.Empty;
    }
    
}
