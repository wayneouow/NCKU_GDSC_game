using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed = 5f;
    public float sprintSpeed = 7f;
    [SerializeField] private float speedMuti = 10f;
    public float groundDrag = 5f;

    [Header("Jumping")]
    public float jumpForce = 7f;
    public float jumpCooldown = 0.2f;
    public float airMultiplier = 0.2f;
    bool readyToJump;

    [Header("Crouching")]
    public float crouchSpeed = 3f;
    [SerializeField] private float downMuti = 5f;
    public float crouchYScale = 0.5f;
    private float startYScale;

    [Header("Key")]
    public KeyCode jumpkey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask Ground;
    bool grounded;

    [Header("Attack")]
    public GameObject atkZone;
    public bool canAttack = true;
    public float AttackCoolDown = 0.7f;
    public bool isAttacking = false;
    public int atk = 30;
    
    [Header("Others")]
    public Transform orientation;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    Rigidbody rb;
    public GameObject respawn;

    public GameObject GroundSpawnParticle;
    public GameObject NewGround;
    public GameObject GamePause;
    bool esc = false;
    Animator animator;
    //Animation State
    bool run = false;

    public PlayerValue pv;

    public enum MovementState
    {
        idle,
        walking,
        sprinting,
        crouching,
        air
    }

    public MovementState state;
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;

        startYScale = transform.localScale.y;

        animator = GetComponent<Animator>();

    }
    private void FixedUpdate() 
    {
        MovePlayer();    
    }
    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, Ground);
        UserInput();
        SpeedControl();
        StateHandler();
        if(state == MovementState.crouching)
        {
            rb.drag = 0.1f;
        }
        else if(grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        animatorSet();

        if (transform.position.y < 0.001f)
        {
            transform.position = new Vector3(transform.position.x, 0.1f , transform.position.z);
            Instantiate(NewGround, new Vector3(transform.position.x, 0f, transform.position.z), transform.rotation);
            Instantiate(GroundSpawnParticle, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
        }
        
    }

    private void UserInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        
        if(Input.GetButton("Jump") && readyToJump && grounded)//GetKey(jumpkey)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(JumpReset), jumpCooldown);
        }

        if(Input.GetKeyDown(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * downMuti, ForceMode.Impulse);
        }
        if(Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }

        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            isAttacking = true;
            canAttack = false;
            Attack();
            Invoke(nameof(AttackReset), AttackCoolDown);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            esc = !esc;
            if (esc)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0;
                GamePause.gameObject.SetActive(esc);
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1;
                GamePause.gameObject.SetActive(esc);
            }
            
        }

    }
    private void StateHandler()
    {
        run = false;
        if(Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
            run = false;
        }
        else if(grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
            run = true;
        }
        else if(grounded && rb.velocity.magnitude <= 0.01)
        {
            state = MovementState.idle;
        }
        else if(grounded )
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
            run = false;
        }
        else
        {
            state = MovementState.air;
            run = false;
        }

    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * speedMuti, ForceMode.Force);
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * speedMuti * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 Vel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if(Vel.magnitude > moveSpeed )
        {
            Vector3 limitedVel = Vel.normalized * moveSpeed;
            rb.velocity = new UnityEngine.Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void JumpReset()
    {
        readyToJump = true;
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");

        Invoke(nameof(AttackStop), 0.8f * AttackCoolDown);
    }
    public void Damage(EnemySystem es)
    {
        es.HP = (es.HP <= 0) ? 0 : es.HP - atk;
        if (es.HP <= 0)
        {
            es.alive = false;
        }
    }
    private void AttackReset()
    {
        canAttack = true;
    }
    private void AttackStop()
    {
        isAttacking = false;
    }
    private void animatorSet()
    {
        animator.SetBool("Run", run);
        animator.SetInteger("movementSt", (int)state);

    }
}
