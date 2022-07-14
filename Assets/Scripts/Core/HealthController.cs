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


    public void UpdateTotalHealthbar(int currentHealth, int startingHP)
    { 
        totalHealthbar.fillAmount = currentHealth/startingHP;
    }

    public void UpdateCurrentHealthbar(int currentHealth, int startingHP)
    { 
        currentHealthbar.fillAmount = (float)currentHealth/startingHP;
    }
    
}
