using System;
using System.Linq;
using System.Text;
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
    public TextMeshProUGUI  highscorerankingText;
    public GameObject playButton;
    public GameObject quitButton;
    public GameObject exitPostGameScreenButton;
    public GameObject postGameScreen;

    private void Start()
    {
        inGameScoreText.enabled = false;
        timerText.enabled = false;
        newScoreText.enabled = false;
        onNewRecordScoreText.enabled = false;
        postGameScreen.SetActive(false);
        UpdateRanking();
        
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
        highscorerankingText.enabled = false;
    }

    public void OnStopGame()
    {
        inGameScoreText.enabled = false;
        timerText.enabled = false;
        postGameScreen.SetActive(true);
        exitPostGameScreenButton.SetActive(true);
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
        exitPostGameScreenButton.SetActive(false);
        UpdateRanking();
        highscorerankingText.enabled = true;
    }

    private void UpdateRanking()
    {
        var ranking = GameManager.Instance.ScoreRankingSet;

        StringBuilder strBuilder = new("BEST SCORE:\n");

        var rankingList = ranking.ToList();
        for (int i = 0; i < rankingList.Count; i++)
        {
            strBuilder.Append($"{i+1}. {rankingList[rankingList.Count - i - 1]}\n");
        }

        var output = strBuilder.ToString();

        highscorerankingText.text = output;
    }
}