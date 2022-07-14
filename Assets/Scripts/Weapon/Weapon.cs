using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapon")]
public class Weapon : ScriptableObject
{
    public int Id;
    public string Name;
    public Sprite Sprite;
    public string Description;
    public TypeOfDamage TypeOfDamage;
    public int Damage;
    public WeaponType WeaponType;
    public float flyDistance;
    public float speed;
    public AudioSource Audio;

    public int weaponClip;
    public ChuckType chuckType;

}

public enum WeaponType
{
    gun,
    machineGun,
    laser
}

public enum ChuckType
{
    none,
    chuckGun,
    chyckMGun
}

public enum TypeOfDamage
{
    coldarms,
    firearms
}
