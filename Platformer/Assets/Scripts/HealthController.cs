using UnityEngine.UI;
using UnityEngine;

public class HealthController : MonoBehaviour
{
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
