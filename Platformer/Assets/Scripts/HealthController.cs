using UnityEngine.UI;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] public Image totalHealthbar;
    [SerializeField] public Image currentHealthbar;

    public void UpdateTotalHealthbar(int currentHealth)
    { 
        totalHealthbar.fillAmount = currentHealth/5;
    }
    public void UpdateCurrentHealthbar(int currentHealth)
    { 
        currentHealthbar.fillAmount = (float)currentHealth/5;
        Debug.Log(currentHealthbar.fillAmount);
    }
    
}
