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
    private Player_Movement pl_movement;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        cam = Camera.main;
        pl_movement = GetComponent<Player_Movement>();
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

        RaycastHit hit;
        var projectile = Instantiate(bulletPrefab, gun.position, Quaternion.identity);

        PlayerProjectile pl_projectile = projectile.GetComponent<PlayerProjectile>();
        
        //pl_projectile.GetComponent<PlayerProjectile>().target = projectile.transform.position + cam.transform.forward;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, Mathf.Infinity))
        {
            pl_projectile.target = hit.point;
        }
        else
        {
            pl_projectile.target = cam.transform.position + cam.transform.forward * 400;
        }                
        
        anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 0);        
    }


    private void Fire()
    {
        /*Vector2 center = new Vector2(Screen.width / 2, Screen.height / 2);
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(center);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue, 2);
*/

    }

}
