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

        if (!Physics.Raycast(ray, out var hit, Mathf.Infinity, targetLayer))
        {
            GameManager.Instance.SelectedMole = null;
            return;
        }
        
        GameManager.Instance.SelectedMole = hit.collider.CompareTag("Mole") ? hit.collider.GetComponent<Mole>() : null;
    }
}
