using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysController : MonoBehaviour
{
    private EnemyQSpikeController qSpike;
    private Transform playerTransform;
    private Vector2 distance;
    [SerializeField] private Vector2 triggerDistance;

    private void Start()
    {
        qSpike = FindObjectOfType<EnemyQSpikeController>();
        playerTransform = PlayerController.Instance.transform;
    }

    void Update()
    {
        if (qSpike)
        {
            distance = qSpike.CheckDistanceToPlayer(playerTransform.transform);
            if(Mathf.Abs(distance.x) <= Mathf.Abs(triggerDistance.x))
                qSpike.FollowToPlayer(playerTransform.transform);
        }
    }
}
