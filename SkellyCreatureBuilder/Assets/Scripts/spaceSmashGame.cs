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
   
    private int score = 0;
    private float timer;
    private bool gameRunning = false;
    private int targetScore;

    // Bone bonus
    private int legBoneBonus = 0;
    private bool bonesDetected = false;

    void Start()
    {
        score = 0;
        timer = gameDuration;
        gameRunning = true;

        //randomize win number
        targetScore = Random.Range(120, 151); 
        UpdateUI();

        if (targetScoreText != null)
            targetScoreText.text = $"Target: {targetScore}";

        if (winPanel != null) winPanel.SetActive(false);
        if (losePanel != null) losePanel.SetActive(false);

        // BoneDetector bonus 
        if (BoneDetector.speed > 0)
        {
            int legBones = Mathf.FloorToInt(BoneDetector.speed);
            legBoneBonus = Mathf.Max(0, legBones - 1); // 1 foot is normal
            bonesDetected = true;

            if (bonusText != null)
                bonusText.text = $"+{legBoneBonus} extra points per press from bones";

            Debug.Log($"Leg bones detected: {legBones} | Bonus per space: +{legBoneBonus}");
        }
        else
        {
            legBoneBonus = 0;
            bonesDetected = false;

            if (bonusText != null)
                bonusText.text = $"No leg bones detected";

            Debug.Log("No leg bones detected — bonus disabled.");
        }
    }

    void Update()
    {
        if (!gameRunning) return;

        // timer
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer = 0f;
            gameRunning = false;
            EndGame();
        }

        timerText.text = "Time: " + timer.ToString("F2");

        // smash space 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            score += 1 + (bonesDetected ? legBoneBonus : 0);
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score.ToString();
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
