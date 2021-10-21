using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    // variables    


    // references
    [SerializeField] private Transform gun;
    [SerializeField] private GameObject bulletPrefab;
    private Camera cam;
    private Animator anim;


    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        cam = Camera.main;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 1);
        anim.SetTrigger("attack");

        yield return new WaitForSeconds(1f);

        var bullet = Instantiate(bulletPrefab, gun.position, Quaternion.identity);
        bullet.GetComponent<PlayerBullet>().target = cam.transform.forward;
        anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 0);

    }
}
