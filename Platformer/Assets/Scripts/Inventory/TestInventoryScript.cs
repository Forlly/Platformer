using UnityEngine;

public class TestInventoryScript : MonoBehaviour
{
    [SerializeField] private InventorySystem playerInventory;
    [SerializeField] private Item testAppleItem;
    private void Start()
    {
        playerInventory.TryAddItem(testAppleItem); // add item to most first slot
        playerInventory.TryAddItem(testAppleItem, 2); // add item to 3 slot
        playerInventory.TryAddItem(testAppleItem); // add item to 2 slot
        playerInventory.TryRemoveItem(1); // remove item from 2 slot
        playerInventory.TryRemoveItem(testAppleItem); // remove item from most first slot

        if (playerInventory.TryAddItem(testAppleItem, 9) == false)
        {
            playerInventory.TryAddItem(testAppleItem, 5);
        }
        playerInventory.TryAddItem(testAppleItem);
        playerInventory.TryAddItem(testAppleItem);
        playerInventory.TryAddItem(testAppleItem);
        playerInventory.TryAddItem(testAppleItem);
    }
}
