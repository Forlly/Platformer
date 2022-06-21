using System;
using System.Collections;
using System.Collections.Generic;
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
        ProcedureGeneration.Instans.GeneratingAllMap();
        MoveSpawnPointOnNewPoint();
        
        GenerateItemsOnMap.Instants.GenerateItems(ProcedureGeneration.Instans.Decode(
            ProcedureGeneration.Instans.lvlSettings.listOfMaps[ProcedureGeneration.Instans.lvlSettings.lvl - 1].mapLvl),
            ProcedureGeneration.Instans.Decode(
                ProcedureGeneration.Instans.lvlSettings.listOfMaps[ProcedureGeneration.Instans.lvlSettings.lvl - 2].mapLvl));

        transform.position = ProcedureGeneration.Instans.GetExitFromLvl(ProcedureGeneration.Instans.Decode(
            ProcedureGeneration.Instans.lvlSettings.listOfMaps[ProcedureGeneration.Instans.lvlSettings.lvl - 1].mapLvl));
        isTrigger = false;
    }

    public void MoveSpawnPointOnNewPoint()
    {
        Vector3 posStartOfLvlOfLvl =  ProcedureGeneration.Instans.GetStartOfLvl(ProcedureGeneration.Instans.Decode(
            ProcedureGeneration.Instans.lvlSettings.listOfMaps[ProcedureGeneration.Instans.lvlSettings.lvl - 1].mapLvl));
        LinkStore.Instans.spawner.transform.position = posStartOfLvlOfLvl;
        LinkStore.Instans.spawner.MovePlayerPosToNewSpawnPoint();
    }
}
