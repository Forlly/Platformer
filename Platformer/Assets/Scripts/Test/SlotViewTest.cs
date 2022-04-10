using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotViewTest : MonoBehaviour
{
    public SlotTest _slotTest;
    [SerializeField] private Image background;
    public Image itemImg;
    [SerializeField] public Item item;
    public InventoryViewTest _inventoryTest;
    private List<SlotTest> items = new List<SlotTest>();

    public void ShowSlotItem(SlotTest slotTest)
    {
        if (slotTest.isOccupied)
        {
            itemImg.sprite = slotTest._item.Sprite;
            itemImg.enabled = true;
        }
    }
    
}
