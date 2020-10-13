using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    public Sprite[] HeartSprites;

    public Image HeartUI;

    private ManController player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<ManController>();
    }
    private void FixedUpdate()
    {
        try
        {
            HeartUI.sprite = HeartSprites[player.curHealth];
            if (player.curHealth <= 0)
            {
                HeartUI.sprite = HeartSprites[0];
            }
        }
        catch (System.IndexOutOfRangeException)
        {
            HeartUI.sprite = HeartSprites[0];
        }
    }
}
