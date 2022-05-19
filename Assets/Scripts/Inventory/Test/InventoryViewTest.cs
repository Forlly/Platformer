using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryViewTest : MonoBehaviour
{
    [SerializeField] private RectTransform content;
    [SerializeField] private InventorySystemTest inventory;
    [SerializeField] private ItemViewTest itemView;
    public List<ItemViewTest> items = new List<ItemViewTest>();
    [SerializeField] private List<ItemRareSprite> backgraoundsOfRare = new List<ItemRareSprite>();

    public void GenerateSlotsView(List<Item> slots)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            ItemViewTest temp = Instantiate(itemView, content);
            items.Add(temp);
            items[i].Item = slots[i];
        }
    }
    

    public void UpdateSlotsView(Item slotItm, int id)
    {
        items[id].Item = slotItm;
    }
    
   public Sprite GetBackgroundRareItem(ItemRare itemRare)
   {
       for (int i = 0; i < backgraoundsOfRare.Count; i++)
       {
           if (itemRare == backgraoundsOfRare[i].itemRare)
           {
               return backgraoundsOfRare[i].backgroundImageRare;
           }
       }

       return null;
   }
}

[Serializable]
public class ItemRareSprite
{
    public Sprite backgroundImageRare;
    public ItemRare itemRare;
}
