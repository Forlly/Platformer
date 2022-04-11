using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryViewTest : MonoBehaviour
{
    public InventoryTest _inventoryTest;
    [SerializeField] private RectTransform content;
    private List<SlotViewTest> _slotsViewTests = new List<SlotViewTest>();
    [SerializeField] private GameObject _slotTest;

    private void Start()
    {
        _inventoryTest = gameObject.AddComponent<InventoryTest>();
        for (int i = 0; i < _inventoryTest.countSlots; i++)
        {
            
            _slotsViewTests = _inventoryTest.GenerateSlot(_slotTest, content, _slotsViewTests, this);
            slot._inventoryTest = this;
            slot._slotTest = new SlotTest();
            slot._slotTest.AddSlot(i);
            if (i == 5)
            {
                slot._slotTest.AddSlotItem(slot.item, i);
                slot.ShowSlotItem(slot._slotTest);
            }
        }
        for (int i = 0; i < _inventoryTest.countSlots; i++)
        {
            Debug.Log(_slotsViewTests[i]._slotTest.isOccupied);
        }
    }
}
