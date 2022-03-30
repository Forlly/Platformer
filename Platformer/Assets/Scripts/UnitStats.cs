using UnityEngine;

[CreateAssetMenu(fileName = "UnitStats", menuName = "ScriptableObjects/UnitStats")]
public class UnitStats : ScriptableObject
{
    public string Id;
    public string Name;
    public string Description;
    public FractionType FractionType;
    public bool CanReceiveDamage;
    public float speed;
}
public enum FractionType
{
    Neutral,
    Friendly,
    Hostile
}