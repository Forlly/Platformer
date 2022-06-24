using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class ProcedureGeneration : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] public int startHeight;
    [SerializeField] private int maxSizeOfCaves;
    [Space] [Range(0f, 1f)]
    [SerializeField] public float ChanceToGenerateCave;


    [Space] [Range(0f, 1f)]
    [SerializeField] private float ChanceToChangeHeight;
    
    [SerializeField] public Tilemap tilemap;
    [SerializeField] public TileBase tilemapBase;

    public static ProcedureGeneration Instans;
    public LvlSettings lvlSettings;
    private string path;
    private string fileName = "LvlSettings.json";
    public int[,] map;
    
    
    void Start()
    {
        Instans = this;
        map = new int[width, height];

        path = Path.Combine(Application.dataPath, "Json");
        
        GetLvlSettings();

        if (lvlSettings.lvl == 0)
        {
            map = GenerationMap(startHeight, 6);
            float a = Time.realtimeSinceStartup;
            map = GenerateCaves(ChanceToGenerateCave);

            lvlSettings.lvl = 1;
            lvlSettings.listOfMaps = new List<ListMapLvl>();
            
            Debug.Log(lvlSettings.listOfMaps);
            lvlSettings.listOfMaps.Add(Encode(map));
            Debug.Log(lvlSettings.lvl);
            
            SaveSystem.SaveFile<LvlSettings>(lvlSettings, path, fileName);
            Debug.Log((Time.realtimeSinceStartup - a).ToString("F6"));
        }

        UpdateMap(Decode(lvlSettings.listOfMaps[SaveSystem.LoadFile<LvlSettings>(path, fileName).currentLvl].mapLvl), tilemap, tilemapBase);
    }
    
    
    public void GeneratingAllMap()
    {
        map = new int[width, height];
        map = GenerationMap(startHeight, 6);
        map = GenerateCaves(ChanceToGenerateCave);
        
        lvlSettings.lvl++;
        lvlSettings.listOfMaps.Add(Encode(map));;
        Debug.Log(lvlSettings.lvl);
            
        SaveSystem.SaveFile<LvlSettings>(lvlSettings, path, fileName);
        
        tilemap.ClearAllTiles();
        UpdateMap(map, tilemap, tilemapBase);
    }


    public Vector3 GetExitFromLvl(int[,] _map)
    {
        int j = 0;

        while (_map[_map.GetUpperBound(0) - 1,j] == 1 && _map[_map.GetUpperBound(0) - 1,j + 1] == 1)
        {
            j++;
        }
        
        return tilemap.GetCellCenterLocal(new Vector3Int(_map.GetUpperBound(0) - 1, j, 0));
    }
    
    
    public Vector3 GetStartOfLvl(int[,] _map)
    {
        int j = _map.GetUpperBound(1) - 1;

        while (_map[0,j] == 0 && _map[0,j - 1] == 0)
        {
            j--;
        }
        
        return tilemap.GetCellCenterLocal(new Vector3Int(0, j, 0));
    }

    public Vector3 GetPositionOnMap(int posX, int posY)
    {
        return tilemap.GetCellCenterLocal(new Vector3Int(posX, posY, 0));
    }
    

    private ListMapLvl Encode(int[,] array)
    {
        ListMapLvl  tempList = new ListMapLvl();
        tempList.mapLvl = new List<SecondList>();

        for (int i = 0; i < array.GetUpperBound(0); i++)
        {
            SecondList tmp = new SecondList();
            tmp.mapList = new List<int>();
                
            for (int j = 0; j < array.GetUpperBound(1); j++)
            {
                tmp.mapList.Add(array[i, j]);

            }
                
            tempList.mapLvl.Add(tmp);
        }

        return tempList;
    }
    
    public int[,] Decode( List<SecondList> tempList)
    {
        int[,] temp = new int[ tempList.Count, tempList[0].mapList.Count];
        
        for (int i = 0; i < temp.GetUpperBound(0); i++)
        {
            for (int j = 0; j < temp.GetUpperBound(1); j++)
            {
                temp[i,j] = tempList[i].mapList[j];
            }
        }
        
        return temp;
    }
    
    private void CheckDirrectory()
    {
        if (string.IsNullOrEmpty(path))
            path = Path.Combine(Application.dataPath, "Json");
            
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
    }
    
    
    private void GetLvlSettings()
    {
        CheckDirrectory();

        if (!File.Exists($"{path}/{fileName}"))
        {
            lvlSettings = new LvlSettings();
            return;
        }

        lvlSettings =  SaveSystem.LoadFile<LvlSettings>(path, fileName);
    }
    
    public void UpdateMap(int[,] map, Tilemap tilemap, TileBase tile)
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


    public int[,] GenerateCaves(float _chanceToGenerateCave)
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
        int countOfStepsLeft = 0;
        int countOfStepsRight = 0;
        bool stepLeft = true, stepRight = true;
        
        int caveX = Random.Range(1, map.GetUpperBound(0) - 1);
        int caveY = Random.Range(1, startHeight);

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

        if ((size - 1) % 2 == 0 )
        {
            caveX -= size - 1;
        }
        else
        {
            caveX += size - 1;
        }

        while (countOfSteps < reqCaveAmount)
        {
            
            size = Random.Range(2, 3);
            
            
            int[] dirNum = {-1, 1,-1, 1}; 
            direction = dirNum[Random.Range(0,3)];

            if (direction == -1)
             {
                 if (countOfSteps == 0)
                 {
                     countOfStepsLeft++;
                 }
                 else if ( countOfStepsRight == 0 )
                 {
                     countOfStepsLeft++;
                 }
                 else if (countOfStepsRight >= 2)
                 {
                     chanceToChangeDir = Random.Range(0f, 1f);
                     if (chanceToChangeDir < 0.9f)
                     {
                         countOfStepsRight = 0;
                         countOfStepsLeft++;
                     }
                     else
                     {
                         continue;
                     }
                 }
                 else
                 {
                     continue;
                 }
             }
             else
             {
                 if (countOfSteps == 0)
                 {
                     countOfStepsRight++;
                 }
                 else if ( countOfStepsLeft == 0)
                 {
                     countOfStepsRight++;
                 }
                 else if (countOfStepsLeft >= 2)
                 {
                     chanceToChangeDir = Random.Range(0f, 1f);
                     if (chanceToChangeDir < 0.9f)
                     {
                         countOfStepsLeft = 0;
                         countOfStepsRight++;
                     }
                     else
                     {
                         continue;
                     }
                 }
                 else
                 {
                     continue;
                 }
             }

            map[caveX, caveY - 1] = 0;
            
            if (caveX + size - 1 > map.GetUpperBound(1) - 1 || caveX - size - 1 < 2 )
            {
                break;
            }
            
            for (int i = 1; i < size; i++)
            {
                map[caveX + (i * direction), caveY - 1] = 0;
            }

            if (direction == -1)
            {
                caveX -= size - 1;
            }
            else
            {
                caveX += size - 1;
            }
            caveY -= 1;
            
            if (caveY - 1 < 6)
            {
                break;
            }
            
            countOfSteps++;
            
        }
        return map;
    }
    
}

[Serializable]
public class LvlSettings
{
    public int lvl;
    public int currentLvl = 1;
    public List<ListMapLvl> listOfMaps;
    //public List<SecondList> mapLvl;
    
}

[Serializable]
public class ListMapLvl
{
    public List<SecondList> mapLvl;
}


[Serializable]
public class SecondList
{
    public List<int> mapList;
}
