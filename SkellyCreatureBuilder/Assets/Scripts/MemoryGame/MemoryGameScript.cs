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
            Debug.Log("light order: " +  lightOrder[i]);
        }

        for (int i = 0; i < rowLights.Length; i++)
        {
            //set all lights back to neutral colour
            rowLights[i].GetComponent<Image>().color = white;
        }

        //start the game
        level = 1;
        //StartCoroutine(ColorOrder());

    }

    void DisableInteractableButtons()
    {
        //while the player watches the lights they cant click anything hence it being a memory game
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Button>().interactable = true;
        }
    }
    void EnableInteractableButtons()
    {
        //while the player watches the lights they cant click anything hence it being a memory game
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Button>().interactable = false;
        }
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
                lightArray[lightOrder[i]].GetComponent<Image>().color = invisible;
                yield return new WaitForSeconds(lightSpeed);
                //makes it green
                lightArray[lightOrder[i]].GetComponent<Image>().color = green;
                yield return new WaitForSeconds(lightSpeed);
                //invis again
                lightArray[lightOrder[i]].GetComponent<Image>().color = invisible;
                //update row on left
                rowLights[i].GetComponent<Image>().color = green;

            }
        }
        //now the player can click after we saw the lights flash
        EnableInteractableButtons();
    }
}
