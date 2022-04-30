using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// \brief Класс точки сохранения прогресса 
/// </summary>
public class CheckPoint : MonoBehaviour
{
    private bool activated ;
/// <summary>
/// \brief Метод сохранения позиции персонажа
/// </summary>
    public void SetPlayerPosition()
    {
        if(!activated)
        {
            PlayerPrefs.SetInt("PlayerPosition", 1);
            PlayerPrefs.SetFloat("xPlayerPosition", transform.position.x);
            PlayerPrefs.SetFloat("yPlayerPosition", transform.position.y);
            activated = true;
            Destroy(gameObject);
        }
    }

    public void ResetPlayerPosition()
    {
        
        PlayerPrefs.SetInt("PlayerPosition", 0);
    }
    
}
