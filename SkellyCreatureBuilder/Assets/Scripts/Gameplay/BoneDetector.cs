using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneDetector : MonoBehaviour
{
    [Header("Base stats")]
    public float baseSpeed = 1f;
    public float footSpeedBonus = 0.5f;
    public float baseStrength = 1f;
    public float handStrengthBonus = 1f;
    public float baseMental = 1f;
    public float skullMentalBonus = 1f;

    // current stats
    public static float speed;
    public static float strength;
    public static float mental;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // count how many grandchildren carry the given tag
            int footCount = CountTaggedParts("foot");
            int handCount = CountTaggedParts("hand");
            int skullCount = CountTaggedParts("skull");

            // not the final way the stats should be counted but its a start
            speed = baseSpeed + footCount * footSpeedBonus;
            strength = baseStrength + handCount * handStrengthBonus;
            mental = baseMental + skullCount * skullMentalBonus;

            Debug.Log($"foots: {footCount} -> speed: {speed}\n" +
                      $"hands: {handCount} -> strength: {strength}\n" +
                      $"skulls: {skullCount} -> mental: {mental}");
        }
    }

    int CountTaggedParts(string tag)
    {
        int count = 0;
        foreach (Transform child in transform)
            foreach (Transform grand in child)
                if (grand.CompareTag(tag))
                    count++;
        return count;
    }
}
