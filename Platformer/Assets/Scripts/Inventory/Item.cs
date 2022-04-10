using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
[Serializable]
public class Item : ScriptableObject
{
    public int ID;
    public string Name;
    public ItemRare ItemRare;
    public Sprite Sprite;
}

public enum ItemRare
{
    unrare,
    rare,
    uniqe,
    epic,
    legendary
}