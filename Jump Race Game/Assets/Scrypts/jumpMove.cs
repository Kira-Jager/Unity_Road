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

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "plateforme")
        {
            rb.velocity = Vector3.up * jumpForce;
            isJumping = true;
            animator.SetBool("isJumping", true);
            animator.SetBool("isFliping", true);
        }
        
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
            dragOrigin = Input.mousePosition;
            rb.velocity = Vector3.forward * moveSpeed;

        }

        if (Input.GetMouseButton(0) && isJumping)
        {
            Vector3 dragDirection = (Input.mousePosition - dragOrigin).normalized;

            // Move the character horizontally based on the mouse drag direction
            rb.velocity = new Vector3(dragDirection.x * moveSpeed, rb.velocity.y, rb.velocity.z);
        }
    }
}
