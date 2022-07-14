using Cinemachine;
using UnityEngine;
/// <summary>
/// \brief Класс котролирующий движение камеры за персонажем
/// </summary>
public class CameraController : MonoBehaviour
{
    [SerializeField] public CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private LinkStore linkStore;
    /// <summary>
    /// \param active Активация следования камеры за персонажем
    /// </summary>
    private bool active;
    public bool Active
    {
        set
        {
            active = value;
            FindPlayer();

        }
        get => active;
    }
    /// <summary>
    /// \param smoothing Плавность следования камеры
    /// \param offset Вектор смещения камеры относительно персонажа
    /// </summary>
    private Transform playerTransform;

/// <summary>
/// \brief Метод поиска персонажа на сцене
/// </summary>
/// <returns></returns>
    private bool FindPlayer()
{
        playerTransform = linkStore.playerLink.transform;
        cinemachineVirtualCamera.Follow = playerTransform;
        
        linkStore.camera = this;
        linkStore.cvCamera = cinemachineVirtualCamera;
        
        return playerTransform != null;
    }
}
