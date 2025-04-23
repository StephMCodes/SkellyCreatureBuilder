using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ribcage : MonoBehaviour
{

    public Transform[] attachmentPoints; // attach EMPTY game objects here for sockets
    private bool[] socketUsed;
    private LinkedList<Transform> socketList = new LinkedList<Transform>();
    public Button waveButton;



    void Start()
    {
        foreach (var socket in attachmentPoints)
        {
            socketList.AddLast(socket);
        }

        if (waveButton != null)
        {
            waveButton.onClick.AddListener(OnWaveButtonClicked);
        }
    }

    private void OnWaveButtonClicked()
    {

        Transform rightArmSocket = GameObject.FindGameObjectWithTag("RightArmSocket")?.transform;

        if (rightArmSocket != null)
        {
            StartCoroutine(WiggleOneSocket(rightArmSocket));
        }
        else
        {
            Debug.Log("RightArmSocket not found.");
        }
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


    Transform GetRandomSocket()
    {
        if (socketList.Count == 0) return null;

        int index = Random.Range(0, socketList.Count);
        var current = socketList.First;

        for (int i = 0; i < index; i++)
            current = current.Next;

        return current.Value;
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
        float duration = 4f; // total time to wiggle

        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration; // goes from 0 to 1 over time

            // slowly reduce intensity over time
            float currentBoneIntensity = Mathf.Lerp(boneWiggleIntensity, 0f, t);
            float currentRibcageIntensity = Mathf.Lerp(ribcageWiggleIntensity, 0f, t);

            float ribcageRotation = Mathf.Sin(Time.time * wiggleSpeed) * currentRibcageIntensity;
            transform.localRotation = Quaternion.Euler(0f, 0f, ribcageRotation);

            foreach (Transform socket in attachmentPoints)
            {
                if (socket != null)
                {
                    float randomRotation = Mathf.Sin(Time.time * wiggleSpeed) * currentBoneIntensity;
                    socket.localRotation = Quaternion.Euler(0f, 0f, randomRotation);
                }
            }

            yield return null;
        }

        // when done, reset rotation
        transform.localRotation = Quaternion.identity;
        foreach (Transform socket in attachmentPoints)
        {
            if (socket != null)
                socket.localRotation = Quaternion.identity;
        }
    }


    IEnumerator WiggleOneSocket(Transform socket)
    {
        float wiggleSpeed = 12f;
        float wiggleIntensity = 50f;
        float duration = 2f;
        float timer = 0f;

        while (timer < duration)
        {
            float angle = Mathf.Sin(Time.time * wiggleSpeed) * wiggleIntensity;
            socket.localRotation = Quaternion.Euler(0f, 0f, angle);

            timer += Time.deltaTime;
            yield return null;
        }

        socket.localRotation = Quaternion.identity;
    }
}

