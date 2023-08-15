using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMover : MonoBehaviour
{
    [SerializeField] float xMove = 0f;
    [SerializeField] float yMove = 0f;
    [SerializeField] float zMove = 0f;

    [SerializeField] float distance = 8f;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        float time = Mathf.PingPong(Time.time, distance * 2) - distance; // PingPong function for smooth movement

        Vector3 newPosition = initialPosition + new Vector3(xMove, yMove, zMove) * time;
        transform.position = newPosition;
    }
}
