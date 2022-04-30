using System.Collections;
using UnityEngine;
/// <summary>
/// \brief Класс котролирующий движение камеры за персонажем
/// </summary>
public class CameraController : MonoBehaviour
{
    /// <summary>
    /// \param active Активация следования камеры за персонажем
    /// </summary>
    private bool active;
    public bool Active
    {
        set
        {
            active = value;
            if (active && FindPlayer())
                StartCoroutine(CameraFollow());
        }
        get => active;
    }
    /// <summary>
    /// \param smoothing Плавность следования камеры
    /// \param offset Вектор смещения камеры относительно персонажа
    /// </summary>
    [SerializeField] private float smoothing = 2f;
    [SerializeField] private Vector2 offset = new Vector2(5f, 1f);
    private Transform playerTransform;
    private float lastX;
    private float lastY;
/// <summary>
/// \brief Метод поиска персонажа на сцене
/// </summary>
/// <returns></returns>
    private bool FindPlayer()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, -10f);
        return playerTransform != null;
    }
/// <summary>
/// \brief Метод следования камеры за персонажем
/// </summary>
/// <returns></returns>
    private IEnumerator CameraFollow()
    {
        lastX = playerTransform.position.x;
        lastY = playerTransform.position.y;
        Debug.Log(playerTransform.position);
        
        while (active)
        {
            float currentX = playerTransform.position.x;
            float currentY = playerTransform.position.y;
            Vector3 target;
            if (currentX > lastX) 
                target = new Vector3(playerTransform.position.x + offset.x, playerTransform.position.y , -10f);
            else if (currentX < lastX)
                target = new Vector3(playerTransform.position.x - offset.x, playerTransform.position.y , -10f);
            if (currentY > lastY)
                target = new Vector3(playerTransform.position.x, playerTransform.position.y + offset.y, -10f);
            else if (currentY < lastY)
                target = new Vector3(playerTransform.position.x, playerTransform.position.y - offset.y, -10f);
            else target = transform.position;
            lastX = currentX;
            lastY = currentY;
            transform.position = Vector3.Lerp(transform.position, target, smoothing * Time.deltaTime);
            
            yield return null;
        }
    }
}
