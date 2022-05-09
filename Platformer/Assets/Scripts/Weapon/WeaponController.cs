using System;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] public Weapon weapon;
    private IWeaponController weaponController;
    [SerializeField] public SpriteRenderer weaponImg;
    [SerializeField] public Bullet bullet;
    [SerializeField] public Transform spawnBulletPos;
    
    public void ChangeWeapon()
    {
        if (weaponController == null)
        {
            if (weapon.TypeOfDamage == TypeOfDamage.coldarms)
            {
                weaponController = gameObject.AddComponent<ColdArmsController>();
            }
            else if (weapon.TypeOfDamage == TypeOfDamage.firearms)
            {
                weaponController = gameObject.AddComponent<GunController>();
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (weapon)
            {
                weaponController.SetWeapon(weapon);
                weaponController.Fire();
            }
        }
    }
}
