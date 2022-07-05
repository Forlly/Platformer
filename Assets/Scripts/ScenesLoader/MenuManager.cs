using System;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private Ray ray;
    private RaycastHit2D hit;
    private Vector2 mousPos;
    [SerializeField] public Button startButton;
    [SerializeField] public Button settingsButton;
    [SerializeField] public Button selectLevelButton;
    [SerializeField] public Button exitButton;

    [SerializeField] private GameObject menuLevels;
    private void Start()
    {
        startButton.onClick.AddListener(StartPlay);
        
        if (!File.Exists($"{Path.Combine(Application.dataPath, "Json")}/LvlSettings.json"))
        {
            selectLevelButton.interactable = false;
        }
        else
        {
            selectLevelButton.interactable = true;
            selectLevelButton.onClick.AddListener(OpenMenuLevels);
        }
        
        exitButton.onClick.AddListener(Exit);
    }
    

    private void OpenMenuLevels()
    {
        menuLevels.SetActive(true);
        gameObject.SetActive(false);
    }
    private void StartPlay()
    {
        SceneTransition.instance.SwitchToScene("lvl1");
    }

    private void Exit()
    {
        Debug.Log("exit");
        Application.Quit();
    }
}

