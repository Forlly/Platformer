using UnityEngine;
using UnityEngine.UI;

public class InventorySlotView : MonoBehaviour
{
    public InventoryView inventoryView;
    private Item item;
    [SerializeField] private Image BackgroundImage;
    [SerializeField] private Image ItemIamge;

    public Item Item
    {
        get => item;
        set
        {
          item = value;
          BackgroundImage.sprite = inventoryView.GetSlotTextureByRare(item.ItemRare);
          ItemIamge.sprite = item.Sprite;
        } 
    }
}
