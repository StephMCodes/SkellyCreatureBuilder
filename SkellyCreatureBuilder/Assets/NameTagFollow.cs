using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameTagFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    void Update()
    {
        if (target == null) return;

        transform.position = target.position + offset;
    }
}
