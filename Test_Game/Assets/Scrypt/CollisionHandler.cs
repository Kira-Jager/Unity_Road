using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    Rigidbody rb;
    MeshRenderer obj;
    public GameObject[] ground;

    private void OnCollisionEnter(Collision collision)
    {
        obj = collision.gameObject.GetComponent<MeshRenderer>();
        if (obj.gameObject.tag == "ground")
        {
            obj.material.color = Color.red;
        }

        ground = GameObject.FindGameObjectsWithTag("ground");
      
    }
}
