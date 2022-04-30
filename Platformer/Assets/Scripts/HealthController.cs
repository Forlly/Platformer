using UnityEngine.UI;
using UnityEngine;
/// <summary>
/// \brief Класс контроллирующий здоровье персонажа
/// </summary>
public class HealthController : MonoBehaviour
{
    /// <summary>
    /// \param totalHealthbar Стартовое здоровье 
    /// \param currentHealthbar Текущее здоровье
    /// </summary>
    [SerializeField] public Image totalHealthbar;
    [SerializeField] public Image currentHealthbar;

    /// <summary>
    /// \brief Метод обновления стартового здоровья персонажа
    /// </summary>
    /// <param name="currentHealth">Текущее здоровье</param>
    /// <param name="startingHP">Стартовое здоровье</param>
    public void UpdateTotalHealthbar(int currentHealth, int startingHP)
    { 
        totalHealthbar.fillAmount = currentHealth/startingHP;
    }
    /// <summary>
    /// \brief Метод обновления текущего здоровья персонажа
    /// </summary>
    /// <param name="currentHealth">Текущее здоровье</param>
    /// <param name="startingHP">Стартовое здоровье</param>
    public void UpdateCurrentHealthbar(int currentHealth, int startingHP)
    { 
        currentHealthbar.fillAmount = (float)currentHealth/startingHP;
    }
    
}
