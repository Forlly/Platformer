using System.Collections;
using System.IO;
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
    [SerializeField] private GenerateItemsOnMap generateItemsOnMap;
    [SerializeField] private TransitionBetweenLevels endOfLvl;
    [SerializeField] private SettingsSystem settingsSystem;
    
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
                procedureGeneration.lvlSettings.listOfMaps[SaveSystem.LoadFile<LvlSettings>(Path.Combine(Application.dataPath, "Json"),
                    "LvlSettings.json").lvl].mapLvl));
            Instantiate(endOfLvl, posEndOfLvl, Quaternion.identity);
            endOfLvl.MoveSpawnPointOnNewPoint(SaveSystem.LoadFile<LvlSettings>(Path.Combine(Application.dataPath, "Json"),
                "LvlSettings.json").currentLvl - 1);
        }
        else
        {
            _spawner.GenerateMainCharacterWithCheackPoint();
            yield return new WaitForSeconds(0.1f);
            _cameraController.Active = true;
            Vector3 posEndOfLvl =  procedureGeneration.GetExitFromLvl(procedureGeneration.Decode(
                procedureGeneration.lvlSettings.listOfMaps[SaveSystem.LoadFile<LvlSettings>(Path.Combine(Application.dataPath, "Json"),
                    "LvlSettings.json").currentLvl - 1].mapLvl));
            Instantiate(endOfLvl, posEndOfLvl, Quaternion.identity);
            endOfLvl.MoveSpawnPointOnNewPoint(SaveSystem.LoadFile<LvlSettings>(Path.Combine(Application.dataPath, "Json"),
                "LvlSettings.json").currentLvl - 1);
            generateItemsOnMap.GenerateItems(procedureGeneration.Decode(
                procedureGeneration.lvlSettings.listOfMaps[SaveSystem.LoadFile<LvlSettings>(Path.Combine(Application.dataPath, "Json"),
                    "LvlSettings.json").currentLvl - 1].mapLvl));
        }
    }
}
