using Unity.Mathematics;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject Player;

    public void GenerateMainCharacter()
    {
        Instantiate(Player, transform.position,quaternion.identity);
    }
    
    public void GenerateMainCharacterWithCheackPoint()
    {
        Vector2 playerPos =
            new Vector2(PlayerPrefs.GetFloat("xPlayerPosition"), PlayerPrefs.GetFloat("yPlayerPosition"));
        Instantiate(Player, playerPos,quaternion.identity);
        Debug.Log(playerPos);

    }
    
}
