using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoost : MonoBehaviour {

    private ManController player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<ManController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(player.Boost());
            Destroy(gameObject);
        }
    }
}
