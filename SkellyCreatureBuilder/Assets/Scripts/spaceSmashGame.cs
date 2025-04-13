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
    public TextMeshProUGUI targetScoreText;

    [Header("Endgame Panels")]
    public GameObject winPanel;
    public GameObject losePanel;

    [Header("Game Settings")]
    public float gameDuration = 20f;

    [Header("Bonus Settings")]
    public bool hasBonus = false;
    public int bonusMultiplier = 2;

    private int score = 0;
    private float timer;
    private bool gameRunning = false;
    private int targetScore;

    void Start()
    {
        score = 0;
        timer = gameDuration;
        gameRunning = true;

        //randomizes the win number
        targetScore = Random.Range(120, 151); 
        UpdateUI();
        UpdateBonusUI();

        if (targetScoreText != null)
            targetScoreText.text = $"Target: {targetScore}";

        if (winPanel != null)
            winPanel.SetActive(false);

        if (losePanel != null)
            losePanel.SetActive(false);
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

        timerText.text = "Time: " + timer.ToString("F2");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            score += hasBonus ? bonusMultiplier : 1;
            UpdateUI();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            hasBonus = !hasBonus;
            UpdateBonusUI();
        }
    }

    void UpdateUI()
    {
        if (scoreText != null)
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

        bool didWin = score >= targetScore;

        if (didWin)
        {
            Debug.Log("You Win!");
            if (winPanel != null) winPanel.SetActive(true);
        }
        else
        {
            Debug.Log("You Lose.");
            if (losePanel != null) losePanel.SetActive(true);
        }
    }
}
