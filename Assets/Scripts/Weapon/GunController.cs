using System.Collections;
using UnityEngine;

public class GunController :  MonoBehaviour,IWeaponController
{
    [SerializeField] public Weapon weapon;
    public Bullet bullet;
    public Transform spawnBulletPos;
    private PlayerController _playerController;

    private void SearchPlayerController()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }

    public Weapon GetWeapon()
    {
        return weapon;
    }

    public void SetWeapon(Weapon _weapon)
    {
        this.weapon = _weapon;
        WeaponController weaponController = FindObjectOfType<WeaponController>();
        bullet = weaponController.bullet;
        spawnBulletPos = weaponController.spawnBulletPos;
    }
    
    public void Fire()
    {
        SearchPlayerController();
        StartCoroutine(Shot());
    }

    private IEnumerator Shot()
    {
        Vector3 startPoint = spawnBulletPos.position;
        Bullet _bullet = Instantiate(bullet, startPoint, Quaternion.identity);
        _bullet.GunController = this;
        float wspeed = weapon.speed * Time.deltaTime;
        Vector3 endPoint = startPoint + new Vector3(_playerController.GetDirection(), 0, 0) * weapon.flyDistance;
        float progressFly = 0f;
        while (_bullet.onFly)
        {
            progressFly += wspeed;
            _bullet.transform.position = Vector3.Lerp(startPoint, endPoint, progressFly);
            if (progressFly >= 1)
            {
                Destroy(_bullet.gameObject);
                yield break;
            }
            yield return null;
        }
    }

    public void MakeDamage( IEnemyController enemyController)
    {
        enemyController.ReceiveDamage(weapon.Damage);
    }
}
