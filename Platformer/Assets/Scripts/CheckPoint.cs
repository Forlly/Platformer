using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private bool activated = false;
    
    /*private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !activated)
        {
            Debug.Log(col.tag);
            PlayerPrefs.SetInt("PlayerPosition", 1);
            PlayerPrefs.SetFloat("xPlayerPosition", transform.position.x);
            PlayerPrefs.SetFloat("yPlayerPosition", transform.position.y);
            activated = true;
        }
    }*/

    public void SetPlayerPosition()
    {
        if(!activated)
        {
            Debug.Log("col.tag");
            PlayerPrefs.SetInt("PlayerPosition", 1);
            PlayerPrefs.SetFloat("xPlayerPosition", transform.position.x);
            PlayerPrefs.SetFloat("yPlayerPosition", transform.position.y);
            activated = true;
        }
    }

    public void ResetPlayerPosition()
    {
        
        PlayerPrefs.SetInt("PlayerPosition", 0);
    }
    
}
