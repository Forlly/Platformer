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
                CaveGeneration2(Random.Range(1, maxSizeOfCaves));
            }
        }
        
        return map;
    }

    private int[,] CaveGeneration2(int reqCaveAmount)
    {

        int size;
        int direction;
        float chanceToChangeDir;

        int countOfSteps = 0;
        int countOfStepsOneWay = 0;
        bool stepLeft = true, stepRight = true;
        
        int caveX = Random.Range(1, map.GetUpperBound(0) - 1);
        int caveY = Random.Range(1, startHeight);

        /*if (map[caveX, caveY + 1 ] == 0 || map[caveX + 1, caveY] == 0 || map[caveX - 1, caveY] == 0 )
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
        }*/

        if (map[caveX, caveY] == 1)
        {
            while ( map[caveX, caveY + 1 ] != 0 || map[caveX, caveY] != 1)
            {
                caveY++;
            }
        }
        else
        {
            while ( map[caveX, caveY + 1 ] != 0 || map[caveX, caveY] != 1)
            {
                caveY--;
            }
        }
        
        map[caveX, caveY] = 0;

        size = Random.Range(2, 3);
        for (int i = 1; i < size ; i++)
        {
            if (i % 2 == 0)
            {
                map[caveX + i, caveY] = 0;
                caveX += i;
            }
            else
            {
                map[caveX - i, caveY] = 0;
                caveX -= i;
            }
        }

        while (countOfSteps < reqCaveAmount)
        {
            
            size = Random.Range(2, 3);
            direction = Random.Range(0,1);

            if (direction == 0 && stepLeft)
            {
                map[caveX, caveY - 1] = 0;

                if (caveX - size - 1 < 2)
                {
                    break;
                }
                
                for (int i = 1; i < size; i++)
                {
                    map[caveX - i, caveY - 1] = 0;
                }

                caveX -= size - 1;
                caveY -= 1;
                if (caveY - 1 < 2)
                {
                    break;
                }
                
                countOfStepsOneWay++;
                
                if (countOfStepsOneWay >= 2)
                {
                    chanceToChangeDir = Random.Range(0f, 1f);
                    if (chanceToChangeDir < 0.7f)
                    {
                        stepRight = true;
                        stepLeft = false;
                    
                        countOfStepsOneWay = 0;
                    }
                }
                else
                {
                    stepRight = false;
                }

                countOfSteps++;
            }
            else if(stepRight)
            {
                map[caveX, caveY - 1] = 0;

                if (caveX + size - 1 > map.GetUpperBound(1) - 1)
                {
                    break;
                }
                
                for (int i = 1; i < size; i++)
                {
                    map[caveX + i, caveY - 1] = 0;
                }
                
                caveX += size - 1;
                caveY -= 1;
                if (caveY - 1 < 2)
                {
                    break;
                }
                
                countOfStepsOneWay++;
                
                if (countOfStepsOneWay >= 2)
                {
                    chanceToChangeDir = Random.Range(0f, 1f);
                    if (chanceToChangeDir<0.7f)
                    {
                        stepRight = false;
                        stepLeft = true;

                        countOfStepsOneWay = 0;
                    }
                }
                else
                {
                    stepLeft = false;
                }
                
                countOfSteps++;
            }
            
        }
        return map;
    }
    
 
}
