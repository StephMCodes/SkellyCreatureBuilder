using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class SkullHealthSystem : MonoBehaviour
{
    [Header("Skull Icons")]
    [SerializeField] private Image[] skullIcons; // the skulls UI

    [Header("No Skulls Warning")]
    [SerializeField] private TMP_Text noSkullsText; // 0 skulls

    private Coroutine shakeRoutine; // Used to control shaking of last skull

    // call this when skull count changes
    public void UpdateSkullUI(int skullCount)
    {
       
        for (int i = 0; i < skullIcons.Length; i++)
        {
            if (i < skullCount)
            {
                // if skull is within remaining count it enables and resets visuals
                skullIcons[i].enabled = true;
                skullIcons[i].color = Color.white;
                skullIcons[i].transform.localScale = Vector3.one;
            }
            else
            {
                // if skull was visible but now lost, animate its loss
                if (skullIcons[i].enabled)
                    StartCoroutine(AnimateSkullLoss(skullIcons[i]));
            }
        }

        // show warning text for 0 skulls
        if (noSkullsText != null)
            noSkullsText.gameObject.SetActive(skullCount == 0);

        // Shake the last skull if only one remains
        if (skullCount == 1 && skullIcons[0].enabled && gameObject.activeInHierarchy)
        {
            if (shakeRoutine == null)
                shakeRoutine = StartCoroutine(ShakeSkull(skullIcons[0].transform));
        }
        else
        {
            // Stop shake if not on the last skull anymore
            if (shakeRoutine != null)
            {
                StopCoroutine(shakeRoutine);
                shakeRoutine = null;
                skullIcons[0].transform.localPosition = Vector3.zero;
            }
        }
    }

    // animate skull and fancy visuals
    private IEnumerator AnimateSkullLoss(Image skull)
    {
        float flashDuration = 0.15f;
        float holdDuration = 0.3f;
        float fadeOutDuration = 0.4f;

        Color originalColor = skull.color;
        Vector3 originalScale = skull.transform.localScale;

        //flashes and bigger
        skull.color = Color.red;
        skull.transform.localScale = originalScale * 1.2f;
        yield return new WaitForSeconds(flashDuration);

        // hold red
        yield return new WaitForSeconds(holdDuration);

        // fades out 
        float elapsed = 0f;
        while (elapsed < fadeOutDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeOutDuration;
            skull.color = Color.Lerp(Color.red, new Color(1, 1, 1, 0), t);
            yield return null;
        }

        //fully disable skull icon
        skull.enabled = false;
        skull.transform.localScale = originalScale;
        skull.color = originalColor;
    }

    // shakes the last skull 
    private IEnumerator ShakeSkull(Transform skullTransform)
    {
        Vector3 originalPos = skullTransform.localPosition;
        float shakeAmount = 4f;
        float speed = 10f;

        while (true)
        {
            float x = Mathf.Sin(Time.time * speed) * shakeAmount;
            float y = Mathf.Cos(Time.time * speed * 1.2f) * shakeAmount * 0.5f;
            skullTransform.localPosition = originalPos + new Vector3(x, y, 0);
            yield return null;
        }
    }

}
