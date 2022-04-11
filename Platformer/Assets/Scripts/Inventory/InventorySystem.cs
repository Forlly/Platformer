using System;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private int InventorySize = 12;
    [SerializeField] private InventoryView inventoryView;
    
    private Item[] items;
    private void Awake()
    {
        Array.Resize(ref items, InventorySize);
        inventoryView.InitViewSettings(InventorySize);
    }

    public bool TryAddItem(Item item, int index)
    {
        if (index > items.Length - 1)
        {
            return false;
        }
        if (items[index] == null)
        {
            items[index] = item;
            inventoryView.SetItem(item, index);
            return true;
        }

        return false;
    }
    
    public bool TryAddItem(Item item)
    {
        for (int i = 0; i < InventorySize; i++)
        {
            if (items[i] != null)
                continue;
            
            items[i] = item;
            inventoryView.SetItem(item, i);
            return true;
        }

        return false;
    }
    
    public bool TryRemoveItem(int index)
    {
        if (index > items.Length - 1)
        {
            return false;
        }
        if (items[index] != null)
        {
            items[index] = null;
            inventoryView.SetItem(null, index);
            return true;
        }

        return false;
    }
    public bool TryRemoveItem(Item item)
    {
        for (int i = 0; i < InventorySize; i++)
        {
            if (items[i] != item)
                continue;
            
            items[i] = null;
            inventoryView.SetItem(null, i);
            return true;
        }

        return false;
    }
}
