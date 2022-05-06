using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapon")]
public class Weapon : ScriptableObject
{
    public int Id;
    public string Name;
    public Sprite Sprite;
    public string Description;
    public int Damage;
    public WeaponType WeaponType;
    public float flyDistance;
    public float speed;
    public AudioSource Audio;
}

public enum WeaponType
{
    gun,
    machineGun,
    laser
}
