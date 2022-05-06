using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWeapon : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
    private bool isCollision = false;
    private GunController _gunController;
    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.CompareTag("Player")&& !isCollision)
        {
            isCollision = true;
            TakeWeapon();
        }
    }

    private void TakeWeapon()
    {
        SearchGunController();
        _gunController._weapon = weapon;
        _gunController.weaponImg.sprite = weapon.Sprite;
        StartCoroutine(DestroyWeapon());
    }
    IEnumerator DestroyWeapon()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

    private void SearchGunController()
    {
        _gunController = FindObjectOfType<GunController>();
    }
}
