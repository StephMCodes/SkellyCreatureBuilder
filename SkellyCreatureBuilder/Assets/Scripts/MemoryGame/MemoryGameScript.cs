using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryGameScript : MonoBehaviour
{
    [SerializeField] GameObject simonSaysPanel; //access to panel
    [SerializeField] GameObject[] buttons; //button obj (right)
    [SerializeField] GameObject[] lightArray; //light objects (left)
    [SerializeField] GameObject[] rowLights; //the top row tracking success
    [SerializeField] int[] lightOrder; //for the light pattern position

    [SerializeField] TMPro.TMP_Text text;
    [SerializeField] GameObject GameStatePanel;

    //the amount of skulls is the amount youre allowed to lose
    public static int skulls = 2;

    //sfx
    private AudioSource audioSource;
    [SerializeField] AudioClip witchLaugh;
    [SerializeField] AudioClip boneBreak;
    [SerializeField] AudioClip winSfx;
    [SerializeField] AudioClip flip;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    int level = 0;
    int buttonsClicked = 0;
    int colorOrderRunCount = 0;
    bool passed = false;
    bool won = false;

    Color32 red = new Color32(255, 39, 0, 255);
    Color32 green = new Color32(4, 204, 0, 255);
    Color32 invisible = new Color32(4, 204, 0, 0);
    Color32 white = new Color32(255, 255, 255, 255);

    public float lightSpeed;

    private void OnEnable()
    {
        //called when panel is enabled or called in script

        //reset game
        level = 0;
        buttonsClicked = 0;
        colorOrderRunCount = -1;
        won = false;

        //get random order of lights
        for (int i = 0; i < lightOrder.Length; i++)
        {
            lightOrder[i] = Random.Range(0, 8);
            Debug.Log("light order: " + lightOrder[i]);
        }

        for (int i = 0; i < rowLights.Length; i++)
        {
            //set all lights back to neutral colour
            rowLights[i].GetComponent<SpriteRenderer>().color = white;
        }

        //start the game
        level = 1;
        StartCoroutine(ColorOrder());

    }

    void DisableInteractableButtons()
    {
        //while the player watches the lights they cant click anything hence it being a memory game
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Button>().interactable = false;
        }
    }
    void EnableInteractableButtons()
    {
        //while the player watches the lights they cant click anything hence it being a memory game
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Button>().interactable = true;
        }
    }

    public void OpenPanel()
    {
        simonSaysPanel.SetActive(true);
    }

    public void ClosePanel()
    {
        simonSaysPanel.SetActive(false);
    }
    IEnumerator ColorOrder()
    {
        buttonsClicked = 0;
        colorOrderRunCount++;
        DisableInteractableButtons();
        for (int i = 0; i <= colorOrderRunCount; i++)
        {
            if (level >= colorOrderRunCount)
            {
                Debug.Log(lightOrder[i]);
                //grabs light order
                //makes it invisible for a wait period between lights
                lightArray[lightOrder[i]].GetComponent<SpriteRenderer>().color = invisible;
                yield return new WaitForSeconds(lightSpeed);
                //makes it green
                lightArray[lightOrder[i]].GetComponent<SpriteRenderer>().color = green;
                yield return new WaitForSeconds(lightSpeed);
                //invis again
                lightArray[lightOrder[i]].GetComponent<SpriteRenderer>().color = invisible;
                //update row on left
                rowLights[i].GetComponent<SpriteRenderer>().color = green;

            }
        }
        //now the player can click after we saw the lights flash
        EnableInteractableButtons();
    }

    public void ButtonClickOrder(int button)
    {
        //sfx
        audioSource.PlayOneShot(flip);
        
        //checks to see if youre clicking the right buttons in the right order
        buttonsClicked++;
        if (button == lightOrder[buttonsClicked - 1])
        {
            Debug.Log("pass");
            passed = true;
        }
        else
        {
            //take a skull away
            skulls--;
            Debug.Log("failed. skull count: " + skulls);
            audioSource.PlayOneShot(boneBreak);
            won = false;
            passed = false;
            StartCoroutine(ColorBlink(red));
            
        }
        //increment level
        if (buttonsClicked == level && passed == true && buttonsClicked != 5)
        {
            level++;
            passed = false;
            StartCoroutine(ColorOrder());
        }
        //check if level is complete
        if (buttonsClicked == level && passed == true && buttonsClicked == 5)
        {
            Debug.Log("beat the game");
            won = true;
            StartCoroutine(ColorBlink(green));
        }
    }

    IEnumerator ColorBlink(Color32 colorToBlink)
    {
        DisableInteractableButtons();
        for (int j = 0; j < 3; j++)
        {
            Debug.Log("I run this many times: " + j);
            for (int i = 0; i < buttons.Length; i++)
            {       
                buttons[i].GetComponent<Image>().color = colorToBlink;
            }
            for (int i = 0; i < rowLights.Length; i++)
            {
                rowLights[i].GetComponent<SpriteRenderer>().color = colorToBlink;
            }
            yield return new WaitForSeconds(.3f);

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].GetComponent<Image>().color = white;
            }
            for (int i = 0; i < rowLights.Length; i++)
            {
                rowLights[i].GetComponent<SpriteRenderer>().color = white;
            }
            yield return new WaitForSeconds(.3f);
        }

        if (won)
        {
            //Debug.Log("Game has been won");
            ClosePanel();
            audioSource.PlayOneShot(winSfx);
            text.SetText("GAME WON");
            GameStatePanel.SetActive(true);
        }

        if (skulls == 0)
        {
            //Debug.Log("GAME OVER");
            audioSource.PlayOneShot(witchLaugh);
            //ClosePanel();
            text.SetText("GAME OVER");
            GameStatePanel.SetActive(true);

        }
        EnableInteractableButtons();
        OnEnable();
    }
}
