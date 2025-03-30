using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ribcage : MonoBehaviour
{
    public Transform[] attachmentPoints; // attach EMPTY game objects here
    private bool[] socketUsed;

    void Start()
    {
        socketUsed = new bool[attachmentPoints.Length];
    }

    public Transform GetClosestSocket(Vector3 position)
    {
        Transform closestSocket = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform socket in attachmentPoints)
        {
            float distance = Vector3.Distance(position, socket.position);
            if (distance < closestDistance)
            {
                closestSocket = socket;
                closestDistance = distance;
            }
        }

        return closestSocket;
    }


}
