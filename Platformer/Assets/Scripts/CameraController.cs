using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
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
    
    [SerializeField] private float smoothing = 2f;
    [SerializeField] private Vector2 offset = new Vector2(5f, 1f);
    private Transform playerTransform;
    private float lastX;
    private float lastY;

    private bool FindPlayer()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        return playerTransform != null;
    }

    private IEnumerator CameraFollow()
    {
        lastX = playerTransform.position.x;
        lastY = playerTransform.position.y;
        
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
