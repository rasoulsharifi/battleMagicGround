using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    // variables
    public float projectileSpeed;

    // references
    [HideInInspector] public Vector3 target;


    void Start()
    {

    }


    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, 
            projectileSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, target) < .1f)
        {
            Destroy(this.gameObject);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
    
}
