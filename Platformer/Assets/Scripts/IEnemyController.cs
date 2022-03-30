using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyController 
{
    public void ReceiveDamage(int damage);
    public Vector2 CheckDistanceToPlayer(Transform player);
    public void FollowToPlayer(Transform player);
    public void MakeDamage(PlayerController playerController,int damage);
}
