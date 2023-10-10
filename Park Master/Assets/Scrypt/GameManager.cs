using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


public class GameManager : MonoBehaviour
{

    public float speed = 0.1f; 
    public float rotationSpeed = 1.0f;
    public float distanceBetweenPoints = 1.0f;
    public float forceMultiplier = 1.0f;
    public float torqueMultiplier = 1.0f;
    public float lineWidth = 2.0f;
    public bool anyCar = false;
    public bool carAccident = false;
    public bool anotherDrawing = false;
    public bool setFirstHitForEachCar = false;
    public static bool disableObject = false;

    public AudioSource CarAudio;
    public AudioClip carMovingAudio;
    public AudioClip carCollisionAudio;
    public AudioClip carWinAudio;



    public LayerMask groundLayer;
    public LayerMask stopDraw;

    public GameObject carParent;
    public Dictionary<int, List<Vector3>> carPoints = new Dictionary<int, List<Vector3>>();

    private LevelManager levelManager;
    private GameObject confettiObject;
    private GameObject confettiObject2;
    private ParticleSystem confetti;
    private ParticleSystem confetti2;

    private bool allCarsFinished = true;
    private bool allCarsHavePaths = false;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();


        int carID = 0;

        foreach (Transform carTransform in carParent.transform)
        {
            carTransform.GetComponent<Car>().setCarID(carID);
            carID++;
        }

        confettiObject = transform.GetChild(0).gameObject;
        confettiObject2 = transform.GetChild(1).gameObject;

        confetti = confettiObject.GetComponent<ParticleSystem>();
        confetti.Stop();
        confetti2 = confettiObject2.GetComponent<ParticleSystem>();
        confetti2.Stop();

    }

    private void Update()
    {
        if (carAccident)
        {
            Invoke("retryLevel",1f);
        }
        if (anotherDrawing)
        {
            foreach (Transform carTransform in carParent.transform)
            {
                carTransform.GetComponent<Car>().resetCar();
            }
            anotherDrawing = false;
        }

        allCarsHavePaths = true;

        for (int i = 0; i < carParent.transform.childCount; i++)
        {
            Transform carTransform = carParent.transform.GetChild(i);
            if (carTransform.GetComponent<Line>().pathExist() == false)
            {
                allCarsHavePaths = false;
                break;
            }
        }

        if (allCarsHavePaths == true)
        {
            for (int i = 0; i < carParent.transform.childCount; i++)
            {
                Transform carTransform = carParent.transform.GetChild(i);
                carTransform.GetComponent<Car>().collidedWithCar = true;
                if (setFirstHitForEachCar)
                {
                    carTransform.GetComponent<Car>().firstHitOnCar = true;
                }


            }
        }

    }

    private void retryLevel()
    {
        levelManager.retryCanvas();
    }

    private void OnEnable()
    {
        Car.onCarFinish += HandleCarFinish;
    }

    private void OnDisable()
    {
        Car.onCarFinish -= HandleCarFinish;
    }

    private void HandleCarFinish()
    {
        bool allCarsFinished = true;
        foreach (Transform carTransform in carParent.transform)
        {
            Car car = carTransform.GetComponent<Car>();
            if (!car.getCarArriveFinish())
            {
                allCarsFinished = false;
                break;
            }
        }

        if (allCarsFinished)
        {
            Invoke("ShowWinCanvas", .4f);
        }
    }

    private void ShowWinCanvas()
    {

        disableObject = true;
        confetti.Play();
        confetti2.Play();

        levelManager.WinCanvas();
    }
}