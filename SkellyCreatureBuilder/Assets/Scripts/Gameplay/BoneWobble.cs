using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragWobble : MonoBehaviour
{
    public float wobbleSpeed = 5f;
    public float wobbleAmount = 15f;
    private Vector3 originalRotation;
    private bool isBeingDragged = false;

    void Start()
    {
        originalRotation = transform.localEulerAngles;
    }

    void Update()
    {
        if (isBeingDragged)
        {
            float wobbleZ = Mathf.Cos(Time.time * wobbleSpeed) * wobbleAmount;
            transform.localEulerAngles = originalRotation + new Vector3(0, 0, wobbleZ);
        }
    }

    public void StartDragging()
    {
        isBeingDragged = true;
    }

    public void StopDragging()
    {
        isBeingDragged = false;
        transform.localEulerAngles = originalRotation; // reset rotation when dropped
    }
}
