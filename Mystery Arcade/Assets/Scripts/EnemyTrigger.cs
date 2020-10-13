using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour {

    private ManController player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<ManController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (player.curHealth > 0)
            {
                player.Damage(1);

                StartCoroutine(player.Knockback(0.02f, 100, player.transform.position));
            }
        }
    }
}
