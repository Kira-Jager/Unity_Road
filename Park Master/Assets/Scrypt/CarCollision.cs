using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollision : MonoBehaviour
{
    // Start is called before the first frame update
    public bool[] carFinishPath;
    public int carParentLenght;
    private MultipleDraw carObject;
    private LevelManager levelManager;
    
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        carObject = FindObjectOfType<MultipleDraw>();

        carParentLenght = carObject.car.Length;

        carFinishPath = new bool[carParentLenght];
        for(int i = 0; i < carParentLenght; i++)
        {
            carFinishPath[i] = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        bool allCarsFinished = true;
        for (int i = 0; i < carParentLenght; i++)
        {
            if (collision.gameObject.CompareTag("Finish") )
            {
                carFinishPath[carObject.getCarIndex()] = true;
            }
            if (collision.gameObject.CompareTag("car"))
            {
                carObject.setCollisionBtwCars();
            }

            if (!carFinishPath[i])
            {
                allCarsFinished = false;
            }
        }

        if (allCarsFinished)
        {
            levelManager.WinCanvas();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
