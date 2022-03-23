using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyController 
{
    //public void ReceiveDamage(int damage);
    public void MakeDamage(PlayerController playerController,int damage);
}
