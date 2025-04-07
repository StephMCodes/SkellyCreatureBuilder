using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Camera mainCamera;
    private float objectZDistance;

    void Start()
    {
        mainCamera = Camera.main;
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

        // raycast to detect ribcage
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log("Hit: " + hit.collider.gameObject.name);

            Ribcage ribcage = hit.collider.GetComponent<Ribcage>();
            if (ribcage != null)
            {
                // closest socket in ribcage script
                Transform socket = ribcage.GetClosestSocket(transform.position);
                if (socket != null)
                {
                    AttachTo(socket);
                }
            }
        }
    }

    void AttachTo(Transform socket)
    {
        // Snap position and rotation
        transform.position = socket.position;
        transform.rotation = socket.rotation;

        // Parent it to the socket
        transform.SetParent(socket);

        // Disable physics
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;  // Stops physics simulation
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // Optional: Disable collider if needed to prevent interaction
        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = false;
    }

}
