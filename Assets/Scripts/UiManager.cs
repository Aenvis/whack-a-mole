using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public TextMeshProUGUI  scoreText;
    public TextMeshProUGUI  timerText;
    public GameObject playButton;

    private void Start()
    {
        scoreText.enabled = false;
        timerText.enabled = false;

        GameManager.Instance.OnGameStart += OnStartGame;
        GameManager.Instance.OnGameStop += OnStopGame;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStart -= OnStartGame;
        GameManager.Instance.OnGameStop -= OnStopGame;
    }

    public void OnStartGame()
    {
        scoreText.enabled = true;
        timerText.enabled = true;
        playButton.SetActive(false);
    }

    public void OnStopGame()
    {
        scoreText.enabled = false;
        timerText.enabled = false;
        playButton.SetActive(true);
    }
}