using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//the point is to update the section where you can add events to responses
//because if you go add a new response in dialogue object, the response handler will not notice
//and it needs to refresh/show new response added

[CustomEditor(typeof(DialogueResponseEvents))]
public class DialogueResponseEventsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        DialogueResponseEvents responseEvents = (DialogueResponseEvents)target; //typecast
        if(GUILayout.Button("Refresh"))
        {
            responseEvents.OnValidate();
        }
    }
}
