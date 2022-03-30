using UnityEngine;

public class EnemySpikeController : MonoBehaviour, IEnemyController
{
    [SerializeField] private int damage = 1;
    public UnitStats UnitStats;
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            PlayerController player = col.gameObject.GetComponent<PlayerController>();
            MakeDamage(player, damage);
        }
    }

    public void ReceiveDamage(int damage)
    {
    }

    public Vector2 CheckDistanceToPlayer(Transform player)
    {
        return Vector2.zero;
    }

    public void FollowToPlayer(Transform player)
    {
    }


    public void MakeDamage(PlayerController player,int _damage)
    {
        player.ReceiveDamageFromEnemy(_damage);
        player.healthController.UpdateCurrentHealthbar(player.currentHealth, player.startingHealth);
    }
    
    
}
