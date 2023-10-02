using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    // Start is called before the first frame update

    private Car car;
    private GameManager gameManager;
    
    private LineRenderer lineRenderer;
    private List<Vector3> PointsList = new List<Vector3>();
    private Vector3 lastPoint;

    private LayerMask ground;


    private float distanceBetweenPoints;

    public void Initialize(GameManager manager, Car car)
    {
        gameManager = manager;
        ground = gameManager.groundLayer;
        distanceBetweenPoints = gameManager.distanceBetweenPoints;
        this.car = car;
    }



    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        car = GetComponent<Car>();
        lineRenderer.enabled = false;
        lineRenderer.startWidth = 3;
        lineRenderer.endWidth = 3;
        lineRenderer.positionCount = 0;

        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found!");
            return;
        }

        Initialize(gameManager, car);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            LineControl();
        }

    }

    private void LineControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (car.CheckTheHitRayOnCar())
            {
                lineRenderer.enabled = true;
                lastPoint = GetMouseWorldPosition();
                Debug.Log("line enabled");
            }
        }
        else if (Input.GetMouseButton(0))
        {
           
            if (car.getcarGetSelected())
            {
                Vector3 mousePoint = GetMouseWorldPosition();
                if (Vector3.Distance(lastPoint, mousePoint) > distanceBetweenPoints)
                {
                    lastPoint = mousePoint;
                    lineRenderer.positionCount++;
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, lastPoint);

                    //gameManager.AddPointToCarPath(car.getCarID(), lastPoint);

                    this.PointsList.Add(mousePoint);
                }
                car.setCarHasPath();
            }
        }
    }

    public List<Vector3> getCarPath()
    {
        return PointsList;
        //return gameManager.carPoints[car.getCarID()];
    }

    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ground))
        {
            return hit.point;
        }
        Debug.Log(hit.point);
        return lastPoint;
    }
}
