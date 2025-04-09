using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    ////Reference memory game script
    //static MemoryGameScript memoryGameScript;

    private void Start()
    {
        Debug.Log("skulls: " + health);
    }
    public int health = MemoryGameScript.skulls;
    public int numOfHearts; //visible health

    

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
}
