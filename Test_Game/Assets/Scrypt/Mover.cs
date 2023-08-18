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
    private Vector2 previousMousePos;
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
        /*if (Input.GetMouseButtonUp(0))
        {
            firstMousePos = Input.mousePosition;
        };

      if (Input.GetMouseButtonDown(0))
        {
            secondMousePos = Input.mousePosition;
            currentMousePos = secondMousePos - firstMousePos;
        };
        currentMousePos.Normalize();*/

        /*xMouse = currentMousePos.x;
        yMouse = currentMousePos.y;*/

        /* if (Input.GetMouseButtonDown(0))
         {
             firstMousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
         }

         if(Input.GetMouseButtonUp(0))
         {
             secondMousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
             currentMousePos = secondMousePos - firstMousePos;
         }
 */

        if (Input.GetMouseButtonDown(0))
        {
            previousMousePos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 currentMousePos = Input.mousePosition;
            Vector2 deltaMousePos = currentMousePos - previousMousePos;
            // Use deltaMousePos for movement calculations
            xMouse = deltaMousePos.x;
            yMouse = deltaMousePos.y;
            // Update previousMousePos for the next frame
            previousMousePos = currentMousePos;
        }

        if (Mathf.Abs(xMouse) > Mathf.Abs(yMouse))
        {
            if (xMouse > 0)
            {
                rb.AddForce ( Vector3.right * speed);
            }
            else if (xMouse < 0)
            {
                rb.AddForce(Vector3.left * speed);

            }
        }
        else
        {
            if (yMouse > 0)
            {
                rb.AddForce(Vector3.forward * speed);
            }
            else if (yMouse < 0)
            {
                rb.AddForce(Vector3.back * speed);
            }
        }



    }
}
