using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponController
{
   public Weapon GetWeapon();
   public void SetWeapon(Weapon _weapon);
   public void Fire();
   public void MakeDamage(IEnemyController enemyController);
}
