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

    void Start()
    {
        StartSkillCheck();
        StartSkillCheck();
        StartSkillCheck();
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
            Debug.Log(" SUCCESS!");
            nextButton.SetActive(true);
        }
        else
        {
            Debug.Log(" FAILURE!");
            nextButton.SetActive(true);
        }


        isRunning = false;
    }

}