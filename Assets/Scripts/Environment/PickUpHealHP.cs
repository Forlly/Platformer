using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpHealHP : MonoBehaviour
{
    private bool isCollision = false;
    private int healHP = 1;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player")&& !isCollision)
        {
            isCollision = true;
            col.GetComponentInParent<PlayerController>().Heal(healHP);
            Destroy(gameObject);
        }
    }
}
