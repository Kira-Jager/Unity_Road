using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class jumpMove : MonoBehaviour
{
    public float jumpForce = 10f;
    public float moveSpeed = 10f;
    public float rotationSpeed = 10f;
    public Cinemachine.CinemachineVirtualCamera vcam;
    public GameObject plateforme;
    public Canvas canvas;

    private Rigidbody rb;
    private Animator animator;

    private bool isJumping = false;
    private Vector3 dragOrigin;
    private Vector3 dragDirection;

    private GameObject[] plateformChild;
    private GameObject nextPlateforme;
    
    private int childCount;
    private int twice = 0;
    private int currentLevel = 0;
    private bool isCanvasActive = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();


        childCount = plateforme.transform.childCount;
        plateformChild = new GameObject[childCount];
        for (int i = 0; i < childCount; i++)
        {
            plateformChild[i] = plateforme.transform.GetChild(i).GetChild(0).gameObject;
            //Debug.Log("Child name in Start: " + plateformChild[i].name);
        }
        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnCollisionEnter(Collision collision)
    {

        rb.velocity = Vector3.up * jumpForce;

        AnimateJump();

        if (twice == 2)
        {
            twice = 0;
        }

        FindNextPlaterform(collision);

        //Debug.Log("twice in collision = " + twice);


    }

    private void ManageLevel()
    {
        canvas.gameObject.SetActive(true);
        isCanvasActive = true;


    }

    private void FindNextPlaterform(Collision collision)
    {
        for (int i = 0; i < plateformChild.Length; i++)
        {
            if (collision.gameObject.tag == "plateforme" && collision.gameObject == plateformChild[i] && !isCanvasActive)
            {


                nextPlateforme = i < plateformChild.Length-1 ?  plateformChild[i + 1] : null;

                //plateformChild[i].gameObject.tag = "Untagged";

                twice++;

                if (!Input.GetMouseButton(0) && twice == 2)
                {
                    //Invoke("lookNextPlateforme", 1.5f);
                    lookNextPlateforme();
                }
                
                if(nextPlateforme == null)
                {
                    ManageLevel();
                    Debug.Log("Done");
                }

                //Debug.Log("twice in loop = " + twice);


                /* Debug.Log("i = " + i);
                 Debug.Log(" plateforme Count in loop = " + plateformChild.Length);

                 Debug.Log("current plateforme name  = " + plateformChild[i].name);
                 Debug.Log("next plateforme name = " + (nextPlateforme != null ? nextPlateforme.name : "None"));*/

                break;
            }

        }
    }

    private void AnimateJump()
    {
        isJumping = true;
        animator.SetBool("isJumping", true);
        animator.SetBool("isFliping", true);
        Debug.Log(plateformChild.Length + " = lenght");

        Invoke("startFlipping", 0.8f);
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
        Invoke("loadCurrentScene", 1f);
    }

    private void loadCurrentScene()
    {
        SceneManager.LoadScene(currentLevel);
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0) && !isCanvasActive)
        {
            dragOrigin = Input.mousePosition;
            //rb.velocity = Vector3.forward * moveSpeed * Time.deltaTime;
        }

        if (Input.GetMouseButton(0) && isJumping && !isCanvasActive)
        {
            dragDirection = (Input.mousePosition - dragOrigin).normalized;

            RotateToTarget();

            //rb.AddRelativeForce( moveSpeed * Time.deltaTime * Vector3.forward , ForceMode.VelocityChange);

            rb.velocity += transform.forward * moveSpeed * Time.deltaTime;

        }

    }
    void RotateToTarget()
    {

        float rotationAmount = dragDirection.x * rotationSpeed ;

        vcam.transform.Rotate(0f, rotationAmount, 0f, Space.World);

        transform.Rotate(0f, rotationAmount, 0f, Space.World);

    }

    void lookNextPlateforme()
    {

        if (nextPlateforme != null)
        {
            Vector3 directionToNextPlatform = nextPlateforme.transform.position - transform.position;
            directionToNextPlatform.y = 0; 

            Quaternion targetRotation = Quaternion.LookRotation(directionToNextPlatform);


            //transform.rotation = targetRotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.8f);


            // Calculate the camera's new rotation without changing its y position

            Vector3 vcamRotateEulerAngles = new Vector3(vcam.transform.rotation.eulerAngles.x, targetRotation.eulerAngles.y, vcam.transform.rotation.eulerAngles.z);
            Quaternion vcamRotateQuaternion = Quaternion.Euler(vcamRotateEulerAngles);


           vcam.transform.rotation = Quaternion.Slerp(vcam.transform.rotation, vcamRotateQuaternion, 0.8f);
           //vcam.transform.rotation =  vcamRotateQuaternion;
        }

    }

}




