using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWithMouse : MonoBehaviour
{
    public GameObject plane;
    public GameObject car;
    public GameObject finishPlane;

    public float lineDistance = 0.2f;

    private LineRenderer line;
    private bool isDrawing = false;

    private Vector3 lastMousePosWorld;
    Vector3 mousePosScreen;
    Vector3 mousePosWorld;
    private List<Vector3> PointsList = new List<Vector3>();

    private void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDrawing();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopDrawing();
        }
        if (Input.GetMouseButton(0))
        {
            UpdateLine();
        }
    }

    private bool CheckTheHitRayOnCar()
    {
        mousePosScreen = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(mousePosScreen);

        //  get information about what the ray hit, like the object's position, etc.
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.transform.CompareTag("car"))
            {
                //collidedWithCar = true;
                Debug.Log("Collision occured with car");
                return true;
            }
        }
        return false;
    }

    private void StartDrawing()
    {
        isDrawing = true;

        mousePosScreen = Input.mousePosition;
        mousePosWorld = Camera.main.ScreenToWorldPoint(mousePosScreen);

        //add the points to the list
        PointsList.Add(mousePosWorld);
        line.positionCount = PointsList.Count;


        line.SetPosition(PointsList.Count - 1, PointsList[PointsList.Count - 1]);
    }

    private void StopDrawing()
    {
        isDrawing = false;
    }

    private void UpdateLine()
    {
        if (isDrawing)
        {
            Vector3 mousePosScreen = Input.mousePosition;
            Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePosScreen);

            float distance = Vector3.Distance(lastMousePosWorld, mousePosWorld);

            if (distance >= lineDistance)
            {
                PointsList.Add(mousePosWorld);
                line.positionCount = PointsList.Count;
                line.SetPosition(PointsList.Count - 1, mousePosWorld);

                lastMousePosWorld = mousePosWorld;

                Debug.Log("Point added");
            }
        }
    }
}
