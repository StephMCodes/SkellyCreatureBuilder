using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class ResponseHandler : MonoBehaviour
{
    //this must be a component of the dialogue canvas
    
    [SerializeField] private RectTransform responseBox;
    [SerializeField] private RectTransform responseButtonTemplate;
    [SerializeField] private RectTransform responseContainer;

    //reference to dialogue ui script and response events script
    private DialogueUI dialogueUI;
    private ResponseHandler responseHandler;
    private ResponseEvent[] responseEvents;

    //list to track created buttons (for their later removal)
    private List<GameObject> tempResponseButtons = new List<GameObject>();
    private void Start()
    {
        dialogueUI = GetComponent<DialogueUI>();
        responseHandler = GetComponent<ResponseHandler>();
        if (responseHandler == null)
        {
            Debug.Log("ResponseHandler is null when trying to show responses!");
        }
    }
    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        this.responseEvents = responseEvents;
    }

    //driver method
    public void ShowResponses(Response[] responses)
    {
        float responseBoxHeight = 0;
        for (int i=0; i<responses.Length; i++)
        {
            Response response = responses[i];
            int responseIndex = i;
            //clone
            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
            responseButton.gameObject.SetActive(true);
            responseButton.GetComponent<TMP_Text>().text = response.ResponseText;
            //addlistener attaches a delegate/function to the onclick
            //this programatically adds the function to the button!
            responseButton.GetComponent<Button>().onClick.AddListener(() => OnPickedResponse(response, responseIndex));

            //add to temp list
            tempResponseButtons.Add(responseButton);

            //keep track of how high to make box for every option available
            responseBoxHeight += responseButtonTemplate.sizeDelta.y;
        }

        //update box size. the y is the value we found earlier
        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, responseBoxHeight);

        //enable game object
        responseBox.gameObject.SetActive(true);
    }

    private void OnPickedResponse (Response response, int responseIndex)
    {
        //hide response choice box
        responseBox.gameObject.SetActive(false);

        //clear out the buttons created previously
        foreach (GameObject button in tempResponseButtons)
        {
            Destroy(button);
        }

        //clear list
        tempResponseButtons.Clear();

        //show response
        dialogueUI.ShowDialogue(response.DialogueObject);
        
        //check if index is in bounds within response array
        if (responseEvents != null && responseIndex <= responseEvents.Length)
        {
            //check if there is a response event at the index if yes invoke the event
            responseEvents[responseIndex].OnPickedResponse?.Invoke();
        }

        //fixing a bug
        responseEvents = null;

        if (response.DialogueObject)
        {
            dialogueUI.ShowDialogue (response.DialogueObject);
        }
        else
        {
            dialogueUI.CloseDialogueBox();
        }
    }

}
