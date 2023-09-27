using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryLine : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private LineRenderer lr;
    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }
    public void RenderLine(Vector2 startPoint, Vector2 endPoint)
    {
        lr.positionCount = 2;
        Vector3[] points = new Vector3[2];
        points[0] = startPoint;
        points[1] = endPoint;
        lr.SetPositions(points);
    }
}

