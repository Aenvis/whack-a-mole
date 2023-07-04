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
        
        if (hit.collider.CompareTag("Mole"))
        {
            GameManager.Instance.SelectedMole = hit.collider.GetComponent<Mole>();
            Debug.DrawRay(transform.position, (hit.transform.position - transform.position) * hit.distance,
                Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            GameManager.Instance.SelectedMole = null;
            Debug.DrawRay(transform.position, (hit.transform.position - transform.position) * hit.distance,
                Color.white);
            Debug.Log("Did not Hit");
        }
    }
}
