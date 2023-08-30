using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tromp_colision : MonoBehaviour
{
    public GameObject trompToAnim;
    public GameObject trompToChangeColor;
    Material mat;
    Animation anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = trompToAnim.GetComponent<Animation>();
        mat = trompToChangeColor.GetComponent<SkinnedMeshRenderer>().material;
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {

        Debug.Log("collision");
        if (collision.gameObject.tag == "Player")
        {
            mat.color = Color.green;
            anim.Play("trompoline_animation");
        }

        Invoke("stopAnimation", 0.4f);

    }

    private void stopAnimation( )
    {
        anim.Stop("trompoline_animation");
    }
}
