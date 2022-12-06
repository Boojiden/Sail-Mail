using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public GameObject pivot;
    public GameObject player;
    private bool isOnGround;
    private bool isSliding;
    private bool isDirecting;
    private Animator anim;
    private SpriteRenderer sr;
    private PlayerMovement pm;
    private SpriteRenderer pivotChild;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        pm = player.GetComponent<PlayerMovement>();
        pivotChild = pivot.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        isOnGround = pm.isGrounded();
        isSliding = pm.isSlide();
        isDirecting = pm.isDirecting();

        anim.SetBool("IsSliding", isSliding);
        anim.SetBool("IsOnGround", isOnGround);
        anim.SetBool("IsDirecting", isDirecting);

        //Debug.Log(pivot.transform.rotation.eulerAngles.z);
        if(pivot.transform.rotation.eulerAngles.z <= 180)
        {
            sr.flipX = false;
            pivotChild.flipY = false;
        }
        else
        {
            sr.flipX = true;
            pivotChild.flipY = true;
        }
    }
}
