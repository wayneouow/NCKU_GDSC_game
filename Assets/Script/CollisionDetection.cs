using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public GameObject HitParticle;
    public PlayerMovement pm;
    

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy" && pm.isAttacking)
        {
            GetComponent<SphereCollider>().enabled = false;
            Debug.Log("hit " + other.name);
            GetComponent<AudioSource>().Play();
            other.GetComponent<Animator>().SetTrigger("Hit");
            other.GetComponent<EnemySystem>().HP = other.GetComponent<EnemySystem>().HP <=0 ? 0 : other.GetComponent<EnemySystem>().HP - pm.atk;
            if(other.GetComponent<EnemySystem>().HP<=0)
                other.GetComponent<EnemySystem>().alive = false;
            Instantiate(HitParticle, new Vector3(other.transform.position.x, other.transform.position.y + 1.5f, other.transform.position.z), other.transform.rotation);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        
    }
    private void Update()
    {
        if (!pm.isAttacking)
        {
            GetComponent<SphereCollider>().enabled = !pm.isAttacking;
        }
    }
}
