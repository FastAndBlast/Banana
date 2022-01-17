using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Speed")]
    public float walkingSpeed;
    public float runningSpeed;
    public float jumpSpeed;
    public float slideSpeed;

    [Header("Cooldowns")]
    public float jumpCooldown;
    public float slideCooldown;
    float jumpCooldownCurrent;
    float slideCooldownCurrent;

    [Header("Misc")]
    public int wallJumpsLeft;

    public bool running;

    public bool crouching;

    public bool touchingGround;

    public float moveCooldown = 0;

    public float fallMultiplier = 2.5f;

    [Header("Enable Unity physics")]
    public bool forceBasedMovement = true;

    Rigidbody rb;

    public bool on = true;

    Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (!on)
        {
            return;
        }

        Vector3 movementVector = transform.forward * Input.GetAxisRaw("Vertical") + transform.right * Input.GetAxisRaw("Horizontal");
            //new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (moveCooldown > 0)
        {
            moveCooldown -= Time.deltaTime * GameManager.timeScale;
        }

        if (forceBasedMovement)
        {
            if (moveCooldown <= 0)
            {
                rb.AddForce(movementVector * walkingSpeed * Time.deltaTime * GameManager.timeScale);
            }
            
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }

            //if (Input.GetButtonDown("Slide"))
            //{
            //    Slide();
            //}

            if (Input.GetButton("Crouch"))
            {
                if (running)
                {
                    Slide();
                }
                else 
                {
                    if (!crouching)
                    {
                        Crouch();
                    }
                    crouching = true;
                }
            }
            else
            {
                if (crouching)
                {
                    Uncrouch();
                }
                crouching = false;
            }

        }
        
        if (jumpCooldownCurrent > 0)
        {
            jumpCooldownCurrent -= Time.deltaTime * GameManager.timeScale;
        }

        if (slideCooldownCurrent > 0)
        {
            slideCooldownCurrent -= Time.deltaTime * GameManager.timeScale;
        }


        RaycastHit[] hits = Physics.SphereCastAll(transform.position, 0.25f, Vector3.down, 0);

        //foreach (RaycastHit hit in hits)
        //{
        //    print(hit.transform.name);
        //}

        //print(hits.Length);

        //Grounded check
        if (hits.Length > 1) //Physics.CheckSphere(transform.position - Vector3.up * 0.5f, 0.25f))
        {
            touchingGround = true;
        }
        else
        {
            touchingGround = false;
        }

        anim.SetBool("touchingGround", touchingGround);


        //Crispier jump
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime * GameManager.timeScale;
        }

        anim.SetFloat("speed", rb.velocity.magnitude);

    }

    public void Crouch()
    {

    }

    public void Uncrouch()
    {

    }

    public void Slide()
    {
        if (!running || slideCooldownCurrent > 0)
        {
            return;
        }

        //Vector3 movementVector = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (forceBasedMovement)
        {
            rb.AddForce(transform.forward * slideSpeed);
        }

        slideCooldownCurrent = slideCooldown;

        moveCooldown = 0.4f;
    }

    public void Jump()
    {
        if (jumpCooldownCurrent > 0 || !touchingGround)
        {
            return;
        }

        if (forceBasedMovement)
        {
            rb.AddForce(transform.up * jumpSpeed);
        }

        jumpCooldownCurrent = jumpCooldown;
    }

    public void Die()
    {
        //TODO
        GameManager.instance.Respawn();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, 0.25f);
    }


}
