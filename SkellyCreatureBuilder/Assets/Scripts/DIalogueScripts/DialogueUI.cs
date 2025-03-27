using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    //Responsible for making the text appear on the dialogue UI.

    //Reference to text label.
    //The canvas should reference the text child component of dialogue box (drag and drop into textLabel)
    [SerializeField] private TMP_Text textLabel;
    
    private void Start()
    {
        //test 1
        //textLabel.text = "Hello!\nThis is my second line.";

        //test 2
        GetComponent<TypewriterEffect>().Run("Hello again!\nThis is my second line.", textLabel);
    }
}
