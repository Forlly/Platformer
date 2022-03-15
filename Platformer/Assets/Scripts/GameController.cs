using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private CameraController _cameraController;
    
    void Awake()
    {
        StartCoroutine(StartSimulation());
    }

    private IEnumerator StartSimulation()
    {
        _spawner.GenerateMainCharacter();
        yield return new WaitForSeconds(1);
        _cameraController.Active = true;
    }
}
