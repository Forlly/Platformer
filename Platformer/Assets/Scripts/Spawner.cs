using Unity.Mathematics;
using UnityEngine;
/// <summary>
/// \brief Класс генерирующий персонажа в определенном месте
/// </summary>
public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    /// <summary>
    /// \brief Метод генерации персонажа
    /// </summary>
    public void GenerateMainCharacter()
    {
        Instantiate(Player, transform.position,quaternion.identity);
    }
    /// <summary>
    /// \brief Метод генерации персонажа в чекпоинте
    /// </summary>
    public void GenerateMainCharacterWithCheackPoint()
    {
        Vector2 playerPos =
            new Vector2(PlayerPrefs.GetFloat("xPlayerPosition"), PlayerPrefs.GetFloat("yPlayerPosition"));
        Instantiate(Player, playerPos,quaternion.identity);
    }
    
}
