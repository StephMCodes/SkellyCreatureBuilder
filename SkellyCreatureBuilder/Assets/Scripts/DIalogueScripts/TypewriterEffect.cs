using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    //this needs to be a component of the dialogue canvas
    //string is what we want to type, label is what we type it on

    //will let us skip dialogue later
    public bool IsRunning { get; private set; }

    //each punctuation can have a different wait time
    private readonly List<Punctuation> punctuations = new List<Punctuation>()
    {
        new Punctuation(new HashSet<char>(){'.','!','?'},0.4f),
        new Punctuation(new HashSet<char>(){',',';',':'},0.2f)

        
        //key = punctuation
        //value = wait time
        //{new HashSet<char>(){'.','!','?'},0.6f},
        //{new HashSet<char>(){',',';',':'},0.3f}
    };

    [SerializeField] private float typewriterSpeed = 50f;

    private Coroutine typingCoroutine;
    public void Run(string textToType, TMP_Text textLabel)
    {
        //call ienumerator method
        typingCoroutine = StartCoroutine(TypeText(textToType, textLabel));
    }

    public void Stop()
    {
        StopCoroutine(typingCoroutine);
        IsRunning = false;
    }

    private IEnumerator TypeText(string textToType, TMP_Text textLabel)
    {
        IsRunning = true;

        //clean label
        textLabel.text = string.Empty;

        float t = 0; //elasped time since writing
        int charIndex = 0; //how many chars to type in a given frame
        while (charIndex < textToType.Length)
        {
            //to detect punctuations
            int lastCharIndex = charIndex;

            t += Time.deltaTime * typewriterSpeed; //tracks seconds
            charIndex = Mathf.FloorToInt(t); //keeps score of seconds in interger form

            //make sure char index does not go beyond text to type
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);

            for (int i = lastCharIndex; i < charIndex; i++)
            {
                //looping characters that have been typed
                //keeps frame weight consistency
                bool isLast = i >= textToType.Length - 1; //check if final ch

                //write text
                textLabel.text = textToType.Substring(0, i + 1);

                //are we at a punctuation, if yes get the wait time
                //make sure we are not pausing at the end of the sentence since its over
                //check if next character is not punctuation to not have very long ?!! or ...
                //if everything is okay, wait
                if (IsPunctuation(textToType[i], out float waitTime) && !isLast && !IsPunctuation(textToType[i + 1], out _))
                {
                    yield return new WaitForSeconds(waitTime);
                }

            }


            yield return null; //wait one frame
        }
        IsRunning = false;

        //will set the following line from the outside
        //textLabel.text = textToType;
    }

    private bool IsPunctuation(char ch, out float waitTime)
    {
        foreach (Punctuation punctuationCategory in punctuations)
        {
            if (punctuationCategory.Punctuations.Contains(ch))
            {
                waitTime = punctuationCategory.WaitTime; //read from the readonly struct
                return true;
            }
        }
        waitTime = default;
        return false;
    }

    private readonly struct Punctuation
    {
        public readonly HashSet<char> Punctuations;
        public readonly float WaitTime;

        public Punctuation(HashSet<char> punctuations, float waitTime)
        {
            Punctuations = punctuations;
            WaitTime = waitTime;
        }
    }
}
