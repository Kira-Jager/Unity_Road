using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Draw : MonoBehaviour
{
    public float distanceBetweenPoints = 0.1f;
    public float lineWidth = 2f;
    public LayerMask groundLayer;


    private LineRenderer lineRenderer;
    private Vector3 lastPoint;
    private List<Vector3> PointsList = new List<Vector3>();

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastPoint = GetMouseWorldPosition();
            lineRenderer.positionCount = 1;
            lineRenderer.SetPosition(0, lastPoint);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 mousePoint = GetMouseWorldPosition();
            if (Vector3.Distance(lastPoint, mousePoint) > distanceBetweenPoints)
            {
                lastPoint = mousePoint;
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, lastPoint);
                PointsList.Add(mousePoint);

            }
            Debug.Log(PointsList.ToArray());
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        /*if (plane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance);
        }*/

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
            return hit.point;
        }
        return lastPoint;
    }
}
