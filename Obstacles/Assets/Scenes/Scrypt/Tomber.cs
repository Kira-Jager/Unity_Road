using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tomber : MonoBehaviour

{

    [SerializeField] int TimeToWait = 5;
    MeshRenderer meshRenderer;
    Rigidbody rigidbody;



    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        rigidbody = GetComponent<Rigidbody>();

        meshRenderer.enabled = false;
        rigidbody.useGravity = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        timer();
    }

    void timer()
    {
        if (Time.time > TimeToWait)
        {
           // Debug.Log("Timer at " + Time.time
           meshRenderer.enabled = true;
            rigidbody.useGravity = true;
        }
    }
}
