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
    public void GenerateMainCharacter(Vector3 transformPos = default)
    {
        if (transformPos == default)
            transformPos = transform.position;
        
        Player.GetComponentInChildren<PlayerController>().healthController = linkStore.HealthController;
        linkStore.playerLink 
            = Instantiate(Player, transformPos, Quaternion.identity).GetComponentInChildren<PlayerController>();
        linkStore.playerLink.joystick = linkStore.joystick;
        Debug.Log(linkStore.playerLink.joystick);
    }
    /// <summary>
    /// \brief Метод генерации персонажа в чекпоинте
    /// </summary>
    public void GenerateMainCharacterWithCheackPoint()
    {
        Vector2 playerPos =
            new Vector2(PlayerPrefs.GetFloat("xPlayerPosition"), PlayerPrefs.GetFloat("yPlayerPosition"));

        GenerateMainCharacter(playerPos);
    }

    public void MovePlayerPosToNewSpawnPoint()
    {
        linkStore.playerLink.transform.position = transform.position;
    }
}
