using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUp : MonoBehaviour
{

    [SerializeField] float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float MoveX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float MoveZ = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        transform.Translate(MoveX * -1, MoveZ,0 );
    }
}