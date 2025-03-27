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

    private TypewriterEffect typewriterEffect;
    
    private void Start()
    {
        typewriterEffect = GetComponent<TypewriterEffect>();

        ShowDialogue(testDialogue); //pass dialogue object

        
        //test 1
        //textLabel.text = "Hello!\nThis is my second line.";

        //test 2
        //GetComponent<TypewriterEffect>().Run("Hello again!\nThis is my second line.", textLabel);
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        //start coroutine to wait before each of the entries of dialogue
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        foreach (string dialogue in dialogueObject.Dialogue)
        {
            yield return typewriterEffect.Run(dialogue, textLabel);
        }
    }
}
