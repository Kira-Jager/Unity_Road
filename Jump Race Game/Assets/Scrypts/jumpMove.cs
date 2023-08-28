using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpMove : MonoBehaviour
{
    public float jumpForce = 10f;
    public float moveSpeed = 10f;
    public Cinemachine.CinemachineVirtualCamera vcam;
    public float rotationSpeed = 10f;

    private Rigidbody rb;
    private Animator animator;

    private bool isJumping = false;
    private Vector3 dragOrigin;
    private Vector3 dragDirection;

    private GameObject[] plateforme;
    private int plateformeCount;
    private GameObject nextPlateforme;
    private GameObject currentlateforme;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        plateforme = GameObject.FindGameObjectsWithTag("plateforme");
        plateformeCount = plateforme.Length;
        currentlateforme = plateforme[0];
        Debug.Log(" plateforme Count = " + plateforme.Length);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
            rb.velocity = Vector3.up * jumpForce;
            isJumping = true; 
            animator.SetBool("isJumping", true);
            animator.SetBool("isFliping", true);
            Debug.Log("jumping");
            Invoke("startFlipping", 0.8f);



        /* for (int i = plateformeCount-1 ; i <= 0 ; i--)
         {
             if (collision.gameObject == plateforme[i] && collision.gameObject.tag == "plateforme")
             {
                 nextPlateforme = plateforme[i-1];
                 plateforme[i].gameObject.tag = "Untagged";
                 lookNextPlateforme();
                 Debug.Log("current plateforme name " + plateforme[i].name);
                 Debug.Log("next plateforme name "+nextPlateforme.name);
                 break;
             }
         }*/

        for (int i = 0; i < plateformeCount; i++)
        {
            if (collision.gameObject == plateforme[i] && collision.gameObject.tag == "plateforme")
            {
                /*nextPlateforme = (i > 0) ? plateforme[i - 1] : null;*/
                Debug.Log("i = " + i);
                Debug.Log(" plateforme Count in loop = " + plateforme.Length);
    
                    nextPlateforme =plateforme[i-1] ;


                plateforme[i].gameObject.tag = "Untagged";

                lookNextPlateforme();

                Debug.Log("current plateforme name  = " + plateforme[i].name);
                Debug.Log("next plateforme name = " + (nextPlateforme != null ? nextPlateforme.name : "None"));
                break;
            }
        }


        if (collision.gameObject.tag == "finish")
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

    void lookNextPlateforme()
    {
        if (nextPlateforme != null)
        {
            Vector3 directionToNextPlatform = nextPlateforme.transform.position - transform.position;
            directionToNextPlatform.y = 0; // Ignore vertical difference
            Quaternion targetRotation = Quaternion.LookRotation(directionToNextPlatform);

            Debug.Log("directionToNextPlatform = " + directionToNextPlatform);

            // Smoothly rotate towards the target rotation
           /* vcam.transform.rotation = Quaternion.Slerp(vcam.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);*/

            vcam.transform.Rotate(0f,directionToNextPlatform.y,0f, Space.World);
            transform.Rotate(0f, directionToNextPlatform.y, 0f, Space.World);


            /*transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);*/
        }
    }
}

