using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MultipleDraw : MonoBehaviour
{
    public float distanceBetweenPoints = 0.1f;
    public LayerMask groundLayer;
    public float speed = 0.1f;
    public float rotationSpeed = 1.0f;

    public GameObject[] car;

    private LineRenderer[] lineRenderer;
    private Vector3 lastPoint;
    private List<Vector3>[] PointsList; //= new List<Vector3>();

    private int carIndex = 0;

    private bool collidedWithCar = false;
    private bool collisionBtwCars = false;
    private bool[] carMoving;
    private bool[] carHasPath;

    //private bool existPreviousList = false;

    void Start()
    {
        lineRenderer = new LineRenderer[car.Length];
        PointsList = new List<Vector3>[car.Length];
        carHasPath = new bool[car.Length];
        carMoving = new bool[car.Length];

        //jut to get rid of warning
        collisionBtwCars = true;

        for (int i = 0; i < car.Length; i++)
        {
            lineRenderer[i] = car[i].GetComponent<LineRenderer>();
            lineRenderer[i].enabled = false;
            lineRenderer[i].startWidth = 3;
            lineRenderer[i].endWidth = 3;

            lineRenderer[i].positionCount = 0;
            PointsList[i] = new List<Vector3>();
            carHasPath[i] = false;
            carMoving[i] = false;
        }
    }

    public void setCollisionBtwCars()
    {
        this.collisionBtwCars = true;
    }

    public bool getCollidedWithCar()
    {
        return collidedWithCar;
    }
    public int getCarIndex()
    {
        return carIndex;
    }

    void Update()
    {
        if (Time.timeScale != 0)
        {
            lineUpdate();
        }

    }

    private void lineUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            carIndex = CheckTheHitRayOnCar();
            if (collidedWithCar && carIndex != -1)
            {
                lineRenderer[carIndex].enabled = true;
                lastPoint = GetMouseWorldPosition();
                //lineRenderer.positionCount = 1;
                //lineRenderer.SetPosition(0, lastPoint);
            }
        }

        else if (Input.GetMouseButton(0))
        {
            if (collidedWithCar && carIndex != -1)
            {

                Vector3 mousePoint = GetMouseWorldPosition();
                if (Vector3.Distance(lastPoint, mousePoint) > distanceBetweenPoints)
                {
                    lastPoint = mousePoint;
                    lineRenderer[carIndex].positionCount++;
                    lineRenderer[carIndex].SetPosition(lineRenderer[carIndex].positionCount - 1, lastPoint);
                    PointsList[carIndex].Add(mousePoint);
                }

                carHasPath[carIndex] = true;
            }
        }


        if (Input.GetMouseButtonUp(0))
        {
            collidedWithCar = false;
            //MoveCarTowardPath();

            if (carIndex != -1)
            {
                for (int i = 0; i < car.Length; i++)
                {
                    if (carHasPath[i])
                    {
                        car[i].gameObject.transform.position = PointsList[i][0];
                        StartCoroutine(MoveCarTowardPath(i));
                        if(collidedWithCar == false)
                        {
                            carMoving[i] = false;
                        }
                        else
                        {
                            carMoving[i] = true;
                        }
                    }

                }
            }

        }


    }

    /*    private void MoveCarTowardPath()
        {
           for(int i =0; i<PointsList.Count; i++)
            {
                car.transform.position = PointsList[i];
                //car.transform.position = Vector3.Lerp(PointsList[i], PointsList[i + 1] , 0.2f * Time.deltaTime);

            }
        }*/

    private IEnumerator MoveCarTowardPath(int carIndex)
    {

        Debug.Log("Car index = " + carIndex);

        carMoving[carIndex] = true;

        for (int i = 0; i < PointsList[carIndex].Count - 1; i++)
        {

            if (carIndex != -1)
            {

                float distance = Vector3.Distance(PointsList[carIndex][i], PointsList[carIndex][i + 1]);

                float Duration = distance / speed;

                float startTime = Time.time;

                //Debug.Log("Start time " + startTime);

                float moveSpeed = Time.deltaTime * speed;


                //car rotation at point i and i + 1


                while (Time.time - startTime < Duration)
                {
                    float Progress = (Time.time - startTime) / Duration;

                    car[carIndex].transform.position = Vector3.Lerp(PointsList[carIndex][i], PointsList[carIndex][i + 1], Progress);

                    Vector3 directionToNextPoint = PointsList[carIndex][i + 1] - car[carIndex].transform.position;

                    if (directionToNextPoint != Vector3.zero)
                    {
                        //car[carIndex].transform.rotation = Quaternion.LookRotation(directionToNextPoint);

                        Quaternion targetRotation = Quaternion.LookRotation(directionToNextPoint);
                        car[carIndex].transform.rotation = Quaternion.Slerp(car[carIndex].transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                    }

                    //car[carIndex].transform.position =  Vector3.MoveTowards(PointsList[carIndex][i], PointsList[carIndex][i +1], moveSpeed);
                    //car[carIndex].transform.position = Vector3.Lerp(PointsList[carIndex][i], PointsList[carIndex][i + 1], moveSpeed);
                    yield return null;
                }

                car[carIndex].transform.position = PointsList[carIndex][i + 1];

            }

        }

        carMoving[carIndex] = false;
        
    }


    Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
            return hit.point;
        }
        return lastPoint;
    }


    private int CheckTheHitRayOnCar()
    {

        for (int i = 0; i < car.Length; i++)
        {

            if (carMoving[i])
            {
                return -1;
            }
            else
            {
                RaycastHit hitInfo;
                Vector3 mousePosScreen = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(mousePosScreen);

                // Use Physics.Raycast to check if the ray hits the current car
                if (Physics.Raycast(ray, out hitInfo))
                {
                    // Check if the hit object has the "car" tag and is the same as the current car
                    if (hitInfo.transform.CompareTag("car") && hitInfo.transform.gameObject == car[i])
                    {
                        collidedWithCar = true;
                        Debug.Log("Collision occurred with car " + i);
                        return i;
                    }
                }

            }

        }

        return -1; // Return -1 if no car was hit
    }

}
