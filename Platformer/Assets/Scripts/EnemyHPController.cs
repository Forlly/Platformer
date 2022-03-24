using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] currentHP;

    public void UpdateSpriteHP(int _countHP, int _startHP)
    {
        int difStartCrnt = _startHP - _countHP;
        for (int i = 0; i < _countHP; i++)
        {
            currentHP[i].enabled = true;
        }
        for (int i = _countHP; i < (_countHP + difStartCrnt); i++)
        {
            currentHP[i].enabled = false;
        }
    }
    
}
