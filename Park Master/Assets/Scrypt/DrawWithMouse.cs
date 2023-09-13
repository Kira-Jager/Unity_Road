using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWithMouse : MonoBehaviour
{
    public GameObject plane;
    public GameObject car;
    public GameObject finishPlane;

    public float lineDistance = 0.2f;

    private LineRenderer lineRenderer;
    private bool isDrawing = false;
    private bool collidedWithCar = false;
    private bool collidedWithGround = false;

    private Vector3 mousePosScreen; 
    private List<Vector3> linePoints = new List<Vector3>();

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (collidedWithCar || CheckTheHitRay("car") && CheckTheHitRay("ground"))
            {
                StartDrawing();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopDrawing();

        }

       /* if (isDrawing)
        {
            UpdateLine();
        }*/
    }

    private bool CheckTheHitRay(string onCarOrGround)
    {
         mousePosScreen = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(mousePosScreen);

        //  get information about what the ray hit, like the object's position, etc.
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (onCarOrGround == "car" && hitInfo.transform.CompareTag("car"))
            {
                collidedWithCar = true;
                Debug.Log("Collision occured with car");
                return true;
            }
            if (onCarOrGround == "ground" && hitInfo.transform.CompareTag("ground"))
            {
                collidedWithGround = true;
                Debug.Log("Collision occurred with ground");
                return true;
            }
            
        }
        return false;
    }

    private void StartDrawing()
    {
        /*
            Check if the plane is the one give as input and then assure to set point only on collision with this one 
            Add the first point on screnn on the plan if the first object hit is the car passed as game object
        */

        Debug.Log("In StartDrawings");


        if (collidedWithCar)
        {
            isDrawing = true;
            mousePosScreen = Input.mousePosition;
            Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePosScreen);

            if (CheckTheHitRay("ground"))
            {
                linePoints.Add(mousePosWorld);
                lineRenderer.SetPosition(lineRenderer.positionCount -1, mousePosWorld);

                Debug.Log("point added");
            }


           
        }
    }

    private void StopDrawing()
    {
         //If button is released then stop drawing 
        //Debug.Log("In StopDrawing");


    }

    private void UpdateLine()
    {
        //Debug.Log("In Update line");


        //Continue to add point in a list of point to make path from the start point to the final line or take the line that have been set previously and then add new line to the existant one
        if (collidedWithGround)
        {
            mousePosScreen = Input.mousePosition;
            Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePosScreen);
            mousePosWorld.z = 0f;
            // Check if the distance between the last point and the current mouse position exceeds lineDistance
            if (Vector3.Distance(linePoints[linePoints.Count - 1], mousePosWorld) >= lineDistance)
            {
                linePoints.Add(mousePosWorld);
                lineRenderer.positionCount = linePoints.Count;
                lineRenderer.SetPositions(linePoints.ToArray());
            }
        }
    }


}
