using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    private GameManager gameManager;
    private Line line;




    private float speed;
    private float rotationSpeed;

    private bool collidedWithCar = false;
    private bool carMoving = false;
    private bool CarArriveFinish = false;
    private bool carGetSelected = false;
    private bool carHasPath = false;


    public void Initialize(GameManager manager, Line line)
    {
        this.line = line;
        gameManager = manager;
        speed = gameManager.speed;
        rotationSpeed = gameManager.rotationSpeed;
    }

    public void setCarMoving()
    {
        carMoving = true;
    }

    public bool getCarMoving()
    {
        return carMoving;
    }

    public void setCarCollisionWithCar()
    {
        collidedWithCar = true;
    }

    public bool getCarCollision()
    {
        return collidedWithCar;
    }


    public void setCarArriveFinish()
    {
        CarArriveFinish = true;
    }

    public bool getCarArriveFinish()
    {
        return CarArriveFinish;
    }


    public void setCarHasPath()
    {
        carHasPath = true;
    }

    public bool getCarHasPath()
    {
        return carHasPath;
    }

    public void setcarGetSelected()
    {
        carGetSelected = true;
    }
    
    public bool getcarGetSelected()
    {
        return carGetSelected;
    }

    public void  Start()
    {
        line = GetComponent<Line>();

        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found!");
            return;
        }

        Initialize(gameManager, line);
    }







    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("car"))
        {
            setCarCollisionWithCar();
        }
        if (collision.gameObject.CompareTag("Finish"))
        {
            setCarArriveFinish();
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (getCarHasPath())
            {
                StartCoroutine(MoveCarTowardPath());
            }

        }
    }

    private IEnumerator MoveCarTowardPath()
    {
        setCarMoving();

        for (int i = 0; i < line.getCarPath().Count - 1; i++)
        {

            if (CheckTheHitRayOnCar() )
            {

                float distance = Vector3.Distance(line.getCarPath()[i], line.getCarPath()[i + 1]);

                float Duration = distance / speed;

                float startTime = Time.time;

                //Debug.Log("Start time " + startTime);

                float moveSpeed = Time.deltaTime * speed;


                //car rotation at point i and i + 1


                while (Time.time - startTime < Duration)
                {
                    float Progress = (Time.time - startTime) / Duration;

                  transform.position = Vector3.Lerp(line.getCarPath()[i], line.getCarPath()[i + 1], Progress);

                    Vector3 directionToNextPoint = line.getCarPath()[i + 1] - transform.position;

                    if (directionToNextPoint != Vector3.zero)
                    {
                        //car[carIndex].transform.rotation = Quaternion.LookRotation(directionToNextPoint);

                        Quaternion targetRotation = Quaternion.LookRotation(directionToNextPoint);
                        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                    }

                    yield return null;
                }

                transform.position = line.getCarPath()[i + 1];
            }

        }

        carMoving = false;
    }



    public bool CheckTheHitRayOnCar()
    {

        /* if (carMoving)
         {
             return false;
         }
         else*/
        {
            RaycastHit hitInfo;
            Vector3 mousePosScreen = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePosScreen);

            // Use Physics.Raycast to check if the ray hits the current car
            if (Physics.Raycast(ray, out hitInfo))
            {
                // Check if the hit object has the "car" tag and is the same as the current car
                if (hitInfo.transform.CompareTag("car") && hitInfo.transform == this.transform )
                {
                    setcarGetSelected();
                    Debug.Log("Hit arrives on car " + transform.name);
                    return true;
                }
            }

        }

        return true; // Return -1 if no car was hit
    }
}
