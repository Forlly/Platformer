using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    private InventorySystemTest _inventorySystemTest;
    [SerializeField] private Item _item;

    private void SearchInventorySystem()
    {
        _inventorySystemTest = FindObjectOfType<InventorySystemTest>();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            SearchInventorySystem();
            _inventorySystemTest.AddItem(_item);
            Destroy(gameObject);
        }
    }
}
