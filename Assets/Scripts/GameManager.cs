using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [SerializeField] private UiManager  uiManager;
    
    [CanBeNull] public Mole SelectedMole { get; set; }
    public bool GameRunning { get; set; }
    public List<int> HighscoreList { get; set; } = new List<int>(5);
    
    public Action OnGameStart;
    public Action OnGameStop;
    public Action OnExitPostGameScreen;

    private int _score;
    public int MolesWhacked
    {
        get
        {
            if (_score < 0) _score = 0;
            return _score;
        }
        set
        {
            _score = value;
            if (_score < 0) _score = 0;
            uiManager.inGameScoreText.text = $"Score: {_score}";
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
    
    private const float StartTime = 10f;
    private readonly string _highscoreKey = "highscore";

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        HighscoreList = SaveLoadHighscoreList.Load(_highscoreKey);
        if(!HighscoreList.Any()) SeedHighscoreData.Initialize(HighscoreList);
        MolesWhacked = 0;
        GameRunning = false;
    }

    private void Update()
    {
        if(!GameRunning) return;

        if (CurrentTime > 0) CurrentTime -= Time.deltaTime; 
        else StopGame();
        
        if (MolesWhacked < 0) MolesWhacked = 0;
        ReadInput();
    }

    private void ReadInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (SelectedMole is null)
            {
                MolesWhacked--;
                return;
            }
            if (SelectedMole is not null && SelectedMole.IsStunned) return;
            
            MolesWhacked++;
            StartCoroutine(SelectedMole.Stun());
        }
    }

    private void TryAddNewHighscore(int score)
    {
        if (HighscoreList.Count < 5)
        {
            HighscoreList.Add(score);
            HighscoreList.Sort();
            return;
        }
        
        var min = HighscoreList.Min();
        if (score <= min) return;

        HighscoreList.Remove(min);
        HighscoreList.Add(score);
        HighscoreList.Sort();
    }

    public int GetMaxScore()
        => HighscoreList.Max();

    public void StartGame()
    {
        OnGameStart?.Invoke();
        GameRunning = true;
        CurrentTime = StartTime + 1f;
        MolesWhacked = 0;
    }

    public void StopGame()
    {
        OnGameStop?.Invoke();
        GameRunning = false;
        TryAddNewHighscore(MolesWhacked);
    }

    public void ExitPostGameScreen()
    {
        OnExitPostGameScreen?.Invoke();
    }

    public void QuitGame()
    {
        SaveLoadHighscoreList.Save(_highscoreKey, HighscoreList);
        Application.Quit();
    }
}