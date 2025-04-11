using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysWobble : MonoBehaviour
{
    public float wobbleSpeed = 5f;
    public float wobbleAmount = 15f;
    private Vector3 originalRotation;

    void Start()
    {
        originalRotation = transform.localEulerAngles;
    }

    void Update()
    {
        float wobbleZ = Mathf.Cos(Time.time * wobbleSpeed) * wobbleAmount;
        transform.localEulerAngles = originalRotation + new Vector3(0, 0, wobbleZ);
    }
}
