using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool isJumping = false;

    private Rigidbody rb;

    public float jumpForce = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }
}
