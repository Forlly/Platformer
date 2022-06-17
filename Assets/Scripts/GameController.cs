using System.Collections;
using UnityEngine;
/// <summary>
/// \brief Класс контроллирующий логику игры
/// </summary>
public class GameController : MonoBehaviour
{
    /// <summary>
    /// \param _spawner Объект класса Spawner
    /// \param _cameraController Объект класса CameraController
    /// </summary>
    [SerializeField] private Spawner _spawner;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private ProcedureGeneration procedureGeneration;
    [SerializeField] private GameObject endOfLvl;
    
    void Awake()
    {
        Application.targetFrameRate = 144;
        StartCoroutine(StartSimulation());
    }
/// <summary>
/// \brief Метод запуска симуляции, генерация персонажа и установление слежения камеры за ним
/// </summary>
/// <returns></returns>
    private IEnumerator StartSimulation()
    {
        if (PlayerPrefs.GetInt("PlayerPosition") == 0)
        {
            _spawner.GenerateMainCharacter();
            yield return new WaitForSeconds(1);
            _cameraController.Active = true;
            Vector3 posEndOfLvl =  procedureGeneration.GetExitFromLvl(procedureGeneration.Decode(
                procedureGeneration.lvlSettings.listOfMaps[procedureGeneration.lvlSettings.lvl - 1].mapLvl));
            Instantiate(endOfLvl, posEndOfLvl, Quaternion.identity);
        }
        else
        {
            _spawner.GenerateMainCharacterWithCheackPoint();
            yield return new WaitForSeconds(0.1f);
            _cameraController.Active = true;
            Vector3 posEndOfLvl =  procedureGeneration.GetExitFromLvl(procedureGeneration.Decode(
                procedureGeneration.lvlSettings.listOfMaps[procedureGeneration.lvlSettings.lvl - 1].mapLvl));
            Instantiate(endOfLvl, posEndOfLvl, Quaternion.identity);
        }
    }
}
