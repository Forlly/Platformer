using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = System.Random;

public class ProcedureGeneration : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase tilemapBase;
    public int[,] map;
    [SerializeField] public int[] highness;
    [SerializeField] public int[] spread;
    void Start()
    { 
        map = new int[width, height];
        map = GenerateArray(width, height, true);
        RenderMap(map, tilemap, tilemapBase);
        
        map = GenerationMap2(map, 10, 2);
        
        UpdateMap(map, tilemap, tilemapBase);
    }

    public static int[,] GenerateArray(int width, int height, bool empty)
    {
        int[,] _map = new int[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (empty)
                {
                    _map[x, y] = 0;
                }
                else
                {
                    _map[x, y] = 1;
                }
            }
        }
        return _map;
    }
    
    public static void UpdateMap(int[,] map, Tilemap tilemap, TileBase tile)
    {
        for (int i = 0; i < map.GetUpperBound(0); i++)
        {
            for (int j = 0; j < map.GetUpperBound(1); j++)
            {

                if (map[i, j] == 0)
                {
                    tilemap.SetTile(new Vector3Int(i, j, 0), null);
                }
                else
                {
                    tilemap.SetTile(new Vector3Int(i, j, 0), tile);
                }
            }
        }
    }
    
    
    public static void RenderMap(int[,] map, Tilemap tilemap, TileBase tile)
    {
        tilemap.ClearAllTiles();
        for (int i = 0; i < map.GetUpperBound(0) ; i++)
        {
            for (int j = 0; j < map.GetUpperBound(1); j++) 
            {
                if (map[i, j] == 1) 
                {
                    tilemap.SetTile(new Vector3Int(i, j, 0), tile);
                }
            }
        }
    }

    public int[,] GenerationMap2(int[,] map, int currentHeight, int minCount)
    {
        int previousHeight = currentHeight;
        int count = 0;
        for (int i = 0; i < map.GetUpperBound(0); i++)
        {
            for (int j = 0; j < map.GetUpperBound(1); j++)
            {
                if (i == 0)
                {
                    while(j < currentHeight && j < map.GetUpperBound(1))
                    {
                        map[i, j] = 1;
                        j++;
                    }
                }
                else
                {
                    if (count > minCount)
                    {
                        previousHeight = new Random().Next(currentHeight + 1, previousHeight + 2);
                        count = 0;
                    }
                    while(j < previousHeight && j < map.GetUpperBound(1))
                    {
                        map[i, j] = 1;
                        j++;
                    }
                }
                
            }
            count++;
        }

        return map;
    }
    public static int[,] GenerationMap(int[,] map, int[] height, int[] width)
    {
        int iW = 0;
        for (int i = 0; i < map.GetUpperBound(0); i++)
        {
            if(iW < width.Length)
            {
                int tempCount = 0;
                
                while (tempCount < width[iW])
                {
                    map[i+tempCount, height[iW]-1] = 1;
                    int j = 0;
                    
                    while (j < height[iW])
                    {
                        map[i+tempCount, j] = 1;
                        j++;
                    }
                    
                    tempCount++;
                }

                i += tempCount - 1;
            }

            iW++;
            Debug.Log(iW);
        }
        return map;
    }
}
