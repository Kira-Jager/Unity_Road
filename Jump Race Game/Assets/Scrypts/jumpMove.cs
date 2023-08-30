using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpMove : MonoBehaviour
{
    public float jumpForce = 10f;
    public float moveSpeed = 10f;
    public float rotationSpeed = 10f;
    public Cinemachine.CinemachineVirtualCamera vcam;
    public GameObject plateforme;

    private Rigidbody rb;
    private Animator animator;

    private bool isJumping = false;
    private Vector3 dragOrigin;
    private Vector3 dragDirection;

    private GameObject[] plateformChild;
    private GameObject nextPlateforme;
    private int childCount;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        childCount = plateforme.transform.childCount;
        plateformChild = new GameObject[childCount];
        for (int i = 0; i < childCount; i++)
        {
            plateformChild[i] = plateforme.transform.GetChild(i).gameObject;
            //Debug.Log("Child name in Start: " + plateformChild[i].name);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        rb.velocity = Vector3.up * jumpForce;
        isJumping = true;
        animator.SetBool("isJumping", true);
        animator.SetBool("isFliping", true);
        Debug.Log(plateformChild.Length + " = lenght");

        Invoke("startFlipping", 0.8f);

        for (int i = 0; i < plateformChild.Length; i++)
        {
            //if(collision.gameObject == plateformChild[i] && (collision.gameObject.tag == "plateforme" || collision.gameObject.tag == "finish") )
            if(collision.gameObject.tag == "plateforme" && collision.gameObject == plateformChild[i])
            {
                
                
                    nextPlateforme = plateformChild[i + 1] ;

                    //plateformChild[i].gameObject.tag = "Untagged";
                    lookNextPlateforme(nextPlateforme);


                Debug.Log("i = " + i);
                Debug.Log(" plateforme Count in loop = " + plateformChild.Length);

                Debug.Log("current plateforme name  = " + plateformChild[i].name);
                Debug.Log("next plateforme name = " + (nextPlateforme != null ? nextPlateforme.name : "None"));

                break;
            }

        }


        if (collision.gameObject.tag == "Finish")
        {
            Debug.Log("Done");
        }

    }


    private void startFlipping()
    {
        Debug.Log("startFlipping");
        animator.SetBool("isFliping", false);
        animator.SetBool("isJumping", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "deadZone")
        {
            animator.SetBool("isFalling", true);
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            rb.velocity = Vector3.forward * moveSpeed * Time.deltaTime;
        }

        if (Input.GetMouseButton(0) && isJumping)
        {
            dragDirection = (Input.mousePosition - dragOrigin).normalized;

            lookTarget();

            /*rb.AddRelativeForce( moveSpeed * Time.deltaTime * Vector3.forward , ForceMode.VelocityChange);*/

            rb.velocity += transform.forward * moveSpeed * Time.deltaTime;

        }

    }
    void lookTarget()
    {

        float rotationAmount = dragDirection.x * rotationSpeed;

        vcam.transform.Rotate(0f, rotationAmount, 0f, Space.World);

        transform.Rotate(0f, rotationAmount, 0f, Space.World);

    }

    void lookNextPlateforme(GameObject nextPlateforme)
    {

        if (nextPlateforme != null)
        {
            Vector3 directionToNextPlatform = nextPlateforme.transform.position - transform.position;
            directionToNextPlatform.y = 0; 

            Quaternion targetRotation = Quaternion.LookRotation(directionToNextPlatform);

            // Rotate the character

            //transform.rotation = targetRotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 3f);


            // Calculate the camera's new rotation without changing its y position

            Vector3 vcamRotateEulerAngles = new Vector3(vcam.transform.rotation.eulerAngles.x, targetRotation.eulerAngles.y, vcam.transform.rotation.eulerAngles.z);
            Quaternion vcamRotateQuaternion = Quaternion.Euler(vcamRotateEulerAngles);

            // Set the camera's rotation

           vcam.transform.rotation = Quaternion.Slerp(vcam.transform.rotation, vcamRotateQuaternion, 3f);
           //vcam.transform.rotation =  vcamRotateQuaternion;
            //vcam.transform.rotation = vcamRotateQuaternion;
        }

    }

}




