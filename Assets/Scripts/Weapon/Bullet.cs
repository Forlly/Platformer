using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GunController GunController;
    public bool onFly = true;
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            IEnemyController enemyController = col.gameObject.GetComponent<IEnemyController>();
            GunController.MakeDamage(enemyController);
        }
        else if (col.gameObject.CompareTag("Player"))
        {
            return;
        }
        
        onFly = false;
        Destroy(gameObject);
    }
}
