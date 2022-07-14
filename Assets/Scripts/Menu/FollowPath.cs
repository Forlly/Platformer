using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public MovementPath path;
    public float speed = 1f;
    private IEnumerator<Transform> pointInPath;

    private void Start()
    {
        if (path == null)
            return;

        pointInPath = path.GetNextPathPoint();
        pointInPath.MoveNext();

        if (pointInPath.Current == null)
            return;

        transform.position = pointInPath.Current.position;
        
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (pointInPath != null && pointInPath.Current != null)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = pointInPath.Current.position;
        
            float progress = 0f;
            float distance = Vector3.Distance(startPos, endPos);
            float step = Time.fixedDeltaTime * (speed / distance);
        
            while (progress < 1f)
            {
                transform.position = Vector3.Lerp(startPos, endPos, progress);
                yield return new WaitForFixedUpdate();
                progress += step;
            }
        
            pointInPath.MoveNext();
        }
    }
}
