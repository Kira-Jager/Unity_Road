using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Mover : MonoBehaviour
{
    public float speed;
    float xMove;
    float zMove;

    public float xMouse;
    public float yMouse;
    private MouseMoveEvent mouse;

    Rigidbody rb;

    private Vector2 currentMousePos;
    private Vector2 firstMousePos;
    private Vector2 secondMousePos;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveWithKeyboard();
        MoveWithMouse();
    }

    private void MoveWithKeyboard()
    {
        xMove = Input.GetAxis("Horizontal");
        zMove = Input.GetAxis("Vertical");
        transform.Translate(xMove * speed * Time.deltaTime, 0, zMove * speed * Time.deltaTime);
    }
    
    private void MoveWithMouse()
    {
        if (Input.GetMouseButtonUp(0))
        {
            firstMousePos = Input.mousePosition;
        };

      if (Input.GetMouseButtonDown(0))
        {
            secondMousePos = Input.mousePosition;
            currentMousePos = secondMousePos - firstMousePos;
            /*transform.Translate(currentMousePos.x * speed * Time.deltaTime, 0, currentMousePos.y * speed * Time.deltaTime);*/
        };
        currentMousePos.Normalize();

        xMouse = currentMousePos.x;
        yMouse = currentMousePos.y;

        if(xMouse > 0 && yMouse > -0.5f && yMouse < 0.5f)
        {
            rb.velocity = Vector3.back * speed;
        }
        else if(xMouse < 0 && yMouse > -0.5f && yMouse < 0.5f)
        {
            rb.velocity = Vector3.forward * speed;
        }
        else if(yMouse > 0 && xMouse > -0.5f && xMouse < 0.5f)
        {
            rb.velocity = Vector3.left * speed;
        }
        else if(yMouse < 0 && xMouse > -0.5f && xMouse < 0.5f)
        {
            rb.velocity = Vector3.right * speed;
        }





    }
}
