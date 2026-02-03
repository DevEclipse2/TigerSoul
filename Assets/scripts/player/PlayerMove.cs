using System.Net;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class PlayerMove : MonoBehaviour
{
    /*
     this also deals with various input functions
    honestly should move the movement code
     
     */



    public float moveSpeed = 5f;         // Player movement speed
    public float jumpForce = 5f;         // Force applied to jump
    public LayerMask groundLayer;         // Layer for the ground
    public Transform groundCheck;         // Transform to check if the player is grounded
    public Transform LeftWall;         // Transform to check if the player is grounded
    public Transform RightWall;         // Transform to check if the player is grounded
    public float groundCheckRadius = 0.2f; // Radius for ground check
    public GameObject attackRoot;
    DealDamage damagescript;
    public Rigidbody2D rb;
    private bool isGrounded = false;
    private bool contactLeft = false;
    private bool contactRight = false;
    private Vector2 movementInput;
    byte lastWall = 0; //no last wall
    float targetSpeed;
    public Vector2 GroundCheckArea;
    public float DashAmt = 3.0f;
    public float DashTime = 0.3f;
    float timer;
    public bool canMove = true;
    bool dashing = true;
    bool isLeft = false;
    public Vector2 DashTarget;
    public Vector2 moveDir;
    bool moving = true;
    public GameObject InputController;
    InputParser parser;
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        parser = InputController.GetComponent<InputParser>();
        damagescript = attackRoot.GetComponent<DealDamage>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (!canMove && dashing) {
            if (timer >= DashTime)
            {
                dashing = false;
                canMove = true;
            }
            else
            {

            }

        }

        if (parser.recentInput.IndexOf(Input.Move) != -1) {
            if (!parser.ongoing[parser.recentInput.IndexOf(Input.Move)])
            {
                if (GroundCheck())
                {

                    rb.linearVelocity = new Vector2(rb.linearVelocity.x * 0.95f, rb.linearVelocity.y);
                }
            }
        
        
        }  
        if (moveDir.x < 0)
        {
            //left
            isLeft = true;
            damagescript.isleft = true;
        }
        else if( moveDir.x > 0)
        {
            isLeft = false;
            damagescript.isleft = false;
        }
    }
    public bool GroundCheck()
    {
        return Physics2D.BoxCast(groundCheck.position, GroundCheckArea , 0 , Vector2.down,0, groundLayer);
    }
    public void OnDash()
    {
        timer = 0;
        canMove = false;
        dashing = true;
        RaycastHit2D geometry;
        if (isLeft)
        {
            if(!(geometry = Physics2D.BoxCast(this.gameObject.transform.position, new Vector2(2.16f,1.18f), 0, Vector2.left , DashAmt)))
            {
                DashTarget = this.gameObject.transform.position + new Vector3(-DashAmt, 0, 0);
            }
            else
            {
                DashTarget = geometry.collider.ClosestPoint(this.gameObject.transform.position);
            }
            
        }
        else{
            DashTarget = this.gameObject.transform.position + new Vector3(DashAmt, 0, 0);
        }
    }
    public void OnMove(InputValue value)
    {
        //Debug.Log("move");
        //float moveInput = Input.GetAxis("Horizontal");

        //if (value.isPressed)
        //{
        if (canMove)
        {
            moveDir = value.Get<Vector2>();
            rb.linearVelocity = new Vector2(moveDir.x * moveSpeed, rb.linearVelocity.y);
        }


        //}
    }

    public void OnJump(InputValue value)
    {
        float basespeed = Mathf.Abs(rb.linearVelocity.x);
        isGrounded = GroundCheck();
        RaycastHit2D leftWallCheck = Physics2D.Raycast(LeftWall.position, Vector2.right * -1, groundCheckRadius + 0.04f * basespeed, groundLayer);
        RaycastHit2D rightWallCheck = Physics2D.Raycast(RightWall.position, Vector2.right , groundCheckRadius + 0.04f * basespeed, groundLayer);
        contactLeft = (leftWallCheck.collider != null);
        contactRight = (rightWallCheck.collider != null);
        if (isGrounded)
        {
            lastWall = 0;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            //rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
        else if (contactLeft || contactRight)
        {
            
            float verticalvelocity = rb.linearVelocity.y;
            if (verticalvelocity < 0) { 
                verticalvelocity = 0f;
            }

            if((targetSpeed = moveSpeed * 0.6f + basespeed * 0.8f ) > 15)
            {
                targetSpeed = 15;
            }
            if (contactRight && (lastWall == 0 || lastWall == 2)) {
                lastWall = 1;
                rb.linearVelocity = new Vector2(-targetSpeed, verticalvelocity + jumpForce * 0.75f);
            }
            else if (contactLeft && (lastWall == 0 || lastWall == 1))
            {
                lastWall = 2;
                rb.linearVelocity = new Vector2(targetSpeed, verticalvelocity + jumpForce * 0.75f);
            }
        }
    }
    //public void OnMove(InputAction.CallbackContext context)
    //{
    //    rb.linearVelocity = new Vector2(moveDir.x * moveSpeed, rb.linearVelocity.y);

    //    if (context.performed)
    //    {
    //        moving = true;
    //    }
    //    if (context.canceled )
    //    {
    //        moving = false;
    //    }
    //}
}