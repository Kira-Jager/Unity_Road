using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



  
}

/*private IEnumerator balance(float rotationForce)
{
    float amount = 0;


    while (Math.Abs(currentRotation - maxRotation) != 0)
    {
        Debug.Log(" I'm working");

        amount = ((maxRotation - currentRotation) / rotationForce) * rotationSpeed * Time.deltaTime;
        float newRotation = currentRotation + amount;
        rotationForce -= amount;

        float rotationValue = newRotation - currentRotation;

        //Quaternion.Slerp(transform.rotation, Quaternion.Euler(rotationValue, 0, 0), 0.1f);


        transform.Rotate(Vector3.up, rotationValue);
        Debug.Log("current rotation " + currentRotation);
        currentRotation = newRotation;

        yield return null;
    }

    currentRotation = 0;


    rotationForce = -rotationForce;

    while (Math.Abs(currentRotation - maxRotation) != 0)
    {
        //Debug.Log(" I'm working");

        amount = ((maxRotation - currentRotation) / rotationForce) * rotationSpeed * Time.deltaTime;
        float newRotation = currentRotation + amount;
        rotationForce -= amount;

        float rotationValue = newRotation - currentRotation;

        //Quaternion.Slerp(transform.rotation, Quaternion.Euler(rotationValue, 0, 0), 0.1f);

        transform.Rotate(Vector3.up, rotationValue);

        //Debug.Log("current rotation " + currentRotation);
        currentRotation = newRotation;

        yield return null;
    }

}



private IEnumerator balance()
{
    float amount = 0;


    while (Math.Abs(currentRotation - maxRotation) > 0)
    {
        Debug.Log(" I'm working");

        amount = ((maxRotation - currentRotation) / rotationForce) * rotationSpeed * Time.deltaTime;
        float newRotation = currentRotation + amount;
        rotationForce -= amount;

        float rotationValue = newRotation - currentRotation;

        //Quaternion.Slerp(transform.rotation, Quaternion.Euler(rotationValue, 0, 0), 0.1f);


        transform.Rotate(Vector3.up, rotationValue);
        Debug.Log("current rotation " + currentRotation);
        currentRotation = newRotation;

        yield return null;
    }

    currentRotation = 0;


    rotationForce = -rotationForce;

    while (Math.Abs(currentRotation - maxRotation) > 0)
    {
        //Debug.Log(" I'm working");

        amount = ((maxRotation - currentRotation) / rotationForce) * rotationSpeed * Time.deltaTime;
        float newRotation = currentRotation + amount;
        rotationForce -= amount;

        float rotationValue = newRotation - currentRotation;

        //Quaternion.Slerp(transform.rotation, Quaternion.Euler(rotationValue, 0, 0), 0.1f);

        transform.Rotate(Vector3.up, rotationValue);

        //Debug.Log("current rotation " + currentRotation);
        currentRotation = newRotation;

        yield return null;
    }


}
}


private IEnumerator Balance()
{
    float currentAmount = 0;

    while (Math.Abs(maxRotation - currentRotation) > 0.5f && Math.Abs(rotationForce) > 0.5f)

    {
        float amount = (rotationForce * rotationSpeed) / (maxRotation - currentRotation) * Time.deltaTime;
        currentAmount += amount;

        // Limit the rotation so it doesn't go beyond maxRotation
        currentAmount = Mathf.Clamp(currentAmount, 0, maxRotation - currentRotation);

        transform.Rotate(Vector3.up, currentAmount);

        Debug.Log("current rotation " + currentRotation);
        //Debug.Log("current force " + rotationForce);


        currentRotation += currentAmount;
        if (rotationForce > 0)
        {
            rotationForce -= amount;
            rotationSpeed -= amount;
        }

        yield return new WaitForSeconds(.5f);
    }

    currentRotation = 0;

    rotationForce = -rotationForce;
    //Debug.Log("rotation force " + rotationForce);

    while (Math.Abs(maxRotation - currentRotation) > 0.5f && Math.Abs(rotationForce) > 0.5f)


    {
        float amount = (rotationForce * rotationSpeed) / (maxRotation - currentRotation) * Time.deltaTime;
        currentAmount += amount;

        // Limit the rotation so it doesn't go beyond maxRotation
        currentAmount = Mathf.Clamp(currentAmount, 0, maxRotation - currentRotation);

        transform.Rotate(Vector3.up, currentAmount);

        Debug.Log("current rotation in second while loop " + currentAmount);

        currentRotation += currentAmount;

        if (Math.Abs(rotationForce) > 0)
        {
            rotationForce += amount;
            rotationSpeed -= amount;
        }
        rotationForce -= amount;

        yield return new WaitForSeconds(.5f);
    }
}*/
