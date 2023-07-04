using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float rotationAngle;

    private void FixedUpdate()
    {
        transform.Rotate(0f, rotationAngle/10f, 0f);
    }
}
