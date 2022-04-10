using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotTest
{
    public bool isOccupied;
    public int id;
    public Item _item;

    public void AddSlotItem(Item item, int _id)
    {
        _item = ScriptableObject.CreateInstance<Item>();
        _item = item;
        id = _id;
        isOccupied = true;
    }
    public void AddSlot(int _id)
    {
        id = _id;
        isOccupied = false;
    }

    private void DeleteSlot()
    {
        
    }
}
