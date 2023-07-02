using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [SerializeField] private List<Mole> moles;
    [SerializeField] private TextMeshProUGUI  scoreText;
    

    [CanBeNull] public Mole SelectedMole { get; set; }

    private int _score;

    public int MolesWhacked
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
            scoreText.text = $"Score: {_score.ToString()}";
    }
    }
    
    private float _time;
    private const float StartTime = 30f;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        MolesWhacked = 0;
    }

    private void Update()
    {
        ReadInput();
    }

    private void ReadInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (SelectedMole is null)
            {
                if (MolesWhacked > 0) MolesWhacked--;
                return;
            }
            if (SelectedMole.IsStunned) return; 
            MolesWhacked++;
            StartCoroutine(SelectedMole.Stun());
        }
    }
}