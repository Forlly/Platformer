using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// \brief Класс контроллирующий здоровье противника
/// </summary>
public class EnemyHPController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] currentHP;
/// <summary>
/// \brief Метод обновляющий отображение здоровья противника
/// </summary>
/// <param name="_countHP"> Текущее здоровье противника</param>
/// <param name="_startHP"> Изначальное здоровье противника</param>
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
