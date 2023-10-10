using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Car : MonoBehaviour
{
    private GameManager gameManager;
    private Line line;

    private float speed;
    private float rotationSpeed;
    private float forceMultiplier;
    private float torqueMultiplier;

    private bool carMoving = false;
    private bool carCollision = false;
    private bool CarArriveFinish = false;
    private bool stopCar = false;
    private bool breakHappen = false;
    private bool once = false;

    private Vector3 carInitialPosition;
    private Quaternion carInitialRotation;

    private int carID = -1;
    private Material carColor;

    private MeshRenderer reestObjectMeshRenderer;
    private MeshRenderer carFinishAnimObjectMeshRenderer;

    private Animation reestObjectAnimation;
    private Animation carFinishAnim;

    private AudioSource CarAudio;
    private AudioClip carMovingAudio;
    private AudioClip carCollisionAudio;
    private AudioClip carWinAudio;

    public GameObject resetCarPosObject;
    public GameObject resetLineAnimObject;
    public GameObject carFinishAnimObject;
    public bool firstHitOnCar = false;
    public bool collidedWithCar = false;
    public bool audioPlaying = false;
    public delegate void onCarFinishACtion();
    public static event onCarFinishACtion onCarFinish;




    public void Initialize(GameManager manager, Line line)
    {
        gameManager = manager;
        this.line = line;
        speed = gameManager.speed;
        rotationSpeed = gameManager.rotationSpeed;
        forceMultiplier = gameManager.forceMultiplier;
        torqueMultiplier = gameManager.torqueMultiplier;
        carWinAudio = gameManager.carWinAudio;
        carMovingAudio = gameManager.carMovingAudio;
        carCollisionAudio = gameManager.carCollisionAudio;
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

        reestObjectMeshRenderer = resetLineAnimObject.gameObject.transform.GetChild(1).GetComponent<MeshRenderer>();
        reestObjectAnimation = resetLineAnimObject.gameObject.GetComponent<Animation>();


        carFinishAnimObjectMeshRenderer = carFinishAnimObject.gameObject.transform.GetChild(1).GetComponent<MeshRenderer>();
        carFinishAnim = carFinishAnimObject.gameObject.GetComponent<Animation>();

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

        resetLineAnimObject.gameObject.SetActive(false);
        reestObjectMeshRenderer.material = carColor;
        reestObjectAnimation.Stop();

        carFinishAnimObject.gameObject.SetActive(false);
        carFinishAnimObjectMeshRenderer.material = carColor;
        carFinishAnim.Stop();
        CarAudio = GetComponent<AudioSource>();
        CarAudio.Stop();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("car"))
        {
            //CarAudio.Stop();
            gameManager.CarAudio.PlayOneShot(carCollisionAudio,.8f);

            // = true;

            Debug.Log("car collision");
            carAccident(collision);
            setCarCollisionWithCar(true);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        //Invoke("delaySetKinematic", 0.02f);

        setKinematic(true);


        carWinCondition(other);
    }

        private void delaySetKinematic()
        {
             setKinematic(true);
        }

    private void setKinematic(bool boolean)
    {
        GetComponent<Rigidbody>().isKinematic = boolean;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            disableAnimation();
            setCarArriveFinish(false);
        }
    }

    private void carWinCondition(Collider other)
    {
        once = true;
        if (other.gameObject.CompareTag("Finish"))
        {
            Color finishColor = other.gameObject.GetComponent<MeshRenderer>().materials[0].color;

            Debug.Log("Inside Once");

            if (carColor.color == finishColor)
            {
                CarAudio.PlayOneShot(carWinAudio, 1f);
                carFinishAnimObject.gameObject.SetActive(true);
                carFinishAnim.Play();


                setCarArriveFinish(true);
                Invoke("disableAnimation", .8f);
            }


            if (once )
            {
            }
            once = false;
        }
    }

    private void carAccident(Collision collision)
    {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        float random = UnityEngine.Random.Range(9f, 10f); ;
        resetCarPosObject.gameObject.SetActive(false);

        // Check if the Rigidbody component exists
        if (rb != null)
        {
            // Add force to the car in the direction of the collision
            Vector3 force = collision.contacts[0].normal * random * -1;

            rb.AddForce(force * forceMultiplier, ForceMode.Force);

            // Add some torque for a spinning effect
            rb.AddTorque(force * torqueMultiplier);
        }

        GetComponent<BoxCollider>().enabled = false;
        //CarAudio.Stop();


    }

    private void disableAnimation()
    {
        carFinishAnimObject.gameObject.SetActive(false);
    }

   
    private void Update()
    {
        if (Time.timeScale != 0)
        {
            if (breakHappen == true)
            {
                setCarMovBoolean(true);
            }
            if (Input.GetMouseButtonDown(0) && collidedWithCar)
            {
                setKinematic(false);
            }

            if (Input.GetMouseButtonUp(0) && !carMoving)
            {
                //Debug.Log(transform.name + ".CarMoving " + carMoving);
                //Debug.Log(transform.name+ ".CollideWithCar " + collidedWithCar);

                if (collidedWithCar && firstHitOnCar)
                {
                    setKinematic(false);

                    StartCoroutine(MoveCarTowardPath());

                    //setCarCollisionWithCar(false);
                    //Debug.Log("Car get hit");
                }
                breakHappen = false;

            }

        }

        if(carMoving == true)
        {
            resetLineAnimObject.gameObject.SetActive(true);
            reestObjectAnimation.Play();
        }

    }

    public void OnMouseDown()
    {
        if (CheckTheHitRayOnCar() && Time.timeScale != 0)
        {
            line.startDrawing();
            gameManager.anotherDrawing = true;
            gameManager.setFirstHitForEachCar = true;
            firstHitOnCar = true;
            once = true;


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
        if (stopCar == false)
        {
            setCarMoving();

            audioPlaying = true;
            CarAudio.PlayOneShot(carMovingAudio, 1f);


           

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

                        if (CarAudio != null && !CarAudio.isPlaying)
                        {
                            // Play the carMovingAudio clip
                            CarAudio.PlayOneShot(carMovingAudio, 1f);
                        }

                        if (!carMoving)
                        {
                            break;
                        }
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
            CarAudio.Stop();
            
            setCarMovBoolean(false);


        }

    }

    private void setCarMovBoolean(bool boolean)
    {
        collidedWithCar = boolean;
        firstHitOnCar = boolean;
        gameManager.setFirstHitForEachCar = boolean;
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
                if (hitInfo.transform.CompareTag("car"))
                {
                    collidedWithCar = true;

                    Debug.Log("Hit on car");
                    return true;
                }
                /*else
                {
                    collidedWithCar = false;
                }*/
            }
            return false;
        }

    }

    private void OnEnable()
    {
        if (resetCarPosObject != null)
            resetCarPosObject.gameObject.GetComponent<Reset_Line_CarPos>().onClicked += resetCarPos;
    }

    private void OnDisable()
    {
        if (resetCarPosObject != null)
            resetCarPosObject.gameObject.GetComponent<Reset_Line_CarPos>().onClicked -= resetCarPos;
    }

    private void resetCarPos()
    {
        //if (carMoving)
        {
            //stopCar = true;
            resetCar();
            line.clearPath();
        }

    }

    public void resetCar()
    {

        //Debug.Log("Car reset");
        carMoving = false;
        breakHappen = true;

        resetLineAnimObject.gameObject.SetActive(false);
        reestObjectAnimation.Stop();

        transform.position = carInitialPosition;
        transform.rotation = carInitialRotation;
    }
}