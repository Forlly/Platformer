using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponController
{
   public void Fire();
   public void MakeDamage(IEnemyController enemyController);
}
