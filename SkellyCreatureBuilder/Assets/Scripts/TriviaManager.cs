using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriviaManager : MonoBehaviour
{
    private char[] correctAnswers = { 'B', 'B', 'D', 'A', 'B' };
    private int currentQuestionIndex = 0;
    private int score = 0;
    private int mistakesAllowed;
    private int currentMistakes = 0;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

    [Header("Skull UI")]
    [SerializeField] private SkullHealthSystem skullHealthUI;
    [SerializeField] private GameObject skullHealthUIPanel;

    private void Start()
    {
        // Allow as many mistakes as skulls
        mistakesAllowed = Mathf.FloorToInt(BoneDetector.mental);
        Debug.Log("Mistakes allowed based on skulls: " + mistakesAllowed);

        if (skullHealthUIPanel != null)
            skullHealthUIPanel.SetActive(true); 

        if (skullHealthUI != null && skullHealthUI.gameObject.activeInHierarchy)
            skullHealthUI.UpdateSkullUI(mistakesAllowed);
    }

    public void AnswerClicked(string choice)
    {
        if (currentQuestionIndex >= correctAnswers.Length) return;

        string cleanInput = choice.Trim().ToUpper();
        if (string.IsNullOrEmpty(cleanInput) || cleanInput.Length != 1)
        {
            Debug.LogWarning($"Invalid input: \"{choice}\"");
            return;
        }

        char selected = cleanInput[0];
        char correct = correctAnswers[currentQuestionIndex];

        Debug.Log($"Q{currentQuestionIndex + 1}: Selected = {selected}, Correct = {correct}");

        if (selected == correct)
        {
            score++;
            feedbackText.text = "Correct!";
        }
        else
        {
            currentMistakes++;
            BoneDetector.mental = Mathf.Max(0, BoneDetector.mental - 1);

            feedbackText.text = $"Wrong! ({currentMistakes} mistake(s))";
            Debug.Log($"Mistakes: {currentMistakes} / {mistakesAllowed}");

            if (skullHealthUI != null)
                skullHealthUI.UpdateSkullUI((int)BoneDetector.mental);

            if (currentMistakes > mistakesAllowed)
            {
                ShowResult();
                return;
            }
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

        if (skullHealthUIPanel != null)
            skullHealthUIPanel.SetActive(false);

        if (score == correctAnswers.Length)
        {
            winPanel.SetActive(true);
        }
        else
        {
            losePanel.SetActive(true);
        }

        Debug.Log($"Final Score: {score} / {correctAnswers.Length}");
    }
}
