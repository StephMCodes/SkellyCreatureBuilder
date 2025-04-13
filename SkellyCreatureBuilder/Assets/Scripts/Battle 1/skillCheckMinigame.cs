using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class skillCheckMinigame : MonoBehaviour
{
    [Header("UI Elements")]
    public RectTransform rotatingZone;
    public TextMeshProUGUI successText;

    [Header("Collider References")]
    public Collider2D rotatingZoneCollider;
    public Collider2D needleCollider;

    [Header("Settings")]
    public float rotationSpeed = 100f;
    public int maxChances = 3;
    public float delayBetweenRounds = 1f;

    [Header("SFX")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip witchLaugh;
    [SerializeField] private AudioClip ding;
    [SerializeField] private AudioClip punch;
    [SerializeField] private AudioClip bonecrack;
    [SerializeField] private AudioClip winSfx;

    [Header("End Panels")]
    public GameObject winPanel;
    public GameObject losePanel;

    private int successCount = 0;
    private int attemptCount = 0;
    private bool gameActive = true;
    private bool inputEnabled = true;

    void Start()
    {
        UpdateSuccessText();
        ResetZoneAngle();

        if (winPanel != null) winPanel.SetActive(false);
        if (losePanel != null) losePanel.SetActive(false);
    }

    void Update()
    {
        if (!gameActive || !inputEnabled) return;

        rotatingZone.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(HandleAttempt());
        }
    }

    IEnumerator HandleAttempt()
    {
        inputEnabled = false;

        attemptCount++;

        if (rotatingZoneCollider.IsTouching(needleCollider))
        {
            successCount++;
            Debug.Log("SUCCESS");
            PlaySound(ding);
            PlaySound(punch);
        }
        else
        {
            Debug.Log("MISS");
            PlaySound(bonecrack);
        }

        UpdateSuccessText();

        if (attemptCount >= maxChances)
        {
            yield return new WaitForSeconds(delayBetweenRounds);
            EndGame();
        }
        else
        {
            yield return new WaitForSeconds(delayBetweenRounds);
            ResetZoneAngle();
            inputEnabled = true;
        }
    }

    void ResetZoneAngle()
    {
        float newAngle = Random.Range(0f, 360f);
        rotatingZone.rotation = Quaternion.Euler(0f, 0f, newAngle);
    }

    void UpdateSuccessText()
    {
        successText.text = $"SUCCESS: {successCount} / {maxChances}";
    }

    void EndGame()
    {
        gameActive = false;
        successText.text = $"FINAL RESULT: {successCount} / {maxChances} successful";

        if (successCount == maxChances)
        {
            if (winPanel != null) winPanel.SetActive(true);
            PlaySound(winSfx);
        }
        else
        {
            if (losePanel != null) losePanel.SetActive(true);
            PlaySound(witchLaugh);
        }
    }

    void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
