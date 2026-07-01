///
/// movement options include
/// walljumping
/// doublejumping
/// dashing
/// crouch / sliding

using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public bool canMove;
    public float moveSpeed = 5f;         // Player movement speed
    public float jumpForce = 5f;         // Force applied to jump
    [SerializeField]
    GameObject damageroot;
    DealDamage damagescript;
    [SerializeField]
    Animator animator;

    public LayerMask groundLayer;         // Layer for the ground
    public Transform groundCheck;         // Transform to check if the player is grounded
    public float groundCheckRadius = 0.2f; // Radius for ground check
    public Vector2 moveDir;
    public Vector2 GroundCheckArea;

    public GameObject dashObject;
    public GameObject walljumpObject;
    public GameObject doublejumpObject;

    private dash dashModule;
    private wallJump walljumpModule;
    private DoubleJump doublejumpModule;
    private Stomp stompModule;
    public GameObject InputController;
    InputParser parser;
    public Rigidbody2D rb;

    public bool isLeft = false;
    public bool targLeft = false;
    public bool Damage = false;
    //for dash
    public void rechargeDash() { dashModule.cooldown(); }
    public void resetWallJump() { walljumpModule.walljumping = false; }
    public void changeMove(bool value) { canMove = value; }
    public void excludeDash(bool  value) { dashModule.Active = value; }
    public void excludeDouble(bool  value) { doublejumpModule.Active = value; }

    //for walljump
    public void resetLastWall() { walljumpModule.lastWall = 0; walljumpModule.walljumping = false; }


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        parser = InputController.GetComponent<InputParser>();
        damagescript = damageroot.GetComponent<DealDamage>();
        if (upgradeLoader.Dash)
        {
            dashModule = dashObject.GetComponent<dash>();
            dashModule.Active = true;
            dashModule.Available = true;
            dashModule.init();
        }
        if (upgradeLoader.wallJump)
        {
            walljumpModule = walljumpObject.GetComponent<wallJump>();
            walljumpModule.Active = true;
            walljumpModule.init();
        }
        if (upgradeLoader.DoubleJump)
        {
            doublejumpModule           = doublejumpObject.GetComponent<DoubleJump>();
            doublejumpModule.Active    = true;
            doublejumpModule.Available = true;
            doublejumpModule.init();
        }

    }
    public bool GroundCheck()
    {
        return Physics2D.BoxCast(groundCheck.position, GroundCheckArea, 0, Vector2.down, 0, groundLayer);
    }
    public void OnMove(InputValue value)
    {
        
        moveDir = value.Get<Vector2>();
        if (canMove)
        {
            rb.linearVelocity = new Vector2(moveDir.x * moveSpeed, rb.linearVelocity.y);
        }
    }

    public void OnJump(InputValue value)
    {
        bool isGrounded = GroundCheck();
        if(dashModule.dashing)
        {
            return;
        }
        if (isGrounded)
        {
            walljumpModule.lastWall = 0;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            doublejumpModule.Available = true;
            //rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
        else
        {
            switch(walljumpModule.useAbility())
            { case -1:break; case 1:break; case 2: doublejumpModule.useAbility(); break; }
        }
    }

    public void OnDash()
    {
        dashModule.useAbility();
    }
    public void OnCrouch()
    {
        stompModule.useAbility();
    }
    // Update is called once per frame
    void Update()
    {
        if (GroundCheck())
        {
            dashModule.cooldown();
        }
        dashModule.dashtick();
        if (parser.recentInput.IndexOf(Input.Move) != -1)
        {
            if (!parser.ongoing[parser.recentInput.IndexOf(Input.Move)])
            {
                if (GroundCheck())
                {
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x * 0.95f, rb.linearVelocity.y);
                }
            }
            else
            {
                if (!walljumpModule.walljumping)
                {
                    
                    if (!Damage && canMove)
                    {

                        rb.linearVelocity = new Vector2(moveDir.x * moveSpeed, rb.linearVelocity.y);
                        //Debug.Log(rb.linearVelocityX);
                    }
                    else
                    {
                        float velx = rb.linearVelocity.x + moveDir.x * moveSpeed * 0.6f * Time.deltaTime;
                        rb.linearVelocity = new Vector2(Mathf.Clamp(velx, -15, 15), rb.linearVelocityY);


                    }
                }
            }
            
        }
       
        if (moveDir.x < 0)
        {
            //left
            isLeft = true;
            damagescript.isleft = true;
        }
        else if (moveDir.x > 0)
        {
            isLeft = false;
            damagescript.isleft = false;
        }
    }
}
