using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public TextMeshProUGUI  inGameScoreText;
    public TextMeshProUGUI  timerText;
    public TextMeshProUGUI  newScoreText;
    public TextMeshProUGUI  onNewRecordScoreText;
    public GameObject playButton;
    public GameObject quitButton;
    public GameObject postGameScreen;

    private void Start()
    {
        inGameScoreText.enabled = false;
        timerText.enabled = false;
        newScoreText.enabled = false;
        onNewRecordScoreText.enabled = false;
        postGameScreen.SetActive(false);

        GameManager.Instance.OnGameStart += OnStartGame;
        GameManager.Instance.OnGameStop += OnStopGame;
        GameManager.Instance.OnExitPostGameScreen += OnExitPostGameScreen;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStart -= OnStartGame;
        GameManager.Instance.OnGameStop -= OnStopGame;
        GameManager.Instance.OnExitPostGameScreen -= OnExitPostGameScreen;
    }

    public void OnStartGame()
    {
        inGameScoreText.enabled = true;
        timerText.enabled = true;
        playButton.SetActive(false);
        quitButton.SetActive(false);
    }

    public void OnStopGame()
    {
        inGameScoreText.enabled = false;
        timerText.enabled = false;
        postGameScreen.SetActive(true);
        newScoreText.enabled = true;
        newScoreText.text = $"YOUR SCORE: {GameManager.Instance.MolesWhacked}";
        if (GameManager.Instance.MolesWhacked > GameManager.Instance.GetMaxScore())
        {
            onNewRecordScoreText.enabled = true;
            onNewRecordScoreText.text = "Congratulations! NEW RECORD!";
        }
    }

    public void OnExitPostGameScreen()
    {
        newScoreText.enabled = false;
        onNewRecordScoreText.enabled = false;
        postGameScreen.SetActive(false);
        playButton.SetActive(true);
        quitButton.SetActive(true);
    }
}