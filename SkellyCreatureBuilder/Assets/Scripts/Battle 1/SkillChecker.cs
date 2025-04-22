using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SkillChecker : MonoBehaviour
{
    public RectTransform successZone;
    public float rotationSpeed = 100f;
    public float needleAngle = 0f;
    public float zoneWidth = 20f;

    private float currentAngle = 0f;
    private bool isRunning = false;

    public GameObject nextButton;

    // SFX
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip witchLaugh;
    [SerializeField] AudioClip ding;
    [SerializeField] AudioClip punch;
    [SerializeField] AudioClip bonecrack;
    [SerializeField] AudioClip winSfx;
    //[SerializeField] AudioClip click;
    //[SerializeField] AudioClip click2;


    void Start()
    {
        StartSkillCheck();
        audioSource.PlayOneShot(ding);
    }

    void Update()
    {
        if (!isRunning) return;

        currentAngle += rotationSpeed * Time.deltaTime;
        currentAngle %= 360f;

        successZone.localRotation = Quaternion.Euler(0, 0, -currentAngle);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckSkillResult();
        }
    }

    public void StartSkillCheck()
    {
        float randomOffset = Random.Range(0f, 360f); // this is where the zone actually rotates
        currentAngle = randomOffset;

        // Random spin direction
        rotationSpeed = Mathf.Abs(rotationSpeed) * (Random.value > 0.5f ? 1f : -1f);

        successZone.localRotation = Quaternion.Euler(0, 0, -currentAngle);
        isRunning = true;
    }

    void CheckSkillResult()
    {
        float normalizedAngle = currentAngle % 360f;

        float lowerBound = (needleAngle - (zoneWidth / 2f) + 360f) % 360f;
        float upperBound = (needleAngle + (zoneWidth / 2f)) % 360f;

        bool success;

        if (lowerBound < upperBound)
        {
            success = normalizedAngle >= lowerBound && normalizedAngle <= upperBound;
        }
        else
        {
            success = normalizedAngle >= lowerBound || normalizedAngle <= upperBound;
        }

        if (success)
        {
            audioSource.PlayOneShot(punch);
            Debug.Log(" SUCCESS!");
            //winButton.SetActive(true);
            nextButton.SetActive(true);

        }
        else
        {
            audioSource.PlayOneShot(bonecrack);
            Debug.Log(" FAILURE!");
            //loseButton.SetActive(true);
            nextButton.SetActive(true);

        }

        isRunning = false;

    }

}