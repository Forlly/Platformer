using Unity.Mathematics;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject Player;

    public void GenerateMainCharacter()
    {
        Instantiate(Player, transform.position,quaternion.identity);
    }
}
