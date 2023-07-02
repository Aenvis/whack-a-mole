using System;
using JetBrains.Annotations;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [SerializeField] private UiManager  uiManager;
    
    [CanBeNull] public Mole SelectedMole { get; set; }
    public bool GameRunning { get; set; }
    public Action OnGameStart;
    public Action OnGameStop;

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
            uiManager.scoreText.text = $"Score: {_score.ToString()}";
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
        GameRunning = false;
    }

    private void Update()
    {
        if(!GameRunning) return;

        if (_time > 0) _time -= Time.deltaTime; 
        else StopGame();
        
        if (MolesWhacked <= 0) MolesWhacked = 0;
        ReadInput();
    }

    private void ReadInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (SelectedMole is null)
            {
                if (MolesWhacked > 0)
                {
                    MolesWhacked--;
                }
                return;
            }
            if (SelectedMole.IsStunned) return; 
            MolesWhacked++;
            StartCoroutine(SelectedMole.Stun());
        }
    }

    public void StartGame()
    {
        OnGameStart?.Invoke();
        GameRunning = true;
        _time = StartTime;
    }

    public void StopGame()
    {
        OnGameStop?.Invoke();
    }
}