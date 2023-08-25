using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpMove : MonoBehaviour
{
    public float jumpForce = 10f;
    public float moveSpeed = 10f;

    private Rigidbody rb;
    private Animator animator;


    private bool isJumping = false;
    private Vector3 dragOrigin;
    private Vector3 dragDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "plateforme" )
        {
            rb.velocity = Vector3.up * jumpForce;
            isJumping = true;
            animator.SetBool("isJumping", true);
            animator.SetBool("isFliping", true);
            Debug.Log("jumping");
            Invoke("startFlipping", 0.8f);
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



    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            moveSpeed = 2f;
            dragOrigin = Input.mousePosition;
            rb.velocity = Vector3.forward * moveSpeed;

        }

        if (Input.GetMouseButton(0) && isJumping)
        {
            dragDirection = (Input.mousePosition - dragOrigin).normalized;

            rb.velocity = Vector3.forward * Mathf.Lerp(2f, moveSpeed, 0.75f);
            // Move the character horizontally based on the mouse drag direction
            rb.velocity = new Vector3(dragDirection.x * moveSpeed, rb.velocity.y*moveSpeed, rb.velocity.z);

        }

        /*lookTarget();*/

    }
    void lookTarget()
    {
        if(dragDirection != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(dragDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f);

        }
    }


}
