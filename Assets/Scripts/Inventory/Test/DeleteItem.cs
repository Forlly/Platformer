using System;
using UnityEngine;
using UnityEngine.UI;

public class DeleteItem : MonoBehaviour
{
    private InventorySystemTest _inventorySystemTest;
    [SerializeField] Button deleteItem;
    private ItemViewTest itemView;

    private void Start()
    {
        deleteItem.onClick.AddListener(RemoveItem);
    }

    private void SearchInventorySystem()
    {
        _inventorySystemTest = FindObjectOfType<InventorySystemTest>();
    }

    private void RemoveItem()
    {
        if (_inventorySystemTest == null)
        {
            SearchInventorySystem();
        }

        itemView = GetComponent<ItemViewTest>();
        int tempId = _inventorySystemTest.GetIdItemView(itemView);
        _inventorySystemTest.DeleteItem(tempId);
        //_inventorySystemTest.DeleteItem(itemView.Item.);

    }
}
