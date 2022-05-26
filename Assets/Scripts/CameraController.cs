using System.Collections;
using Cinemachine;
using UnityEngine;
/// <summary>
/// \brief Класс котролирующий движение камеры за персонажем
/// </summary>
public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
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
            /*if (active && FindPlayer())
                StartCoroutine(CameraFollow());*/
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
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        cinemachineVirtualCamera = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        cinemachineVirtualCamera.Follow = playerTransform;
        return playerTransform != null;
    }
}