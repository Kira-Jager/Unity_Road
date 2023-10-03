using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Car : MonoBehaviour
{
    private GameManager gameManager;
    private Line line;

    private float speed;
    private float rotationSpeed;

    private bool collidedWithCar = false;
    private bool carMoving = false;
    private bool carCollision = false;
    private bool CarArriveFinish = false;
    private bool stopCar = false;
    private Vector3 carInitialPosition;

    Quaternion carInitialRotation;

    private int carID = -1;
    private Material carColor;

    public delegate void onCarFinishACtion();
    public static event onCarFinishACtion onCarFinish;


    public void Initialize(GameManager manager, Line line)
    {
        gameManager = manager;
        this.line = line;
        speed = gameManager.speed;
        rotationSpeed = gameManager.rotationSpeed;
    }

    public void setCarID(int carID)
    {
        this.carID = carID;
    }

    public int getCarID()
    {
        return this.carID;
    }

    public void setCarMoving()
    {
        this.carMoving = true;
    }

    public bool getCarMoving()
    {
        return this.carMoving;
    }

    public void setCarCollisionWithCar(bool boolean)
    {
        gameManager.carAccident = boolean;
        this.carCollision = boolean;
    }

    public bool getCarCollision()
    {
        return this.carCollision;
    }


    public void setCarArriveFinish(bool boulean)
    {
        this.CarArriveFinish = boulean;
        onCarFinish?.Invoke();
    }

    public bool getCarArriveFinish()
    {
        return this.CarArriveFinish;
    }


    public void Start()
    {
        line = GetComponent<Line>();

        carColor = GetComponent<MeshRenderer>().material;


        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found!");
            return;
        }

        Initialize(gameManager, line);
        line.setLineColor(carColor);

        carInitialPosition = transform.position;

        carInitialRotation = transform.rotation;


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("car"))
        {
            Debug.Log("car collision");
            setCarCollisionWithCar(true);
        }
        if (collision.gameObject.CompareTag("Finish"))
        {
            setCarArriveFinish(true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            setCarArriveFinish(false);
        }
    }

    private void Update()
    {
        if (Time.timeScale != 0)
        {

            if (Input.GetMouseButtonUp(0) && !carMoving)
            {
                if (CheckTheHitRayOnCar())
                {
                    setCarCollisionWithCar(false);
                    Debug.Log("Car get hit");
                    StartCoroutine(MoveCarTowardPath());
                }
            }
        }

    }

    public void OnMouseDown()
    {
        if (CheckTheHitRayOnCar() && Time.timeScale != 0)
        {
            gameManager.anotherDrawing = true;
            line.startDrawing();
        }

    }
    public void OnMouseUp()
    {
        if (Time.timeScale != 0)
        {
            line.stopDrawing();
        }
    }

    //StartCoroutine(MoveCarTowardPath());

    private IEnumerator MoveCarTowardPath()
    {   
        if(stopCar == false)
        {
            setCarMoving();

            List<Vector3> carPath = line.getCarPath();

            for (int i = 0; i < line.getCarPath().Count - 1; i++)
            {

                if (collidedWithCar && carCollision == false && carMoving)
                {

                    float distance = Vector3.Distance(carPath[i], carPath[i + 1]);

                    float Duration = distance / speed;

                    float startTime = Time.time;


                    while (Time.time - startTime < Duration)
                    {

                        if (!carMoving) { break; }
                        float Progress = (Time.time - startTime) / Duration;
                            

                        transform.position = Vector3.Lerp(carPath[i], carPath[i + 1], Progress);


                        Vector3 directionToNextPoint = carPath[i + 1] - transform.position;

                        if (directionToNextPoint != Vector3.zero)
                        {
                            Quaternion targetRotation = Quaternion.LookRotation(directionToNextPoint);
                            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                        }

                        yield return null;
                    }



                    //transform.position = carPath[i + 1];
                }

            }

            carMoving = false;
        }
        
    }

    public bool CheckTheHitRayOnCar()
    {
        if (carMoving)
        {
            return false;
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
                if (hitInfo.transform.CompareTag("car") && hitInfo.transform == this.transform)
                {
                    collidedWithCar = true;

                    /*  Debug.Log("Hit arrives on car " + transform.name);
                      Debug.Log("Car ID " + this.getCarID());*/
                    return true;
                }
            }
        }
        return true;
    }

    private void OnEnable()
    {
        Reset_Line_CarPos.onClicked += resetCarPos;
    }

    private void OnDisable()
    {
        Reset_Line_CarPos.onClicked -= resetCarPos;
    }

    private void resetCarPos()
    {
        if (carMoving)
        {
            //stopCar = true;
            resetCar();
            line.clearPath();
        }

    }

    public void resetCar()
    {
        carMoving = false;
        transform.position = carInitialPosition;
        transform.rotation = carInitialRotation;
    }
}
