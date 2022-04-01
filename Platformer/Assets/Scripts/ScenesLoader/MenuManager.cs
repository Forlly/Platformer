using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private Ray ray;
    private RaycastHit2D hit;
    private Vector2 mousPos;

    private void OnMouseDown()
    {
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
    }

    private void StartPlay()
    {
        SceneManager.LoadScene("lvl1");
    }
    private void SettingsLoad()
    {
        SceneManager.LoadScene("Settings");
    }
    private void Exit()
    {
        Debug.Log("exit");
        Application.Quit();
    }
}

