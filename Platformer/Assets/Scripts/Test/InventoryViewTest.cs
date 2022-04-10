using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryViewTest : MonoBehaviour
{
    private readonly InventoryTest _inventoryTest = new InventoryTest();
    [SerializeField] private RectTransform content;
    private readonly List<SlotViewTest> _slotsViewTests = new List<SlotViewTest>();
    [SerializeField] private GameObject _slotTest;
    
    private void Start()
    {
        for (int i = 0; i < _inventoryTest.countSlots; i++)
        {
            SlotViewTest slot = Instantiate(_slotTest, content).GetComponent<SlotViewTest>();
            _slotsViewTests.Add(slot);
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
