using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    private string path;
    [SerializeField] private string saveFileName = "Progress";
    [SerializeField] private ScoreView scoreView;
    private ProgressPlayer progressPlayer;

    public ProgressPlayer ProgressPlayer
    {
        get => progressPlayer;
        set
        {
            progressPlayer = value;
            scoreView.UpdateScore(progressPlayer.score);
        }
    }

    private void Start()
    {
        path = Path.Combine(Application.dataPath, "Json");
        GetProgress();
        scoreView.UpdateScore(progressPlayer.score);
    }
    private void GetProgress()
    {
        CheckDirrectory();

        if (!File.Exists($"{path}/{saveFileName}"))
        {
            progressPlayer = new ProgressPlayer();
            return;
        }

        progressPlayer = SaveSystem.LoadFile<ProgressPlayer>(path, saveFileName);
    }
    private void CheckDirrectory()
    {
        if (string.IsNullOrEmpty(path))
            path = Path.Combine(Application.dataPath, "Json");
            
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
    }

    public void AddPoints(int points)
    {
        progressPlayer.score += points;
        scoreView.UpdateScore(progressPlayer.score);
        SaveSystem.SaveFile(progressPlayer,path,saveFileName);
    }

    public void DeductPoints(int points)
    {
        progressPlayer.score -= points;
        scoreView.UpdateScore(progressPlayer.score);
        SaveSystem.SaveFile(progressPlayer,path,saveFileName);
    }
    
}
[Serializable]
public class ProgressPlayer
{
    public int score = 0;
}
