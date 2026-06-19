using System.Collections;
using UnityEngine;

public class wallJump : baseUpgrade
{
    public byte lastWall = 0;
    bool contactLeft = false;
    bool contactRight = false;
    [SerializeField]
    Transform LeftWall;         // Transform to check if the player is grounded
    [SerializeField]
    Transform RightWall;         // Transform to check if the player is grounded
    [SerializeField]
    float groundCheckRadius = 0.2f; // Radius for ground check
    float moveSpeed = 0;
    float jumpForce = 0;
    LayerMask groundLayer;
    float targetSpeed = 0;
    public bool walljumping;

    private Coroutine coroutine;
    public override void init()
    {
        groundCheckRadius = movementscript.groundCheckRadius;
        moveSpeed = movementscript.moveSpeed;
        groundLayer = movementscript.groundLayer;
        jumpForce = movementscript.jumpForce;
    }

    private IEnumerator ClearJump()
    {
        yield return new WaitForSeconds(1.2f);
        lastWall = 0;
        yield return null;
    }

    public override void useAbility()
    {
        if(!Active) return;


        Rigidbody2D rb = movementscript.rb;
        float basespeed = Mathf.Abs(rb.linearVelocity.x);
        RaycastHit2D leftWallCheck = Physics2D.Raycast(LeftWall.position, Vector2.right * -1, groundCheckRadius + 0.04f * basespeed, groundLayer);
        RaycastHit2D rightWallCheck = Physics2D.Raycast(RightWall.position, Vector2.right, groundCheckRadius + 0.04f * basespeed, groundLayer);
        contactLeft = (leftWallCheck.collider != null);
        contactRight = (rightWallCheck.collider != null);
        if (contactLeft || contactRight)
        {
            movementscript.rechargeDash();
            float verticalvelocity = rb.linearVelocity.y;
            if (verticalvelocity < 0)
            {
                verticalvelocity = 0f;
            }

            if ((targetSpeed = moveSpeed * 0.6f + basespeed * 0.8f) > 15)
            {
                targetSpeed = 15;
            }
            if (contactRight && (lastWall == 0 || lastWall == 2))
            {
                lastWall = 1;
                walljumping = true;
                rb.linearVelocity = new Vector2(-targetSpeed, verticalvelocity + jumpForce * 0.75f);
            }
            else if (contactLeft && (lastWall == 0 || lastWall == 1))
            {
                lastWall = 2;
                walljumping = true;
                rb.linearVelocity = new Vector2(targetSpeed, verticalvelocity + jumpForce * 0.75f);
            }
            if (coroutine != null)
            {
                StopCoroutine(ClearJump());
            }
            coroutine = StartCoroutine(ClearJump());
        }
    }
}
