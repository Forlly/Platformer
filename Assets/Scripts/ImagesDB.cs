using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ImagesDB", menuName = "ScriptableObjects/ImagesDB")]
public class ImagesDB : ScriptableObject
{
    public List<ImageData> ImageDatas = new List<ImageData>();
}

[Serializable]
public class ImageData
{
    public string id;
    public Sprite sprite;
}

public static class ImageDBHelper
{
    private static readonly ImagesDB imagesDB;
    
    static ImageDBHelper()
    {
        imagesDB = Resources.Load<ImagesDB>("ImagesDB");
    }

    public static Sprite GetSprite(string _id)
    {
        return imagesDB.ImageDatas.FirstOrDefault(data => data.id == _id)?.sprite;
    }
}