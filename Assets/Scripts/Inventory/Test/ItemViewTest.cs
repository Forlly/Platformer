using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemViewTest : MonoBehaviour
{
    private Item _item;
    [SerializeField] private Image backgraund;
    [SerializeField] private Image itemImg;
    [SerializeField] private Image deleteButton;
    private InventoryViewTest _inventoryViewTest;

     private void SearchInventoryView()
     {
         _inventoryViewTest = FindObjectOfType<InventoryViewTest>();
     }
     public Item Item
    {
        get => _item;
        set
        {
            _item = value;
            
            if (_inventoryViewTest == null)
                SearchInventoryView();
            
            if (_item == null)
            { 
                backgraund.sprite = _inventoryViewTest.GetBackgroundRareItem(ItemRare.none);
                itemImg.enabled = false;
                deleteButton.enabled = false;
                deleteButton.GetComponentInChildren<Text>().enabled = false;
                return;
            }
            
            backgraund.sprite = _inventoryViewTest.GetBackgroundRareItem(_item.ItemRare);
            itemImg.enabled = true;
            deleteButton.enabled = true;
            deleteButton.GetComponentInChildren<Text>().enabled = true;
            itemImg.sprite = _item.Sprite;
        }
    }
    
}

