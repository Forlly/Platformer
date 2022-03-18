using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject topEnemyPos;

    public Vector2 GetTopPos()
    {
        return topEnemyPos.transform.position;
    }
    public void GetDamageSpike()
    {
        PlayerController.Instance.currentHealth--;
        Debug.Log(PlayerController.Instance.currentHealth);
        if (PlayerController.Instance.currentHealth<=0)
            Destroy(PlayerController.Instance.gameObject);
    }
    
    public void GetDamageEnemy()
    {
        PlayerController.Instance.currentHealth -= 2 ;
        Debug.Log(PlayerController.Instance.currentHealth);
        if (PlayerController.Instance.currentHealth<=0)
            Destroy(PlayerController.Instance.gameObject);
    }
}
