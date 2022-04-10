public class InventorySystem
{
    public static int InventorySizeX = 10;
    public static int InventorySizeY = 4;

   // private InventoryView _inventoryView;
    
    private readonly Item[,] items = new Item[InventorySizeX,InventorySizeY];
    public bool AddItem(Item item)
    {
        for (int i = 0; i < InventorySizeX; i++)
        {
            for (int j = 0; j < InventorySizeY; j++)
            {
                if (items[i, j] == null)
                {
                    items[i, j] = item;
                    //_inventoryView.slotViews[j].Item = item;
                    return true;
                }
            }
        }

        return false;
    }
}
