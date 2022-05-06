using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// \brief Интерфейс определяющий функционал противников
/// </summary>
public interface IEnemyController 
{
    public void ReceiveDamageFromPlayer(PlayerController player);
    public void ReceiveDamage(int _damage);
    public Vector2 CheckDistanceToPlayer(Transform player);
    public void FollowToPlayer(Transform player);
    public void MakeDamage(PlayerController playerController,int damage);
}
