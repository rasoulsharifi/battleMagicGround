using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{

    // variables
    public float bulletSpeed;

    // references
    [HideInInspector] public Vector3 target;


    void Start()
    {
        
    }


    void Update()
    {
        transform.position += target * Time.deltaTime * bulletSpeed;
    }
}
