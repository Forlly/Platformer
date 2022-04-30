using UnityEngine;
/// <summary>
/// \brief Класс статического противника
/// </summary>
public class EnemySpikeController : MonoBehaviour, IEnemyController
{
    /// <summary>
    /// \param damage Урон который может нанести противник
    /// </summary>
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

    public void ReceiveDamage(PlayerController player)
    {
    }

    public Vector2 CheckDistanceToPlayer(Transform player)
    {
        return Vector2.zero;
    }

    public void FollowToPlayer(Transform player)
    {
    }
    /// <summary>
    /// \brief Метод нанесения урона 
    /// </summary>
    /// <param name="player">Персонаж, которому будет нанесен урон</param>
    /// <param name="_damage">Количество урона, которое будет нанесено</param>
    public void MakeDamage(PlayerController player,int _damage)
    {
        player.ReceiveDamageFromEnemy(_damage);
        player.healthController.UpdateCurrentHealthbar(player.currentHealth, player.startingHealth);
    }
    
    
}
