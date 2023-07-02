using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private Transform transform;

    private void Start()
    {
        TryGetComponent<Transform>(out transform);
    }

    private void FixedUpdate()
    {
        transform.Rotate(0f, 0.1f, 0f);
    }
}
