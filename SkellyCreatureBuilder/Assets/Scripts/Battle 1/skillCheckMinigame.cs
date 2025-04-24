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
    [SerializeField] private GameObject MusicPlayer;

    [Header("End Panels")]
    public GameObject winPanel;
    public GameObject losePanel;

    private int successCount = 0;
    private int attemptCount = 0;
    private bool gameActive = true;
    private bool inputEnabled = true;

    // arm count from Bone detector
   // float armCount = BoneDetector.strength;

    void Start()
    {
        // Sets rotation speed 1 arm = 450 an extra arm -100
        rotationSpeed = Mathf.Max(50f, 450f - (Mathf.Max(BoneDetector.strength - 1, 0) * 100f)); // Prevents negative speed

        Debug.Log($"Arm count: {BoneDetector.strength}, Rotation Speed: {rotationSpeed}");

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
        else if (BoneDetector.strength > 0)
        {
            BoneDetector.strength--;
            Debug.Log("MISS! arms left: " + BoneDetector.strength);
            PlaySound(bonecrack);
        }
        else
        {
            Debug.Log("YOU LOSE");
            PlaySound(bonecrack);
            if (losePanel != null) losePanel.SetActive(true);
            MusicPlayer.SetActive(false);
            PlaySound(witchLaugh);
            gameActive = false;
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

        if (BoneDetector.strength >0)
        {
            if (winPanel != null) winPanel.SetActive(true);
            MusicPlayer.SetActive(false);
            PlaySound(winSfx);
        }
        else
        {
            if (losePanel != null) losePanel.SetActive(true);
            MusicPlayer.SetActive(false);
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
