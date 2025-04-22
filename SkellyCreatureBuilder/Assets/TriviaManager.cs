using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriviaManager : MonoBehaviour
{
    private char[] correctAnswers = { 'B', 'B', 'A', 'D', 'A' };
    private int currentQuestionIndex = 0;
    private int score = 0;

    [SerializeField] private TextMeshProUGUI feedbackText;

    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

    public void AnswerClicked(string choice)
    {
        Debug.Log($"Raw input: \"{choice}\"");

        if (currentQuestionIndex >= correctAnswers.Length) return;

        string cleanInput = choice.Trim().ToUpper();
        if (string.IsNullOrEmpty(cleanInput) || cleanInput.Length != 1)
        {
            Debug.LogWarning($"Invalid input: \"{choice}\"");
            return;
        }

        char selected = cleanInput[0];
        char correct = correctAnswers[currentQuestionIndex];

        Debug.Log($"Question {currentQuestionIndex + 1}: Selected = {selected}, Correct = {correct}");

        if (selected == correct)
        {
            score++;
            feedbackText.text = "Correct!";
        }
        else
        {
            feedbackText.text = "Wrong!";
        }

        currentQuestionIndex++;

        if (currentQuestionIndex >= correctAnswers.Length)
        {
            ShowResult();
        }
    }

    private void ShowResult()
    {
        feedbackText.text = "";

        if (score == correctAnswers.Length)
        {
            winPanel.SetActive(true);
        }
        else
        {
            losePanel.SetActive(true);
        }
    }
}
