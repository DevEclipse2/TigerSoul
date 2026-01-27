using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

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

    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        damagescript = attackRoot.GetComponent<DealDamage>();
    }

    void Update()
    {

    }

    public void OnMove(InputValue value)
    {
        //Debug.Log("move");
        //float moveInput = Input.GetAxis("Horizontal");

        //if (value.isPressed)
        //{
        rb.linearVelocity = new Vector2(moveSpeed * ((Vector2)value.Get<Vector2>()).x, rb.linearVelocity.y);
        if(value.Get<Vector2>().x == 0)
        {
            return;
        }
        if(value.Get<Vector2>().x < 0)
        {
            //left
            damagescript.isleft = true;
            Debug.Log("left");
        }
        else
        {
            damagescript.isleft = false;
            Debug.Log("rigth");

        }
        //}
    }

    public void OnJump(InputValue value)
    {
        float basespeed = Mathf.Abs(rb.linearVelocity.x);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        RaycastHit2D leftWallCheck = Physics2D.Raycast(LeftWall.position, Vector2.right * -1, groundCheckRadius + 0.04f * basespeed, groundLayer);
        RaycastHit2D rightWallCheck = Physics2D.Raycast(RightWall.position, Vector2.right , groundCheckRadius + 0.04f * basespeed, groundLayer);
        contactLeft = (leftWallCheck.collider != null);
        contactRight = (rightWallCheck.collider != null);
        if (isGrounded)
        {
            lastWall = 0;
            Debug.Log("jump");
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
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed || context.canceled)
        {
            movementInput = context.ReadValue<Vector2>();
        }
    }
}