using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
[Serializable]
public class Item : ScriptableObject
{
    public Item(Item item)
    {
        ID = item.ID;
        Name = item.Name;
        ItemRare = item.ItemRare;
        Sprite = item.Sprite;
    }
    
    public int ID;
    public string Name;
    public ItemRare ItemRare;
    public Sprite Sprite;
}

public enum ItemRare
{
    none,
    unrare,
    rare,
    uniqe,
    epic,
    legendary
}