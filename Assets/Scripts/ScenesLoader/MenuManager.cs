using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private Ray ray;
    private RaycastHit2D hit;
    private Vector2 mousPos;
    [SerializeField] Button startButton;
    [SerializeField] Button settingsButton;
    [SerializeField] Button exitButton;
    private void Start()
    {
        startButton.onClick.AddListener(StartPlay);
        settingsButton.onClick.AddListener(SettingsLoad);
        exitButton.onClick.AddListener(Exit);
    }

    /*private void OnMouseDown()
    {
        startButton.onClick.AddListener(StartPlay);
        mousPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ray = new Ray(mousPos, -transform.forward);
        hit = Physics2D.Raycast(mousPos, -transform.forward,10);
        if (!hit.collider)
        {
            return;
        }

        if (hit.collider.CompareTag("StartPlay"))
        {
            StartPlay();
        }
        else if(hit.collider.CompareTag("Settings"))
        {
            SettingsLoad();
        }
        else if(hit.collider.CompareTag("Exit"))
        {
            Exit();
        }
    }*/

    private void StartPlay()
    {
        SceneTransition.instance.SwitchToScene("lvl1");
    }
    private void SettingsLoad()
    {
        SceneTransition.instance.SwitchToScene("Settings");
    }
    private void Exit()
    {
        Debug.Log("exit");
        Application.Quit();
    }
}

