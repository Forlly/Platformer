using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class ProcedureGeneration : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private int startHeight;
    [SerializeField] private int maxSizeOfCaves;
    [Space] [Range(0f, 1f)]
    [SerializeField] private float ChanceToGenerateCave;


    [Space] [Range(0f, 1f)]
    [SerializeField] private float ChanceToChangeHeight;
    
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase tilemapBase;

    private int[,] map;
    
    void Start()
    { 
        map = new int[width, height];

        map = GenerationMap(startHeight, 2);
        float a = Time.realtimeSinceStartup;
        map = GenerateCaves(ChanceToGenerateCave);
        Debug.Log((Time.realtimeSinceStartup - a).ToString("F6"));
        UpdateMap(map, tilemap, tilemapBase);
    }
    
    public static void UpdateMap(int[,] map, Tilemap tilemap, TileBase tile)
    {
        for (int i = 0; i < map.GetUpperBound(0); i++)
        {
            for (int j = 0; j < map.GetUpperBound(1); j++)
            {
                if (map[i, j] == 0)
                    continue;
                
                tilemap.SetTile(new Vector3Int(i, j, 0), tile);
            }
        }
    }

    public int[,] GenerationMap(int _startHeight, int minCountHeightTiles)
    {
        int currentHeight = _startHeight;
        int countHeightTiles = 0;
        
        for (int j = 0; j < map.GetUpperBound(1); j++)
        {
            while(j < _startHeight)
            {
                map[0, j] = 1;
                j++;
            }
        }
        countHeightTiles++;
        
        for (int i = 1; i < map.GetUpperBound(0); i++)
        {
            if (countHeightTiles > minCountHeightTiles)
            {
                if (Random.Range(0f, 1f) <= ChanceToChangeHeight)
                {
                    int[] heghtI = {-2, -1, 1, 2}; 
                    currentHeight += heghtI[Random.Range(0, heghtI.Length)];
                    currentHeight = Mathf.Clamp(currentHeight ,1, height);
                    countHeightTiles = 0;
                }
            }
            
            for (int j = 0; j < map.GetUpperBound(1); j++)
            {
                while (j < currentHeight)
                {
                    map[i, j] = 1;
                    j++;
                }
            }
            countHeightTiles++;
        }

        return map;
    }


    private int[,] GenerateCaves(float _chanceToGenerateCave)
    {
        int countOfCaves = (startHeight * width) / (maxSizeOfCaves * maxSizeOfCaves);
        for (int i = 0; i < countOfCaves; i++)
        {
            if (Random.Range(0f, 1f) <= _chanceToGenerateCave)
            {
                CaveGeneration(Random.Range(1, maxSizeOfCaves));
            }
        }
        
        return map;
    }

    private int[,] CaveGeneration2(int reqCaveAmount)
    {
        int size;
        int caveX = Random.Range(1, map.GetUpperBound(0) - 1);
        int caveY = Random.Range(1, startHeight);
        
        while ( map[caveX, caveY + 1 ] != 0 || map[caveX, caveY] != 1)
        {
            caveY++;
        }

        map[caveX, caveY] = 0;

        size = Random.Range(1, 3);
        for (int i = 0; i < Mathf.Ceil(size/2); i++)
        {
            
        }
        return map;
    }
    
    private int[,] CaveGeneration(int reqCaveAmount)
    {
        int caveX = Random.Range(1, map.GetUpperBound(0) - 1);
        int caveY = Random.Range(1, startHeight);
        int caveAmount = 0;
        int countTilesAbouve = 0;
        int countOfErrors = 0;
        
        if (map[caveX, caveY + 1 ] == 0 || map[caveX + 1, caveY] == 0 || map[caveX - 1, caveY] == 0 )
        {
            map[caveX, caveY] = 0;
        }
        else
        {
            while ( map[caveX, caveY + 1 ] != 0 || map[caveX, caveY] != 1)
            {
                caveY++;
            }

            map[caveX, caveY] = 0;
        }
        

        while (caveAmount < reqCaveAmount)
        {
            int direction = Random.Range(0, 3);

            switch (direction)
            {
                case 0:
                    if ((caveX + 1) < (map.GetUpperBound(0) - 1) 
                                                       &&  map[caveX, caveY + 2] == 1 &&  map[caveX + 1, caveY + 2] == 1)
                    {
                        caveY++;

                        if (map[caveX, caveY] == 1 && map[caveX + 1, caveY] == 1)
                        {
                            map[caveX, caveY] = 0;
                            map[caveX + 1, caveY] = 0;

                            countTilesAbouve = 0;
                            caveAmount++;
                        }
                        
                    }
                    else
                    {
                        countOfErrors++;
                    }
                    break;
                case 1:
                    if ((caveY - 1) > 1 && (caveX - 1) > 1 &&  map[caveX, caveY - 2] == 1 
                        && map[caveX - 1, caveY - 2] == 1 )
                    {
                        caveY--;

                        if (map[caveX, caveY] == 1 && map[caveX - 1, caveY] == 1 && countTilesAbouve < 2)
                        {

                            map[caveX, caveY] = 0;
                            map[caveX - 1, caveY] = 0;

                            countTilesAbouve++;
                            caveAmount++;
                        }
                    }
                    else
                    {
                        countOfErrors++;
                    }
                    break;
                case 2:
                    if ((caveX + 1) < (map.GetUpperBound(0) - 1) && (caveY - 1) > 1 
                         &&  map[caveX + 2, caveY] == 1 &&  map[caveX + 2, caveY - 1] == 1)
                    {
                        caveX++;

                        if (map[caveX, caveY] == 1 && map[caveX, caveY - 1] == 1)
                        {
                            map[caveX, caveY] = 0;
                            map[caveX, caveY - 1] = 0;

                            countTilesAbouve = 0;
                            caveAmount++;
                        }
                    }
                    else
                    {
                        countOfErrors++;
                    }
                    break;
                case 3:
                    if ((caveX - 1) > 1
                                        &&  map[caveX - 2, caveY] == 1 &&  map[caveX - 2, caveY + 1] == 1)
                    {
                        caveX--;

                        if (map[caveX, caveY] == 1 && map[caveX, caveY + 1] == 1)
                        {
                            map[caveX, caveY] = 0;
                            map[caveX, caveY + 1] = 0;
                            
                            caveAmount++;
                        }
                    }
                    else
                    {
                        countOfErrors++;
                    }
                    break;
            }

            if (countOfErrors>10)
            {
                 break ;
            }
        }



        return map;
    }
}
