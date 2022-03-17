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
        PlayerController.Instance.lives--;
        Debug.Log(PlayerController.Instance.lives);
        if (PlayerController.Instance.lives<=0)
            Destroy(PlayerController.Instance.gameObject);
    }
    
    public void GetDamageEnemy()
    {
        PlayerController.Instance.lives -= 2 ;
        Debug.Log(PlayerController.Instance.lives);
        if (PlayerController.Instance.lives<=0)
            Destroy(PlayerController.Instance.gameObject);
    }
}
