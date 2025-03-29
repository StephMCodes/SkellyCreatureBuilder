using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    //this needs to be a component of the dialogue canvas
    //string is what we want to type, label is what we type it on

    //each punctuation can have a different wait time
    private readonly Dictionary<HashSet<char>, float> punctuations = new Dictionary<HashSet<char>, float>()
    {
        //key = punctuation
        //value = wait time
        {new HashSet<char>(){'.','!','?'},0.6f},
        {new HashSet<char>(){',',';',':'},0.3f}
    };

    [SerializeField] private float typewriterSpeed = 50f;
    public Coroutine Run(string textToType, TMP_Text textLabel)
    {
        //call ienumerator method
        return StartCoroutine(TypeText(textToType, textLabel));
    }

    private IEnumerator TypeText(string textToType, TMP_Text textLabel)
    {
        //clean label
        textLabel.text = string.Empty;

        float t = 0; //elasped time since writing
        int charIndex = 0; //how many chars to type in a given frame
        while (charIndex < textToType.Length)
        {
            t += Time.deltaTime * typewriterSpeed; //tracks seconds
            charIndex = Mathf.FloorToInt(t); //keeps score of seconds in interger form

            //make sure char index does not go beyond text to type
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);

            //write text
            textLabel.text = textToType.Substring(0, charIndex);
            yield return null; //wait one frame
        }

        textLabel.text = textToType;
    }

    private bool IsPunctuation(char ch, out float waitTime)
    {
        foreach (KeyValuePair<HashSet<char>, float> punctuationCategory in punctuations)
        {
            if (punctuationCategory.Key.Contains(ch))
            {
                waitTime = punctuationCategory.Value;
                return true;
            }
        }
        waitTime = default;
        return false;
    }
}
