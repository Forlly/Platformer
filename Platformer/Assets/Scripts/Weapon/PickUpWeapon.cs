using System.Collections;
using UnityEngine;

public class PickUpWeapon : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
    private bool isCollision = false;
    private WeaponController weaponController;
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
        weaponController.weapon = weapon;
        weaponController.weaponImg.sprite = weapon.Sprite;
        weaponController.ChangeWeapon();
        StartCoroutine(DestroyWeapon());
    }
    IEnumerator DestroyWeapon()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

    private void SearchGunController()
    {
        weaponController = FindObjectOfType<WeaponController>();
    }
}
