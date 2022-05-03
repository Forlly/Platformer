using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private int score;
    private string path;
    [SerializeField] private string saveFileName = "Progress";
    private SaveSystem _saveSystem;
    [SerializeField] private ScoreView scoreView;

    private void Start()
    {
        path = Path.Combine(Application.dataPath, "Json");
        _saveSystem = FindObjectOfType<SaveSystem>();
    }

    public void AddPoints(int points)
    {
        score += points;
        scoreView.UpdateScore(score);
        _saveSystem.SaveFile(score,path,saveFileName);
    }

    public void DeductPoints(int points)
    {
        score -= points;
        scoreView.UpdateScore(score);
        _saveSystem.SaveFile(score,path,saveFileName);
    }

    public int GetScore()
    {
        return score;
    }
}
