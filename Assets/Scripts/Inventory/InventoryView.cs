using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class InventoryView : MonoBehaviour
{
    private readonly List<ItemView> itemViews = new List<ItemView>();
    [SerializeField] private ItemView itemViewPrefab;
    [SerializeField] private RectTransform Content;
    [SerializeField] private List<SlotBackgroundType> slotBackgroundPresets = new List<SlotBackgroundType>();
    
    public Sprite GetSlotTextureByRare(ItemRare itemRare)
    {
        return slotBackgroundPresets.FirstOrDefault(type => type.itemRare == itemRare)?.sprite;
    }

    public void InitViewSettings(int countOfItems)
    {
        for (int i = 0; i < countOfItems; i++)
        {
            ItemView itemView = Instantiate(itemViewPrefab, Content);
            itemView.inventoryView = this;
            itemView.Item = null;
            itemViews.Add(itemView);
        }
    }
    public void SetItem(Item item, int itemPositionIndex)
    {
        itemViews[itemPositionIndex].Item = item;
    }
}

[Serializable]
public class SlotBackgroundType
{
    public ItemRare itemRare;
    public Sprite sprite;
}
