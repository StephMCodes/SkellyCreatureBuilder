using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneDragger : MonoBehaviour
{
    private Camera cam;
    private bool isDragging = false;
    private DragWobble wobbleScript;

    void Start()
    {
        cam = Camera.main;
        wobbleScript = GetComponent<DragWobble>();
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 5f; // adjust depth
            transform.position = cam.ScreenToWorldPoint(mousePos);
        }
    }

    void OnMouseDown()
    {
        isDragging = true;
        wobbleScript.StartDragging();
    }

    void OnMouseUp()
    {
        isDragging = false;
        wobbleScript.StopDragging();
    }
}
