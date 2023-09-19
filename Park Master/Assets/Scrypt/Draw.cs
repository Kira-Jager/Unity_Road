using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Draw : MonoBehaviour
{
    public float distanceBetweenPoints = 0.1f;
    public LayerMask groundLayer;
    public float speed = 0.2f;
    public float rotationSpeed = 1.0f;

    public GameObject car;

    private LineRenderer lineRenderer;
    private Vector3 lastPoint;
    private List<Vector3> PointsList = new List<Vector3>();
    private List<Vector3> PreviousPointsList = new List<Vector3>();

    private bool collidedWithCar = false;
    //private bool existPreviousList = false;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        lineRenderer.alignment = LineAlignment.View;
        lineRenderer.positionCount = 0;

        pauseGame();
    }

    void Update()
    {
        if(Time.timeScale != 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                CheckTheHitRayOnCar();
                if (collidedWithCar)
                {
                    lineRenderer.enabled = true;
                    lastPoint = GetMouseWorldPosition();
                    //lineRenderer.positionCount = 1;
                    //lineRenderer.SetPosition(0, lastPoint);
                }


            }
            else if (Input.GetMouseButton(0))
            {
                if (collidedWithCar)
                {

                    Vector3 mousePoint = GetMouseWorldPosition();
                    if (Vector3.Distance(lastPoint, mousePoint) > distanceBetweenPoints)
                    {
                        lastPoint = mousePoint;
                        lineRenderer.positionCount++;
                        lineRenderer.SetPosition(lineRenderer.positionCount - 1, lastPoint);
                        PointsList.Add(mousePoint);
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

        for (int i = 0; i < PointsList.Count - 1; i++)
        {
            t = Time.deltaTime * speed;
            car.transform.position = Vector3.Lerp(PointsList[i], PointsList[i + 1], t);
            //car.transform.rotation = Quaternion.LookRotation(new Vector3(PointsList[i].x, PointsList[i].y, PointsList[i].z) );

            Vector3 direction = new Vector3(0,(PointsList[i + 1] - PointsList[i]).x,0);

            //transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);;

            if (direction != Vector3.zero)
            {

                Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }

            /* if (direction != Vector3.zero) // Prevent LookRotation from throwing an error when direction is zero
                 {
                     car.transform.rotation = Quaternion.LookRotation(direction);
                 }*/
            yield return null;
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


    private void CheckTheHitRayOnCar()
    {
        Vector3 mousePosScreen = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(mousePosScreen);

        //  get information about what the ray hit, like the object's position, etc.
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.transform.CompareTag("car"))
            {
                collidedWithCar = true;
                Debug.Log("Collision occured with car");
            }
        }
    }

    private void pauseGame()
    {
        Time.timeScale = 0;
    }
}




/*
The `Camera.ScreenToWorldPoint` function works well for 2D games or 3D games where the camera is aligned orthogonally (i.e., facing directly down onto the XZ plane). It converts a point from screen space into world space based on the camera's projection.

However, in a 3D game where the camera is not orthogonal (like in your case where the camera rotation is `(70.43,0,0)`), using `ScreenToWorldPoint` can lead to unexpected results. This is because `ScreenToWorldPoint` does not take into account the depth (Z coordinate) by default. It will return a point on the camera's near clipping plane.

The `Raycast` method, on the other hand, allows us to find where a ray (a line starting from the camera and passing through the mouse position) intersects with a specific plane (in this case, the XZ plane). This gives us a more accurate position in 3D space for where the mouse cursor is "pointing" at.

In your original script, you were using `Raycast` to check if the mouse was over the car. The revised script uses a similar approach but checks for intersection with the XZ plane instead of a specific object. This allows you to draw on any part of the plane, not just on specific objects.

I hope this clarifies things! Let me know if you have any other questions

 */