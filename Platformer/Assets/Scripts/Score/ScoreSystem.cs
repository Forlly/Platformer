using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    private string path;
    [SerializeField] private string saveFileName = "Progress";
    private SaveSystem _saveSystem;
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
        _saveSystem = FindObjectOfType<SaveSystem>();
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

        progressPlayer = _saveSystem.LoadFile<ProgressPlayer>(path, saveFileName);
        Debug.Log(progressPlayer.score);
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
        _saveSystem.SaveFile(progressPlayer,path,saveFileName);
        Debug.Log(progressPlayer.score);
    }

    public void DeductPoints(int points)
    {
        progressPlayer.score -= points;
        scoreView.UpdateScore(progressPlayer.score);
        _saveSystem.SaveFile(progressPlayer,path,saveFileName);
        Debug.Log(progressPlayer.score);
    }
    
}
[Serializable]
public class ProgressPlayer
{
    public int score = 0;
}
