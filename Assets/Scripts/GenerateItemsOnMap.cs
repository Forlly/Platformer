using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class GenerateItemsOnMap : MonoBehaviour
{
    public GameObject[] items;
    [SerializeField] private int maxCountOfItems;
    public static GenerateItemsOnMap Instants;

    private void Start()
    {
        Instants = this;
    }

    public void GenerateItems(int[,] map)
    {
        int countOfItems = Random.Range(1, maxCountOfItems);
        
        int itemX;
        int itemY;

        while(countOfItems >= 0)
        {
            itemX = Random.Range(1, map.GetUpperBound(0) - 1);
            itemY = map.GetUpperBound(1) - 1;
            
            while (map[itemX, itemY - 1] == 0 && map[itemX, itemY] == 0)
            {
                itemY--;
            }

            Instantiate(items[Random.Range(0, items.Length)],
                ProcedureGeneration.Instans.GetPositionOnMap(itemX, itemY), Quaternion.identity);
            
            countOfItems--;
        }
    }
}
