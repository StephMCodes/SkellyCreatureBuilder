using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class spaceSmashGame : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI bonusText;
    public GameObject tutorialPanel;

    [Header("Game Settings")]
    public float gameDuration = 20f;

    [Header("Bonus Settings")]
    public bool hasBonus = false;
    public int bonusMultiplier = 2;

    private int score = 0;
    private float timer;
    private bool gameRunning = false;

    void Start()
    {
        timer = gameDuration;
        gameRunning = true;
        score = 0;
        UpdateUI();
        UpdateBonusUI();
    }

    void Update()
    {
        if (!gameRunning) return;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer = 0f;
            gameRunning = false;
            EndGame();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            score += hasBonus ? bonusMultiplier : 1;
            UpdateUI();
        }

        if (Input.GetKeyDown(KeyCode.B)) // Optional toggle for testing
        {
            hasBonus = !hasBonus;
            UpdateBonusUI();
        }

        timerText.text = "Time: " + timer.ToString("F2");
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    void UpdateBonusUI()
    {
        if (bonusText != null)
            bonusText.text = hasBonus ? $"BONUS x{bonusMultiplier} ACTIVE!" : "No Bonus";
    }

    void EndGame()
    {
        timerText.text = "Time's up!";
        Debug.Log("Final Score: " + score);
    }
}
