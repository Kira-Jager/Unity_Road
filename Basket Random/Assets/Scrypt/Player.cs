using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool isJumping = false;

    private Rigidbody rb;


    public float currentRotation = 0;
    private float maxRotation = 180;

    public float jumpForce = 10f;
    //public float balanceForce = 10f;
    public float rotationSpeed = 10f;
    public float rotationForce = 10f;

    public int maxIterations = 1000; // Set an appropriate maximum iteration count
    int iterationCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isJumping == false)
        {

           Invoke("balance",2f);
        }
    }

    private void OnEnable()
    {
        InputController.onKeyPressed += jump;
    }

    private void OnDisable()
    {
        InputController.onKeyPressed -= jump;
    }

    private void jump()
    {
        isJumping = true;

        rb.AddRelativeForce(Vector3.forward * jumpForce);

        isJumping = false;
    }



    private void balance()
    {
        while (Mathf.Abs(currentRotation - maxRotation/2) > 1f && Mathf.Abs(rotationForce) > 0.001f)
        {
            float amount = (rotationForce * rotationSpeed) / (maxRotation - currentRotation) * Time.deltaTime;

            // Check for potential division by zero
            if (Mathf.Approximately(maxRotation, currentRotation))
            {
                break; // Avoid division by zero
            }

            currentRotation += amount;

            if (rotationForce > 0.0f)
            {
                rotationForce -= amount/2;
            }
            //Debug.Log("current rot " + currentRotation);

            // Calculate the new target rotation
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, currentRotation));

            // Slerp from the current rotation to the target rotation
            transform.rotation = Quaternion.Slerp(Quaternion.Euler(Vector3.zero), targetRotation, 0.2f);
        }

 /*       rotationForce = -rotationForce;

        while (currentRotation < maxRotation / 2 && Mathf.Abs(rotationForce) > 0.001f)
        {
            Debug.Log("in second loop" );
            float amount = (rotationForce * rotationSpeed) / (maxRotation - currentRotation) * Time.deltaTime;

            // Check for potential division by zero
            if (Mathf.Approximately(maxRotation, currentRotation))
            {
                break; // Avoid division by zero
            }

            currentRotation += amount;

            if (rotationForce < 0.0f)
            {
                rotationForce += amount / 2;
            }
            iterationCount++;
            if (iterationCount >= maxIterations)
            {
                Debug.LogWarning("Reached maximum iteration count. Exiting loop.");
                break;
            }

            // Calculate the new target rotation
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, currentRotation));

            // Slerp from the current rotation to the target rotation
            transform.rotation = Quaternion.Slerp(Quaternion.Euler(Vector3.zero), targetRotation, 0.2f);
        }*/




        /*while (Mathf.Abs(currentRotation) < maxRotation)
        {
            float amount = (rotationForce * rotationSpeed) / (maxRotation - currentRotation) * Time.deltaTime;
            // += amount;

            float newRotation = currentRotation + amount;

            // Limit the rotation so it doesn't go beyond maxRotation
            //currentAmount = Mathf.Clamp(currentAmount, 0, maxRotation - currentRotation);

            transform.Rotate(new Vector3(currentRotation,0,0), newRotation);

            Debug.Log("current rotation " + currentRotation);
            //Debug.Log("current force " + rotationForce);


            currentRotation += currentAmount;

            if (rotationForce > 0)
            {
                rotationForce -= amount;
                //rotationSpeed -= amount;
            }

           // yield return new WaitForSeconds(.5f);
        }*/

        /*        currentRotation = 0;

                rotationForce = -rotationForce;
                //Debug.Log("rotation force " + rotationForce);

                while (Mathf.Abs(currentRotation) < maxRotation)


                {
                    float amount = (rotationForce * rotationSpeed) / (maxRotation - currentRotation) * Time.deltaTime;
                    //currentAmount += amount;

                    float newRotation = currentRotation + amount;

                    // Limit the rotation so it doesn't go beyond maxRotation
                    currentAmount = Mathf.Clamp(currentAmount, 0, maxRotation - currentRotation);

                    transform.Rotate(Vector3.up, newRotation);

                    Debug.Log("current rotation in second while loop " + currentAmount);

                    currentRotation += currentAmount;

                    if (Math.Abs(rotationForce) > 0)
                    {
                        rotationForce += amount;
                    }

                   // yield return new WaitForSeconds(.5f);
                }*/
    }

}