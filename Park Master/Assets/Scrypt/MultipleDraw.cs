using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MultipleDraw : MonoBehaviour
{
    public float distanceBetweenPoints = 0.1f;
    public LayerMask groundLayer;
    public float speed = 0.2f;
    public float rotationSpeed = 1.0f;

    public GameObject[] car;

    private LineRenderer[] lineRenderer;
    private Vector3 lastPoint;
    private List<Vector3>[] PointsList; //= new List<Vector3>();
    private List<Vector3> PreviousPointsList = new List<Vector3>();

    private int carIndex = 0;

    private bool collidedWithCar = false;
    //private bool existPreviousList = false;

    void Start()
    {
        lineRenderer = new LineRenderer[car.Length];
        PointsList = new List<Vector3>[car.Length];

        for (int i = 0; i < car.Length; i++)
        {
            lineRenderer[i] = car[i].GetComponent<LineRenderer>();
            lineRenderer[i].enabled = false;
            lineRenderer[i].positionCount = 0;
            PointsList[i] = new List<Vector3>();
        }
        //pauseGame();
    }

    void Update()
    {
        if (Time.timeScale != 0 )
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
                }
            }


            if (Input.GetMouseButtonUp(0))
            {
                collidedWithCar = false;
                StartCoroutine(MoveCarTowardPath());

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

    private IEnumerator MoveCarTowardPath()
    {
        float t = 0;

        for (int i = 0; i < PointsList[carIndex].Count - 1; i++)
        {
            if(carIndex != -1)
            {
                t = Time.deltaTime * speed;
                //car[carIndex].transform.Translate(PointsList[carIndex][i + 1]) ;
                car[carIndex].transform.position = Vector3.Lerp(PointsList[carIndex][i], PointsList[carIndex][i + 1], t);
                //car.transform.rotation = Quaternion.LookRotation(new Vector3(PointsList[i].x, PointsList[i].y, PointsList[i].z) );

                Vector3 direction = new Vector3(0, (PointsList[carIndex][i + 1] - PointsList[carIndex][i]).x, 0);

                direction = Vector3.Normalize(direction);   


                if (direction != Vector3.zero)
                {

                   Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
                   transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
                }

           
                yield return null;
            }
           
        }
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
        Vector3 mousePosScreen = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(mousePosScreen);

        for (int i = 0; i < car.Length; i++)
        {
            //Debug.Log("car i = " + i);
            RaycastHit hitInfo;

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
        return -1; // Return -1 if no car was hit
    }


    private void pauseGame()
    {
        Time.timeScale = 0;
    }
}
