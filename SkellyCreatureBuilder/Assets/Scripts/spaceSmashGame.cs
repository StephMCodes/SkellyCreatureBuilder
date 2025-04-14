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

    [Header("SFX")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource audioSourceSteps;
    [SerializeField] private AudioClip witchLaugh;
    [SerializeField] private AudioClip whistle;
    [SerializeField] private AudioClip[] steps;
    //[SerializeField] private AudioClip step;
    //[SerializeField] private AudioClip bonecrack;
    [SerializeField] private AudioClip winSfx;

    [Header("Endgame Panels")]
    public GameObject winPanel;
    public GameObject losePanel;

    [Header("Game Settings")]
    public float gameDuration = 20f;

    [Header("SpaceBar Flash")]
  public RawImage flashImage;
public Color flashColorStart = new Color(0f, 0f, 0.545f); // Deep Blue (#00008B)
public Color flashColorEnd = Color.white;
public float flashDuration = 0.5f;
public float pulseSpeed = 5f;

private bool isFlashing = false;
private float flashTimer = 0f;

    private int score = 0;
    private float timer;
    private bool gameRunning = false;
    private int targetScore;

    // Bone bonus
    private int legBoneBonus = 0;
    private bool bonesDetected = false;

    void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    void Steps()
    {
        AudioClip step = steps[UnityEngine.Random.Range(0, steps.Length)];
        audioSourceSteps.PlayOneShot(step);
    }

    void Start()
    {
        audioSourceSteps = GetComponent<AudioSource>();
        
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

        PlaySound(whistle);
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
            Steps();
            //PlaySound(step);
            score += 1 + (bonesDetected ? legBoneBonus : 0);
            UpdateUI();

            if (flashImage != null)
            {
                isFlashing = true;
                flashTimer = 0f;
            }

            if (isFlashing && flashImage != null)
            {
                flashTimer += Time.deltaTime;

                float t = Mathf.PingPong(flashTimer * pulseSpeed, 1f);
                flashImage.color = Color.Lerp(flashColorStart, flashColorEnd, t);

                if (flashTimer >= flashDuration)
                {
                    isFlashing = false;
                    flashImage.color = flashColorStart; // Reset to deep blue
                }
            }
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
            PlaySound(winSfx);
            if (winPanel != null) winPanel.SetActive(true);
        }
        else
        {
            Debug.Log("You Lose.");
            PlaySound(witchLaugh);
            if (losePanel != null) losePanel.SetActive(true);
        }
    }
}
