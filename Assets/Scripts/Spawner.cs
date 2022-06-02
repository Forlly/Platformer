using UnityEngine;
/// <summary>
/// \brief Класс генерирующий персонажа в определенном месте
/// </summary>
public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private LinkStore linkStore;
    /// <summary>
    /// \brief Метод генерации персонажа
    /// </summary>
    public void GenerateMainCharacter()
    {   
        linkStore.playerLink 
            = Instantiate(Player, transform.position, Quaternion.identity).GetComponentInChildren<PlayerController>();
    }
    /// <summary>
    /// \brief Метод генерации персонажа в чекпоинте
    /// </summary>
    public void GenerateMainCharacterWithCheackPoint()
    {
        Vector2 playerPos =
            new Vector2(PlayerPrefs.GetFloat("xPlayerPosition"), PlayerPrefs.GetFloat("yPlayerPosition"));
        Player.GetComponentInChildren<PlayerController>().linkStore = linkStore;
        linkStore.playerLink 
            = Instantiate(Player, playerPos,Quaternion.identity).GetComponentInChildren<PlayerController>();
        

    }
    
}
