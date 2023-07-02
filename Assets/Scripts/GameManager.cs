using System.Collections.Generic;
using DefaultNamespace;
using JetBrains.Annotations;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [SerializeField] private List<Mole> moles;

    [CanBeNull] Mole SelectedMole { get; set; }
    
    private float _time;
    private const float StartTime = 30f;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Update()
    {
        ReadInput();
    }

    private void ReadInput()
    {
        if (SelectedMole is null) return;
        if (SelectedMole.IsStunned) return; 
        
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(SelectedMole.Stun());
        }
    }
}