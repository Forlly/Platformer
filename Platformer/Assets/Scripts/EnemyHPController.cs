using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// \brief Класс контроллирующий здоровье противника
/// </summary>
public class EnemyHPController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] currentHP;

    public void UpdateSpriteHP(int _countHP, int _startHP)
    {
        float percentCurrentHp = 100f * _countHP / _startHP;
        float percentSpriteHp = percentCurrentHp * currentHP.Length / 100f;
        int indexSprite = (int)Math.Floor(percentSpriteHp);

        for (int i = 0; i < currentHP.Length; i++)
        {
            currentHP[i].enabled = i < indexSprite;
        }
    }
    
}
