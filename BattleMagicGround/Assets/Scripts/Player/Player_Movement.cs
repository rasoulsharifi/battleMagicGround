using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    // variables
    [SerializeField] private float playerSpeed;

    // references
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(x, 0, z);
        transform.position += movement * Time.deltaTime * playerSpeed;

           

        if (movement != Vector3.zero)
        {
            anim.SetBool("run", true);

            transform.rotation = Quaternion.Slerp
                (transform.rotation, Quaternion.LookRotation(movement), .3f);
        }
        else
        {
            anim.SetBool("run", false);
        }

        
     }
}
