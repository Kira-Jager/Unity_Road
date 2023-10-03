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
    public LayerMask groundLayer;
    public bool anyCar = false;
    public bool carAccident = false;
    public bool anotherDrawing = false;

    public GameObject carParent;
    public Dictionary<int, List<Vector3>> carPoints = new Dictionary<int, List<Vector3>>();

    private bool allCarsFinished = true;
    private LevelManager levelManager;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();

        int carID = 0;

        foreach ( Transform carTransform  in carParent.transform)
        {
            carTransform.GetComponent<Car>().setCarID(carID);
            carID++;
        }
    }

    private void Update()
    {
        if (carAccident)
        {
            levelManager.retryCanvas();
        }
        if (anotherDrawing)
        {
            foreach (Transform carTransform in carParent.transform)
            {
                carTransform.GetComponent<Car>().resetCar();
            }
            anotherDrawing = false;
        }
        
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
            ShowWinCanvas();
        }
    }

    private void ShowWinCanvas()
    {
        levelManager.WinCanvas();
    }
}
