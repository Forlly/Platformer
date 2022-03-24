using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyQSlikeController : MonoBehaviour, IEnemyController
{
    [SerializeField] private GameObject topEnemyPos;
    [SerializeField] private int startingHP = 2;
    [SerializeField] private int currentHP;
    public EnemyHPController enemyHpController;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private int damage = 2;
    public UnitStats UnitStats;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        enemyHpController = GetComponent<EnemyHPController>();
        currentHP = startingHP;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            PlayerController player = col.gameObject.GetComponent<PlayerController>();
            if (player.BottomPos.transform.position.y > GetTopPos().y)
            {
                ReceiveDamage(player.damage);
                StartCoroutine(AfterDamage());
            }
            else
            {
                MakeDamage(player, damage);
                Debug.Log(player.BottomPos.transform.position.y );
                Debug.Log(topEnemyPos.transform.position.y);
            }
        }
    }
    
    public Vector2 GetTopPos()
    {
        return topEnemyPos.transform.position;
    }

    private void ReceiveDamage( int playerDamage)
    {
        currentHP -= playerDamage;
        if (currentHP<=0)
            Destroy(gameObject);
        Debug.Log(currentHP);
        Debug.Log(startingHP);
        Debug.Log(enemyHpController);
        enemyHpController.UpdateSpriteHP(currentHP, startingHP);
    }
    public void MakeDamage(PlayerController player,int _damage)
    {
        player.ReceiveDamageFromEnemy(_damage);
        player.healthController.UpdateCurrentHealthbar(player.currentHealth, player.startingHealth);
    }
    
    private IEnumerator AfterDamage()
    {
        float step_sec = 0.1f;
        for (float i = 0; i < 1; i += step_sec)
        {
            _spriteRenderer.color = new Color(1,0.3f,0.3f,0.3f);
            yield return new WaitForSeconds(step_sec/2);
            _spriteRenderer.color = new Color(1,0.3f,0.3f,0.6f);
            yield return new WaitForSeconds(step_sec/2);
        }
        _spriteRenderer.color = new Color(1,1,1,1);
    }
}
