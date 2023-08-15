using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float Thrust = 2f  ;
   

    [SerializeField] float RotateAngle = 0f;
    
    Rigidbody rb;
    AudioSource audio;
    bool isPlaying = false;
   
    public AudioClip rocket;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
        audio.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        


        if(Input.GetKey(KeyCode.D) )
        {
            transform.Rotate(- moveSpeed * RotateAngle * Time.deltaTime * Vector3.forward );
        };
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(- moveSpeed * RotateAngle * Time.deltaTime * Vector3.back );
        };
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(0,1 * Thrust * moveSpeed * Time.deltaTime, 0) ;
            audio.enabled = true;
            if (!audio.isPlaying)
                audio.PlayOneShot(rocket, 1f);
            isPlaying = true;
        };
        if(isPlaying && !Input.GetKey(KeyCode.Space))
        audio.Stop();
       

    }
}
