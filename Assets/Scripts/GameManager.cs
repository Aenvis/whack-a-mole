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
    public float CurrentTime
    {
        get => _time;
        set
        {
            _time = value;
            uiManager.timerText.text = $"time left: {(int)_time}";
        }
    }
    
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

        if (CurrentTime > 0) CurrentTime -= Time.deltaTime; 
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
        CurrentTime = StartTime + 1f;
    }

    public void StopGame()
    {
        OnGameStop?.Invoke();
    }
}