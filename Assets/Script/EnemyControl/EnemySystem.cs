using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    public bool found = false;
    public bool lookat = false;
    public bool near = false;

    public GameObject player;

    public bool isMoving;
    [SerializeField] float speed = 10;
    [SerializeField] float velLimit = 2;
    [SerializeField] float multiplier = 10;
    public LayerMask Ground;
    [SerializeField] bool grounded;
    [SerializeField] float jumpForce = 1;
    [Header("Health")]
    public int MaxHP = 100;
    public int HP = 100;
    public bool alive = true;
    public GameObject Drop;
    public GameObject DropEffect;
    bool dropped = false;
    [Header("Attack")]
    public bool canAttack = true;
    public float AttackCoolDown = 0.7f;
    public bool isAttacking = false;
    public int atk = 10;
    public GameObject atkParticle;

    Rigidbody rb;
    Animator animator;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        animatorSet();
        if (alive)
        {
            if (found)
                lookat = true;
            if (lookat)
            {
                transform.LookAt(player.transform);
                Vector3 vel = rb.velocity;
                if (near)
                {

                    GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
                    GetComponent<Rigidbody>().AddForce(0.8f * speed * multiplier * Time.deltaTime * transform.forward);

                    //GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
                }
                if (!near && vel.x > -velLimit && vel.x < velLimit && vel.z > -velLimit && vel.z < velLimit)
                {
                    grounded = Physics.Raycast(transform.position, Vector3.down, 1f, Ground);
                    GetComponent<Rigidbody>().AddForce(speed * multiplier * Time.deltaTime * transform.forward);

                    if (grounded)
                    {
                        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
                        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
                    }
                }

            }

            if (rb.velocity.magnitude > 0.2f)
            {
                isMoving = true;
            }
            else
                isMoving = false;

            if (canAttack && near)
            {
                isAttacking = true;
                canAttack = false;
                Attack();
                Invoke(nameof(AttackReset), AttackCoolDown);
            }


        }
        else
        {

            //transform.localScale = new Vector3(transform.localScale.x , transform.localScale.y*0.3f, transform.localScale.z);
            //
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
            //transform.rotation = Quaternion.Euler(new Vector3(0f, transform.rotation.y, 0f));
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
            GetComponent<SphereCollider>().enabled = false;
            if (!dropped)
            {
                dropped = true;
                Instantiate(DropEffect, new Vector3(transform.position.x + Random.Range(-3f, 3f), transform.position.y, transform.position.z + Random.Range(-3f, 3f)), transform.rotation);
                Instantiate(Drop, new Vector3(transform.position.x + Random.Range(-3f, 3f), transform.position.y, transform.position.z + Random.Range(-3f, 3f)), transform.rotation);
            }
            
        }
        
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
        Instantiate(atkParticle, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
        player.GetComponent<PlayerValue>().HP -= atk;
    }
    private void AttackReset()
    {
        canAttack = true;
    }

    private void animatorSet()
    {
        animator.SetBool("Walk", isMoving);
        animator.SetBool("dead", !alive);
    }
}
