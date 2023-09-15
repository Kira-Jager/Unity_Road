using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    public GameObject line;
    private void OnCollisionEnter(Collision collision)
    {
        Draw draw;
        if (collision.gameObject.CompareTag("Finish"))
        {
            Debug.Log("You win");
        }
    }
}
