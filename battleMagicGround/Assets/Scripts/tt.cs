using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tt : MonoBehaviour
{
    // variables
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    private float moveSpeed;

    private Vector3 moveDirection;

    // references
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        Move();
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        moveDirection = new Vector3(x, 0, y);

        if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
        {
            Walk();
        }
        else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
        {
            Run();
        }
        else
        {
            Idle();
        }

        moveDirection *= moveSpeed;
        transform.position += moveDirection * Time.deltaTime;
        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp
                (transform.rotation, Quaternion.LookRotation(moveDirection), .3f);
        }

    }
    private void Idle()
    {
        moveSpeed = 0;
        anim.SetFloat("moveSpeed", 0, .1f, Time.deltaTime);
    }
    private void Walk()
    {
        moveSpeed = walkSpeed;
        anim.SetFloat("moveSpeed", .5f, .1f, Time.deltaTime);
    }
    private void Run()
    {
        moveSpeed = runSpeed;
        anim.SetFloat("moveSpeed", 1, .1f, Time.deltaTime);
    }
    private void Jump()
    {

    }




}
