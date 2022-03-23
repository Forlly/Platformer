using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject topEnemyPos;
    [SerializeField] private int damage = 1;
    [SerializeField] private TypeOfEnemy type;
    public UnitStats UnitStats;
    
    public Vector2 GetTopPos()
    {
        return topEnemyPos.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            PlayerController player = col.gameObject.GetComponent<PlayerController>();
            if (player.BottomPos.transform.position.y > GetTopPos().y && type !=(TypeOfEnemy)0)
            {
                Destroy(gameObject);
            }
            else
            {
                player.ReceiveDamageFromEnemy(damage);
                player.healthController.UpdateCurrentHealthbar(player.currentHealth);
            }
        }
    }

    
}
public enum TypeOfEnemy
{
    Spike,
    Square
}
