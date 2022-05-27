using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpHealHP : MonoBehaviour
{
    private bool isCollision = false;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player")&& !isCollision)
        {
            isCollision = true;
            PlayerController playerController = col.GetComponent<PlayerController>();
            HealHP(playerController);
        }
    }

    private void HealHP(PlayerController player)
    {
        player.currentHealth += 1;
        player.healthController.UpdateCurrentHealthbar( player.currentHealth, player.startingHealth);
        Destroy(gameObject);
    }
}
