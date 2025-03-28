using TMPro;
using System.Collections;
using UnityEngine;

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
    
    private void Start()
    {
        typewriterEffect = GetComponent<TypewriterEffect>();

        CloseDialogueBox(); //clean

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
        foreach (string dialogue in dialogueObject.Dialogue)
        {
            yield return typewriterEffect.Run(dialogue, textLabel);
            //wait for input when typewriter effect is done
            yield return new WaitUntil(() => Input.GetKey(KeyCode.Space));
        }
        CloseDialogueBox();
    }

    //closing the dialogue box
    private void CloseDialogueBox()
    {
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }
}
