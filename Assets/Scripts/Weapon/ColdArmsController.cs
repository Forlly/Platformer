using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdArmsController : MonoBehaviour, IWeaponController
{
    [SerializeField] public Weapon weapon;
    private PlayerController _playerController;
    private WeaponController weaponController;
    private bool isSwing = false;
    
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
        if (isSwing)
        {
            isSwing = false;
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
    }

    private IEnumerator SwingWeapon()
    {
        WeaponController weaponController = FindObjectOfType<WeaponController>();
        weaponController.weaponImg.flipY = true;
        yield return new WaitForSeconds(0.13f);
        weaponController.weaponImg.flipY = false;
        isSwing = true;
    }

    /*private IEnumerator SwingWeapon()
    {
        WeaponController weaponController = FindObjectOfType<WeaponController>();
        Quaternion weaponImgTransform = weaponController.weaponImg.transform.localRotation;
        Quaternion Rotation = weaponImgTransform;
        Rotation.z = -45;
        weaponController.weaponImg.transform.localRotation = 
            Quaternion.Euler(weaponImgTransform.x, weaponImgTransform.y, Rotation.z);
        yield return new WaitForSeconds(0.1f);
        weaponController.weaponImg.transform.localRotation 
            = Quaternion.Euler(weaponImgTransform.x, weaponImgTransform.y, weaponImgTransform.z);;
        isSwing = true;
        transform.right = Vector3.right;
    }*/

    public void MakeDamage(IEnemyController enemyController)
    {
        enemyController.ReceiveDamage(weapon.Damage);
    }
}
