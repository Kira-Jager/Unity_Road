using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDraw : MonoBehaviour
{
    private LineRenderer line;
    private Vector3 previousMousePos;

    public float minDistance = 0.2f;



    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        previousMousePos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 currentMousePos =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentMousePos.y = 0;

            if(Vector3.Distance(currentMousePos,previousMousePos) < minDistance)
            {
                line.positionCount++;
                line.SetPosition(line.positionCount - 1, currentMousePos);
            }
        }
    }
}

