using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrueFalseMemManager : MonoBehaviour
{
    private bool[] correctAnswers = { false, false, false, true, true };
    private int currentQuestion = 0;
    private int score = 0;
    private int allowedMistakes;
    private int currentMistakes = 0;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

    private void Start()
    {
        allowedMistakes = Mathf.FloorToInt(BoneDetector.mental);
        currentMistakes = 0;

        Debug.Log($"Skull Bonus (Mental): {BoneDetector.mental} → Allowed Mistakes: {allowedMistakes}");

        // Show a warning if no mistakes are allowed
        if (allowedMistakes == 0)
            feedbackText.text = "You can't make a single mistake!";
    }

    public void OnPlayerAnswer(bool playerAnswer)
    {
        if (currentQuestion >= correctAnswers.Length) return;

        bool correct = correctAnswers[currentQuestion];

        if (playerAnswer == correct)
        {
            score++;
            feedbackText.text = "Correct!";
        }
        else
        {
            currentMistakes++;
            feedbackText.text = $"Wrong! Mistakes: {currentMistakes}/{allowedMistakes}";

            if (currentMistakes > allowedMistakes)
            {
                EndGame(false);
                return;
            }
        }

        currentQuestion++;

        if (currentQuestion >= correctAnswers.Length)
        {
            EndGame(score == correctAnswers.Length);
        }
    }

    private void EndGame(bool won)
    {
        feedbackText.text = "";

        if (won)
            winPanel.SetActive(true);
        else
            losePanel.SetActive(true);
    }
}
