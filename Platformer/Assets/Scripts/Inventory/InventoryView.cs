using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class InventoryView : MonoBehaviour
{
    public InventorySystem inventorySystem;
    //remove it
    [SerializeField] private Item apple;
    //
    [SerializeField] private List<InventorySlotView> slotViews = new List<InventorySlotView>();
    [SerializeField] private List<SlotBackgroundType> slotBackground = new List<SlotBackgroundType>();

    private void Awake()
    {
        inventorySystem = new InventorySystem();
        slotViews[0].Item = apple;
        //Debug.Log(inventorySystem.AddItem(apple));
        //for (int i = 0; i < 2; i++)
        //{
        //    Debug.Log(slotViews[i]);
        //}
    }

    public Sprite GetSlotTextureByRare(ItemRare itemRare)
    {
        return slotBackground.FirstOrDefault(type => type.itemRare == itemRare)?.sprite;
    }
}

[Serializable]
public class SlotBackgroundType
{
    public ItemRare itemRare;
    public Sprite sprite;
}
