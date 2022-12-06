using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Properties")]
    public float maxSpeed;
    public float accel;
    public float fallSpeed;
    public float jumpHeight;
    public float linearDrag;
    public float maxAngleWalk;

    [Space]
    [Header("Collision Attributes")]
    public LayerMask ground;
    //public LayerMask enemy;
    public float groundLength;
    public float groundSpace;

    [Space]
    [Header("Physics Material")]
    public PhysicsMaterial2D noFric;
    public PhysicsMaterial2D Fric;
    public bool grounded
    {
        get { return onGround; }
    }
    [Header("Cosmetic")]
    public ParticleSystem slide;


    private bool canjump;
    private bool onGround;
    private bool isSliding;

    public bool isOnSlope;
    public bool canWalkOnSlope;
    private Vector2 slopeNormalPerp;
    public Rigidbody2D rb { get; private set; }
    //private Animator anim;
    private SpriteRenderer r;
    private CapsuleCollider2D cap;

    private float dir;
    private Vector2 totalForce ;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        r = transform.GetComponentInChildren<SpriteRenderer>();
        cap = GetComponent<CapsuleCollider2D>();
        //anim = GetComponent<Animator>();
    }
    void Update()
    {
        checkSlope();
        onGround = Physics2D.Raycast(transform.position, -transform.up, groundLength, ground);
        canjump = onGround;
        isSliding = onGround && Input.GetAxis("Vertical") < 0f;
        dir = isSliding ? 0f : !onGround ? Input.GetAxis("Horizontal")* 0.20f : Input.GetAxis("Horizontal");
        if (canjump && Input.GetButtonDown("Jump"))
        {
            doJump();
        }
    }

    private void checkSlope()
    {
        Vector2 checkPos = (Vector2)transform.position -  new Vector2(0, cap.size.y / 5);

        checkVertical(checkPos);
        checkWalk(checkPos);
    }

    private void checkWalk(Vector2 checkPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.right * Mathf.Sign(dir), groundLength, ground);
        if (hit)
        {
            isOnSlope = true;

            RaycastHit2D check = Physics2D.Raycast(new Vector2(hit.point.x, hit.point.y + 1f), Vector2.down, 1.1f, ground);
            Vector2 norm = Vector2.Perpendicular(check.normal).normalized;
            if (norm != slopeNormalPerp)
            {
                slopeNormalPerp = norm;
            }
        }
    }

    private void checkVertical(Vector2 checkPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos, -transform.up, groundLength, ground);
        float slopeDownAngle = 0f;
        if (hit)
        {

            slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;

            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);
            if (slopeDownAngle != 0f)
            {
                isOnSlope = true;
            }
            else
            {
                isOnSlope = false;
            }

            Debug.DrawRay(hit.point, slopeNormalPerp, Color.blue);
            Debug.DrawRay(hit.point, hit.normal, Color.green);
            Debug.DrawRay(checkPos, Vector2.right, Color.red);

        }
        else
        {
            isOnSlope = false;
        }

        if (slopeDownAngle > maxAngleWalk)
        {
            canWalkOnSlope = false;
        }
        else
        {
            canWalkOnSlope = true;
        }
    }

    public void FixedUpdate()
    {
        doMovement();
    }

    public void doJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
    }
    public void doMovement()
    {
        bool changedir = Mathf.Sign(rb.velocity.x) != Mathf.Sign(dir);
        var direction = dir;
        if (!onGround)
        {
            direction *= 0.5f;
        }
        Vector2 newForce = new Vector2(direction * accel, 0f);
        if (isOnSlope && canWalkOnSlope && onGround)
        {
            if(rb.velocity.magnitude < maxSpeed)
            newForce.Set(accel * slopeNormalPerp.x * -direction, accel * slopeNormalPerp.y * -direction);
        }
        if ((rb.velocity.magnitude > maxSpeed || rb.velocity.magnitude < -maxSpeed) && onGround)
        {
            if (changedir)
            {
                rb.AddForce(newForce);
            }
        }
        else if ((rb.velocity.x > maxSpeed || rb.velocity.x < -maxSpeed) && !onGround)
        {
            if (changedir)
            {
                rb.AddForce(newForce);
            }
        }
        else
        {
            rb.AddForce(newForce);
        }
        float absX = Mathf.Abs(rb.velocity.x);
        float absY = Mathf.Abs(rb.velocity.y);
        //rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * speed, rb.velocity.y);
        if (absX < 0.5f && absY < 2f && Mathf.Abs(dir) < 0.1f)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        if (rb.velocity.y < -fallSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, -fallSpeed);
        }
        if (onGround && !isSliding)
        {
            rb.sharedMaterial = Fric;
        }
        else
        {
            rb.sharedMaterial = noFric;
        }

        if(isSliding && !slide.isPlaying)
        {
            slide.Play();
        }
        else if(!isSliding && slide.isPlaying)
        {
            slide.Stop();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        //Gizmos.DrawLine(new Vector3(transform.position.x, transform.position.y), new Vector3(transform.position.x, transform.position.y - groundLength));
    }

    public bool isGrounded()
    {
        return onGround;
    }

    public bool isSlide()
    {
        return isSliding;
    }

    public bool isDirecting()
    {
        return Math.Abs(dir) > 0.1f;
    }
}
