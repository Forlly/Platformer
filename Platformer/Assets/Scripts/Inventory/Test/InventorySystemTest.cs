using System.Collections.Generic;
using UnityEngine;

public class InventorySystemTest : MonoBehaviour
{
    [SerializeField] private int countSlots;
    public List<Item> slots = new List<Item>();
    [SerializeField] private InventoryViewTest _inventoryViewTest;

    void Awake()
    {
        for (int i = 0; i < countSlots; i++)
        {
            slots.Add(null);
        }
        _inventoryViewTest.GenerateSlotsView(slots);
    }

    public bool AddItem(Item item, int id)
    {
        if (id >= slots.Count)
            return false;
        
        if (slots[id] == null)
        {
            slots[id] = item;
            _inventoryViewTest.UpdateSlotsView(slots[id], id);
        }
        else
        {
            Debug.Log("Slot is't Empty");
            return false;
        }
        
        return true;
    }
    public bool AddItem(Item item)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i] == null)
            {
                slots[i] = item;
                _inventoryViewTest.UpdateSlotsView(slots[i], i);
                return true;
            }
        }

        return false;
    }

    public bool DeleteItem(int id)
    {
        if(id >= slots.Count)
            return false;
        
        for (int i = 0; i < slots.Count; i++)
        {
            if (id == i)
            {
                slots[i] = null;
                _inventoryViewTest.UpdateSlotsView(slots[i], i);
                return true;
            }
        }

        return false;
    }
}
