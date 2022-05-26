using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public MovementPath path;
    public float speed = 1f;
    public float maxDistance = 1f;
    private IEnumerator<Transform> pointInPath;

    private void Start()
    {
        if (path == null)
        {
            return;
        }

        pointInPath = path.GetNextPathPoint();
        pointInPath.MoveNext();

        if (pointInPath.Current == null)
            return;

        transform.position = pointInPath.Current.position;
    }

    private void Update()
    {
        if (pointInPath == null || pointInPath.Current == null)
            return;
        float distanceSqure;
        transform.position =
                Vector3.MoveTowards(transform.position, pointInPath.Current.position, Time.deltaTime * speed);
        distanceSqure = (transform.position - pointInPath.Current.position).sqrMagnitude;
        
        if (distanceSqure < maxDistance * maxDistance)
        {
            pointInPath.MoveNext();
        }
    }
}
