using System;
using System.IO;
using UnityEngine;

public class TransitionBetweenLevels : MonoBehaviour
{
    private bool isTrigger = false;

     private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !isTrigger)
        {
            isTrigger = true;
            GoToNextLevel();
        }

    }

    private void GoToNextLevel()
    {

        if (SaveSystem.LoadFile<LvlSettings>(Path.Combine(Application.dataPath, "Json"), "LvlSettings.json").currentLvl 
            >= SaveSystem.LoadFile<LvlSettings>(Path.Combine(Application.dataPath, "Json"), "LvlSettings.json").lvl - 1)
        {
            ProcedureGeneration.Instans.GeneratingAllMap();
            MoveSpawnPointOnNewPoint(ProcedureGeneration.Instans.lvlSettings.lvl - 1);
        
            GenerateItemsOnMap.Instants.GenerateItems(ProcedureGeneration.Instans.Decode(
                    ProcedureGeneration.Instans.lvlSettings.listOfMaps[ProcedureGeneration.Instans.lvlSettings.lvl - 1].mapLvl),
                ProcedureGeneration.Instans.Decode(
                    ProcedureGeneration.Instans.lvlSettings.listOfMaps[ProcedureGeneration.Instans.lvlSettings.lvl - 2].mapLvl));

            transform.position = ProcedureGeneration.Instans.GetExitFromLvl(ProcedureGeneration.Instans.Decode(
                ProcedureGeneration.Instans.lvlSettings.listOfMaps[ProcedureGeneration.Instans.lvlSettings.lvl - 1].mapLvl));
        }
        else
        {
            MoveSpawnPointOnNewPoint(SaveSystem
                .LoadFile<LvlSettings>(Path.Combine(Application.dataPath, "Json"), "LvlSettings.json").currentLvl - 1);
            
            LvlSettings lvlSettings =
                SaveSystem.LoadFile<LvlSettings>(Path.Combine(Application.dataPath, "Json"), "LvlSettings.json");
            lvlSettings.currentLvl++;
            
            SaveSystem.SaveFile<LvlSettings>(lvlSettings,Path.Combine(Application.dataPath, "Json"), "LvlSettings.json");

            ProcedureGeneration.Instans.UpdateMap(ProcedureGeneration.Instans.Decode(
                ProcedureGeneration.Instans.lvlSettings.listOfMaps[lvlSettings.currentLvl - 1].mapLvl), 
                ProcedureGeneration.Instans.tilemap, ProcedureGeneration.Instans.tilemapBase);
            
            GenerateItemsOnMap.Instants.GenerateItems(ProcedureGeneration.Instans.Decode(
                    ProcedureGeneration.Instans.lvlSettings.listOfMaps[lvlSettings.currentLvl - 1].mapLvl),
                ProcedureGeneration.Instans.Decode(
                    ProcedureGeneration.Instans.lvlSettings.listOfMaps[lvlSettings.currentLvl - 1].mapLvl));

            transform.position = ProcedureGeneration.Instans.GetExitFromLvl(ProcedureGeneration.Instans.Decode(
                ProcedureGeneration.Instans.lvlSettings.listOfMaps[lvlSettings.currentLvl - 1].mapLvl));
            
        }
       
        isTrigger = false;
    }

    public void MoveSpawnPointOnNewPoint(int curlvl)
    {
        Vector3 posStartOfLvlOfLvl =  ProcedureGeneration.Instans.GetStartOfLvl(ProcedureGeneration.Instans.Decode(
            ProcedureGeneration.Instans.lvlSettings.listOfMaps[curlvl].mapLvl));
        LinkStore.Instans.spawner.transform.position = posStartOfLvlOfLvl;
        LinkStore.Instans.spawner.MovePlayerPosToNewSpawnPoint();
        Debug.Log("hii");
    }
}
