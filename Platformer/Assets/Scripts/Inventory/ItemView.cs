using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    [HideInInspector] public InventoryView inventoryView;
    
    [SerializeField] private Image BackgroundImage;
    [SerializeField] private Image ItemIamge;


    private Item item;
    public Item Item
    {
        get => item;
        set
        {
          item = value;
          ItemIamge.enabled = item != null;
          if (item == null)
          {
              BackgroundImage.sprite = inventoryView.GetSlotTextureByRare(ItemRare.unrare);
              return;
          }
          
          BackgroundImage.sprite = inventoryView.GetSlotTextureByRare(item.ItemRare);
          ItemIamge.sprite = item.Sprite;
        } 
    }
}
