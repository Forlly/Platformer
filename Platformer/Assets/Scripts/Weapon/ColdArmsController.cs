using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdArmsController : MonoBehaviour, IWeaponController
{
    [SerializeField] public Weapon weapon;
    private PlayerController _playerController;
    
    public Weapon GetWeapon()
    {
        return weapon;
    }

    public void SetWeapon(Weapon _weapon)
    {
        weapon = _weapon;
    }

    public void Fire()
    {
        throw new System.NotImplementedException();
    }

    public void MakeDamage(IEnemyController enemyController)
    {
        enemyController.ReceiveDamage(weapon.Damage);
    }
}
