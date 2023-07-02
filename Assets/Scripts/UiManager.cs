using System;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public TextMeshProUGUI  scoreText;
    public TextMeshProUGUI  timerText;

    private void Start()
    {
        scoreText.enabled = false;
        timerText.enabled = false;
    }

    public void OnStartGame()
    {
        scoreText.enabled = true;
        timerText.enabled = true;
    }
}