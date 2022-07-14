using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class InventorySystemTest : MonoBehaviour
{
    [SerializeField] private int countSlots;
    public List<Item> slots = new List<Item>();
    [SerializeField] private InventoryViewTest _inventoryViewTest;

    void Start()
    {
        PlayerProgress playerProgress = SaveSystem.LoadFile<PlayerProgress>(Path.Combine(Application.dataPath, "Json"),
            "PlayerProgress.json");
        
        for (int i = 0; i < countSlots; i++)
        {
            if (i < playerProgress.inventorySlots.Count && playerProgress != null)
            {
                for (int j = 0; j < LinkStore.Instans.inventoryItems.Count; j++)
                {
                    if (LinkStore.Instans.inventoryItems[j].ID == playerProgress.inventorySlots[i])
                    {
                        slots.Add(LinkStore.Instans.inventoryItems[j]);
                    }
                }
            }
            else
                slots.Add(null);
        }
        _inventoryViewTest.GenerateSlotsView(slots);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            int i = 0;
            WeaponController weaponController = FindObjectOfType<WeaponController>();
            
            foreach (Item item in slots.Where(item => item != null))
            {
                if (item.ID == 2 && weaponController.weapon.WeaponType == WeaponType.gun)
                {
                    weaponController.currentCountOfBullets = weaponController.weapon.weaponClip;
                    DeleteItem(i);
                }
                else if (item.ID == 3 && weaponController.weapon.WeaponType == WeaponType.machineGun)
                {
                    weaponController.currentCountOfBullets = weaponController.weapon.weaponClip;
                    DeleteItem(i);
                }
                i++;  
            }
        }
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
    
    public int GetIdItemView(ItemViewTest itemViewTest)
    {
        for (int i = 0; i < _inventoryViewTest.items.Count; i++)
        {
            if (itemViewTest == _inventoryViewTest.items[i])
            {
                return i;
            }
        }

        return -1;
    }
}
