using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // variables
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private Vector3 offset;
    Vector3 velocity = Vector3.zero;
    Vector3 lastPlayerPosition;
    [SerializeField] private bool lookAtPlayer = false;
    [Range(0.01f, 1f)]
    [SerializeField] private float smoothFactor = 5.5f;
    private float xRotation;

    // references    
    private GameObject player;



    

    


    void Start()
    {
        // offset = transform.position - player.position;
        try
        {
            player = GameObject.FindWithTag("Player");
        }
        catch (System.NullReferenceException ex)
        {
            print("player not founded");
            return;
        }
        
        //Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        if (player == null) return;

        Vector3 newPos = player.transform.position + offset;
        //   transform.position = Vector3.Slerp(transform.position, newPos, smoothFactor * Time.deltaTime);
        //  transform.position = newPos;
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, smoothFactor);
        if (lookAtPlayer)
            transform.LookAt(player.transform);

        Rotate();
    }

    private void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        transform.Rotate(Vector3.up * mouseX);
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }
}
