using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask targetLayer;
    private Camera _camera;

    private void Start()
    {
        _camera = GetComponentInParent<Camera>();
    }

    private void FixedUpdate()
    {
        Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity,
                targetLayer))
        {
            Debug.DrawRay(mousePosition, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(mousePosition, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }
}
