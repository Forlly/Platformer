using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class GenerateItemsOnMap : MonoBehaviour
{
    [SerializeField] private int maxCountOfItems;
    [SerializeField] private GameObject[] items;
    
    [SerializeField] private List<GameObject> generatesItems;
    public static GenerateItemsOnMap Instants;
    

    private void Start()
    {
        Instants = this;
    }

    public void GenerateItems(int[,] mapCurrent, int[,] mapPrev = default)
    {

        if (mapPrev != default)
        {
            for (int i = 0; i < generatesItems.Count; i++)
            {
                Destroy(generatesItems[i].gameObject);
            }
            
            generatesItems.Clear();
        }
        
        
        int countOfItems = Random.Range(1, maxCountOfItems);
        
        int itemX;
        int itemY;
        
        
        
        while(countOfItems >= 0)
        {
            itemX = Random.Range(1, mapCurrent.GetUpperBound(0) - 1);
            itemY = mapCurrent.GetUpperBound(1) - 1;
            
            while (mapCurrent[itemX, itemY - 1] == 0 && mapCurrent[itemX, itemY] == 0)
            {
                itemY--;
            }
            
            generatesItems.Add(Instantiate(items[Random.Range(0, items.Length)],
                ProcedureGeneration.Instans.GetPositionOnMap(itemX, itemY), Quaternion.identity));
            
            countOfItems--;
        }
        
    }
}
