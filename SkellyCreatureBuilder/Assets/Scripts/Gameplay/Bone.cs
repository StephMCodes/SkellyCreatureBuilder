using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Camera mainCamera;
    private float objectZDistance;
    [SerializeField] private Transform snapPoint;
    private AudioSource AudioPlayer;
    [SerializeField] AudioClip BoneClip;


    void Start()
    {
        mainCamera = Camera.main;
        //Fetch the AudioSource from the GameObject
        AudioPlayer = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isDragging)
        {
            // bone pos will follow cursor
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = objectZDistance; // used for keeping the position, might change the way this works later
            transform.position = mainCamera.ScreenToWorldPoint(mousePosition) + offset;
        }
    }

    void OnMouseDown()
    {
        isDragging = true;

        // so the object doesnt teleport on click
        objectZDistance = mainCamera.WorldToScreenPoint(transform.position).z;

        offset = transform.position - mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, objectZDistance));
    }

void OnMouseUp()
{
    isDragging = false;

    // Check for nearby ribcage colliders
    Collider[] colliders = Physics.OverlapSphere(transform.position, 0.5f); // 0.5 is the radius around the bone, tweak if needed
    foreach (Collider collider in colliders)
    {
        Ribcage ribcage = collider.GetComponent<Ribcage>();
        if (ribcage != null)
        {
            // Found a ribcage, attach to closest socket
            Transform socket = ribcage.GetClosestSocket(transform.position);
            if (socket != null)
            {
                AttachTo(socket);
                return; // We're done, no need to check other colliders
            }
        }
    }

    Debug.Log("No ribcage nearby to attach.");
}


    void AttachTo(Transform socket)
    {
        transform.SetParent(null);

        // First match rotation
        transform.rotation = socket.rotation;

        // THEN move bone so that snapPoint lands exactly on socket
        Vector3 worldOffset = socket.position - snapPoint.position;
        transform.position += worldOffset;

        // Re-parent
        transform.SetParent(socket);

        //play sfx
        AudioPlayer.PlayOneShot(BoneClip);

        // Disable physics
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = false;
    }



}
