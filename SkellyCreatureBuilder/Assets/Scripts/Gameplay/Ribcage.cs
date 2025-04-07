using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ribcage : MonoBehaviour
{

    public Transform[] attachmentPoints; // attach EMPTY game objects here for sockets
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


    public void BringToLife()
    {
        StartCoroutine(WiggleBonesCoroutine());
    }

    private IEnumerator WiggleBonesCoroutine()
    {
        float wiggleSpeed = 10f;
        float boneWiggleIntensity = 30f;
        float ribcageWiggleIntensity = 10f;

        while (true)
        {
            // wiggle the ribcage but less intense
            float ribcageRotation = Mathf.Sin(Time.time * wiggleSpeed) * ribcageWiggleIntensity;
            transform.localRotation = Quaternion.Euler(0f, 0f, ribcageRotation);

            // go through all sockets to rotate
            foreach (Transform socket in attachmentPoints)
            {   
                if (socket != null)
                {

                    float randomRotation = Mathf.Sin(Time.time * wiggleSpeed) * boneWiggleIntensity;

                    // rotate the socket, maybe later add general socket rotation for more dynamic looking wiggling ?
                    socket.localRotation = Quaternion.Euler(0f, 0f, randomRotation);
                }
            }

            yield return null; // required for couroutine 
        }
    }
}

