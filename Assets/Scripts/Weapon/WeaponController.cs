using System;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] public Weapon weapon;
    private IWeaponController weaponController;
    [SerializeField] public SpriteRenderer weaponImg;
    [SerializeField] public Bullet bullet;
    [SerializeField] public Transform spawnBulletPos;
    [SerializeField] public int currentCountOfBullets;
    [SerializeField] public ChuckType chuckType;
    
    public void ChangeWeapon()
    {
        if (weaponController != null)
        {
            ColdArmsController tempCold = GetComponent<ColdArmsController>();
            GunController tempGun = GetComponent<GunController>();
            Destroy(tempCold);
            Destroy(tempGun);
        }
        if (weapon.TypeOfDamage == TypeOfDamage.coldarms)
        {
            weaponController = gameObject.AddComponent<ColdArmsController>();
        }
        else if (weapon.TypeOfDamage == TypeOfDamage.firearms)
        {
            weaponController = gameObject.AddComponent<GunController>();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (weapon)
            {
                Debug.Log(weaponController);
                Debug.Log(currentCountOfBullets);
                if (currentCountOfBullets > 0)
                {
                    weaponController.SetWeapon(weapon);
                    weaponController.Fire();
                    currentCountOfBullets--;
                }
            }
        }
    }

    
}
