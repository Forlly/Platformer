using System.Collections.Generic;
using UnityEngine;

public class InventoryTest: MonoBehaviour
{
    public List<SlotTest> _slotsTests;
    public int countSlots = 7;
    public SlotViewTest _slotViewTest;

    public List<SlotViewTest> GenerateSlot(GameObject slot, RectTransform content, List<SlotViewTest> _slotsViewTests, 
        InventoryViewTest inventoryViewTest)
    {
        SlotViewTest slotObj = Instantiate(slot, content).GetComponent<SlotViewTest>();
        _slotsViewTests.Add(slotObj);
        slotObj._inventoryTest = inventoryViewTest._inventoryTest;
        slot._slotTest = new SlotTest();
        slot._slotTest.AddSlot(i);
        return _slotsViewTests;
    }
}
