using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class ResponseHandler : MonoBehaviour
{
    //this must be a component of the dialogue canvas
    
    [SerializeField] private RectTransform responseBox;
    [SerializeField] private RectTransform responseButtonTemplate;
    [SerializeField] private RectTransform responseContainer;

    //driver method
    public void ShowResponses(Response[] responses)
    {
        float responseBoxHeight = 0;
        foreach (Response response in responses)
        {
            //clone from prefab (?)
            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
            responseButton.gameObject.SetActive(true);
            responseButton.GetComponent<TMP_Text>().text = response.ResponseText;
            //addlistener attaches a delegate/function to the onclick
            //this programatically adds the function to the button!
            responseButton.GetComponent<Button>().onClick.AddListener(() => OnPickedResponse(response));

            //keep track of how high to make box for every option available
            responseBoxHeight += responseButtonTemplate.sizeDelta.y;
        }

        //update box size. the y is the value we found earlier
        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, responseBoxHeight);

        //enable game object
        responseBox.gameObject.SetActive(true);
    }

    private void OnPickedResponse (Response response)
    {

    }

}
