using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GunController :  MonoBehaviour,IWeaponController
{
    [SerializeField] public Weapon _weapon;
    [SerializeField] private Bullet bullet;
    [SerializeField] private Transform spawnBulletPos;
    private PlayerController _playerController;
    [SerializeField] public SpriteRenderer weaponImg;

    private void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if (_weapon)
            {
                Fire();
            }
        }
    }


    public void Fire()
    {
        StartCoroutine(Shot());
    }

    private IEnumerator Shot()
    {
        Vector3 startPoint = spawnBulletPos.position;
        Bullet _bullet = Instantiate(bullet, startPoint, Quaternion.identity);
        _bullet.GunController = this;
        float wspeed = _weapon.speed * Time.deltaTime;
        Vector3 endPoint = startPoint + new Vector3(_playerController.GetDirection(), 0, 0) * _weapon.flyDistance;
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
        enemyController.ReceiveDamage(_weapon.Damage);
    }
}
