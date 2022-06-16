using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionBetweenLevels : MonoBehaviour
{
    [SerializeField] private ProcedureGeneration procedureGeneration;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            GoToNextLevel();

    }

    private void GoToNextLevel()
    {
        ProcedureGeneration.Instans.GeneratingAllMap();
    }
}
