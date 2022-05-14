using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdArmsController : MonoBehaviour, IWeaponController
{
    [SerializeField] public Weapon weapon;
    private PlayerController _playerController;
    private WeaponController weaponController;
    
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
        SendRay();
    }
    
    private void SendRay()
    {
        StartCoroutine(SwingWeapon());
        LayerMask ignoreLayer = 1 << 7;
        RaycastHit2D hit = 
            Physics2D.Raycast(transform.position, transform.right, weapon.flyDistance, ignoreLayer);
        Debug.DrawRay(transform.position, transform.right * weapon.flyDistance, Color.yellow);
        if (hit)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                IEnemyController enemyController = hit.transform.gameObject.GetComponent<IEnemyController>();
                MakeDamage(enemyController);
            } 
        }
    }

    private IEnumerator SwingWeapon()
    {
        Debug.Log("hi");
        WeaponController weaponController = FindObjectOfType<WeaponController>();
        Quaternion weaponImgTransform = weaponController.weaponImg.transform.localRotation;
        Quaternion Rotation = weaponImgTransform;
        Rotation.z = -45;
        int speedRotation = -5;
        Debug.Log(weaponImgTransform);
        Debug.Log(Rotation);
        while (speedRotation > Rotation.z)
        {
            speedRotation -= 5;
            weaponImgTransform = Quaternion.Lerp(weaponImgTransform, Rotation, speedRotation);
            Debug.Log(weaponImgTransform);
        }

        yield return 0;

    }

    public void MakeDamage(IEnemyController enemyController)
    {
        enemyController.ReceiveDamage(weapon.Damage);
    }
}
