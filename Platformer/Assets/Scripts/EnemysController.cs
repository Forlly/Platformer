using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// \brief Класс контролирующий логику поведения проивников 
/// </summary>
public class EnemysController : MonoBehaviour
{
    [SerializeField] private EnemyQSpikeController[] qSpikes;
    private Transform playerTransform;
    private Vector2 distance;
    [SerializeField] private Vector2 triggerDistance;

    private void Start()
    {
        qSpikes = FindObjectsOfType<EnemyQSpikeController>();
        playerTransform = PlayerController.Instance.transform;
    }

    void Update()
    {
        for (int i = 0; i < qSpikes.Length; i++)
        {
            if (qSpikes[i])
            {
                distance = qSpikes[i].CheckDistanceToPlayer(playerTransform.transform);
                if (Mathf.Abs(distance.x) <= Mathf.Abs(triggerDistance.x))
                    qSpikes[i].FollowToPlayer(playerTransform.transform);
            }
        }
    }
}
