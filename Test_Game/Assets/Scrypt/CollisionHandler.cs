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
            obj.tag = "finish";
        }

        ground = GameObject.FindGameObjectsWithTag("ground");

        for (int i = 0; i<= ground.Length; i++)
        {
            break;
        }


        /*else if (obj.gameObject.tag == "wall")
        {
            obj.material.color = Color.blue;
        }*/
    }
}
