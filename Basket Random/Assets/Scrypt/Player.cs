using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool isJumping = false;
    private bool flipRotation = false;

    private Rigidbody rb;
    private bool maxRotationDecreased = false;

    private float rotationDirection = 1f;
    private Quaternion targetRotation;
    private Quaternion lastRotation;

    public float currentRotation = 0;
    float previousMaxROtation;


    public float amount = 2f;
    public float decreaseAmount = 2f;

    public float maxRotation = 90;
    //public float currentMaxRotation = 90;
    public float threshold = 0.1f;
    public float jumpForce = 10f;
/*    public float rotationSpeed = 10f;
    public float rotationForce = 10f;*/

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
            balance();
            //Invoke("balance",2f);
        }
    }

    private void OnEnable()
    {
        //GetComponent<Rigidbody>().isKinematic = false;

        InputController.onKeyPressed += jump;
    }

    private void OnDisable()
    {
        InputController.onKeyPressed -= jump;

        //GetComponent<Rigidbody>().isKinematic = true;

    }

    private void jump()
    {
        isJumping = true;

        rb.AddRelativeForce(Vector3.up * jumpForce);

    }

    private void Balance()
    {
        if (maxRotation > 0)
        {
            currentRotation += amount * rotationDirection;

            Quaternion targetRotation = Quaternion.Euler(0, 0, currentRotation);
            transform.rotation = targetRotation;

            if (Mathf.Abs(currentRotation) > maxRotation)
            {
                // Decrease maxRotation after it's been reached
                Debug.Log("Max rotation" + maxRotation);
                maxRotation -= decreaseAmount;
                switchDirection();
            }
        }
    }

    private void switchDirection()
    {
        rotationDirection *= -1;
        
        transform.rotation = Quaternion.Euler(0, 0, maxRotation * rotationDirection);
    }




    private void balance()
    {
        if (maxRotation > threshold)
        {
            if (flipRotation == false)
            {
                currentRotation += amount;
                rotatePlayer();
            }
            else
            {
                currentRotation -= amount;
                rotatePlayer();
            }
        }
        
    }
    private void rotatePlayer()
    {
        

        Quaternion targetRotation = Quaternion.Euler(0, 0, currentRotation);
        transform.rotation = targetRotation;
        //lastRotation = Quaternion.Slerp(lastRotation != null ? lastRotation : Quaternion.Euler(Vector3.zero), targetRotation, 0.2f);
        //transform.rotation = lastRotation;
        if (currentRotation > maxRotation)
        {
            flipRotation = true;
            maxRotationDecreased = false;
        }
        else if (currentRotation < -maxRotation)
        {
            flipRotation = false;
            maxRotationDecreased = false;
        }
        if (maxRotationDecreased == false && Mathf.Abs(currentRotation) > maxRotation )
        {
            maxRotation -=  decreaseAmount; 
            maxRotationDecreased = true; // Set the flag to prevent further decrease
            Debug.Log("max Rotation " + maxRotation); //work only when decreased amount < amount
        }
    }
}


/*  private void balance()

    { 
        if (flipRotation == false)
        {
            currentRotation += amount;
            rotatePlayer();
        }
        else
        {
            currentRotation -= amount;
            rotatePlayer();
        }
    }


    private void rotatePlayer()
    {
        Quaternion targetRotation = Quaternion.Euler(0, 0, currentRotation);
        lastRotation = Quaternion.Slerp(lastRotation != null ? lastRotation : Quaternion.Euler(Vector3.zero), targetRotation, 0.2f);
        transform.rotation = lastRotation;

        if (Math.Abs(currentRotation - maxRotation) <= threshold)
        {
            flipRotation = true;
            maxRotationDecreased = false;
        }
        else if (Math.Abs(currentRotation - (-maxRotation)) <= threshold)
        {
            flipRotation = false;
            maxRotationDecreased = false;
        }
        if (!maxRotationDecreased && Mathf.Abs(Mathf.Abs(currentRotation) - maxRotation) > threshold)
        {
            maxRotation = maxRotation - decreaseAmount;
            maxRotationDecreased = true; // Set the flag to prevent further decrease
            Debug.Log("max Rotation " + maxRotation);
        }
    }*/



/*    private void rotatePlayer()
    {
       
            //float amount = (rotationForce * rotationSpeed) / (maxRotation - Mathf.Abs(currentRotation)) * Time.deltaTime;


            //currentRotation += amount;

            if (rotationForce > 0)
            {
                // Cap rotation within 0 to 180
                //currentRotation = Mathf.Clamp(currentRotation, 0, maxRotation);

                // Decrease rotationForce if it exceeds a threshold
                
                rotationForce -= amount ;
                
            }
            else
            {
                // Cap rotation within -180 to 0
                //currentRotation = Mathf.Clamp(currentRotation, -maxRotation, 0);

                // Increase rotationForce if it is below a threshold
             
                 rotationForce += amount ;
                
            }

        currentRotation += amount;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, currentRotation));
            lastRotation = Quaternion.Slerp(lastRotation != null ? lastRotation : Quaternion.Euler(Vector3.zero), targetRotation, 0.2f);
            transform.rotation = lastRotation;

            if (Mathf.Approximately(Mathf.Abs(currentRotation), maxRotation - 1))
            {
                Debug.Log("flipped");
                flipRotation = true;
            }
            else
            {
                Debug.Log("nothing yet");
            }
        
    }*/



/*    private void rotatePlayer()
    {
        if ( Mathf.Abs(rotationForce) > 0.1f)
        {
            float amount = (rotationForce * rotationSpeed) / (maxRotation - currentRotation) * Time.deltaTime;

            currentRotation += amount;

            if (rotationForce > 0.1f)
            {
                rotationForce -= amount / 2;
            }
            else if (rotationForce < 0.1f)
            {
                rotationForce += amount/2;
            }

            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, currentRotation));
            lastRotation = Quaternion.Slerp(lastRotation != null ? lastRotation : Quaternion.Euler(Vector3.zero), targetRotation, 0.2f);
            transform.rotation = lastRotation;

            if(rotationForce > 0)
            {
                if (Mathf.Approximately(currentRotation , (maxRotation / 2) -1) )
                {
                    Debug.Log("flipped");
                    flipRotation = true;
                }
            }
            else
            {
                    Debug.Log("nothing yet");

            }

        }
    }*/


