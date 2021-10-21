using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{    
    [Header("Movement")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [HideInInspector] public Vector3 moveDirection;
    private float moveSpeed;
    
    [Header("Rotation")]
    [SerializeField] private float turnSmoothTime;
    private float turnSmoothVelocity;
    
    [Header("Jump")]    
    [SerializeField] private float groundDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpHeight;
    private bool isGrounded;
    private Vector3 velocity;
        

    // references
    private CharacterController controller;
    private Transform cam;
    private Animator anim;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main.transform;
        anim = GetComponentInChildren<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Move();

        
    }

    private void Move()
    {        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        moveDirection = new Vector3(horizontal, 0, vertical).normalized;


        if (moveDirection.magnitude >= .1f && !Input.GetKey(KeyCode.LeftShift))
        {
            Walk();
        }
        else if (moveDirection.magnitude >= .1f && Input.GetKey(KeyCode.LeftShift))
        {
            Run();
        }
        else if (moveDirection.magnitude == 0)
        {
            Idle();
        }

        isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        else
        {
            anim.SetFloat("moveSpeed", 0);
        }

        moveDirection *= moveSpeed;

        if (moveDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle,
                ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void Idle()
    {
        moveSpeed = 0;
        anim.SetFloat("moveSpeed", 0f, .1f, Time.deltaTime);
    }

    void Walk()
    {
        moveSpeed = walkSpeed;
        anim.SetFloat("moveSpeed", .5f, .1f, Time.deltaTime);
    }
    void Run()
    {
        moveSpeed = runSpeed;
        anim.SetFloat("moveSpeed", 1f, .1f, Time.deltaTime);
    }

    void Jump()
    {        
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);    
    }
   
}

