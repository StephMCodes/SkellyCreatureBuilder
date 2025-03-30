using UnityEngine.Events;
using UnityEngine;

[System.Serializable]
public class ResponseEvent
{
    [HideInInspector] public string name;
    [SerializeField] private UnityEvent onPickedResponse;

    //get of event
    public UnityEvent OnPickedResponse => onPickedResponse;
}
