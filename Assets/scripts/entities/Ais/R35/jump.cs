
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class jump : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 JumpForce;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Vector2 GroundCheckArea;
    public bool EnteredTrigger;
    public Transform player;
    public GameObject followPath;
    FollowPath fp;
    float landtime;
    public float range = 0.5f;
    public GameObject ChaseObject;
    public float JumpCd;
    float JumpCdTimer;
    bool jumpable = true;
    public bool jumping;
    public float JumpTimer = 0;
    public bool ground;
    private void Start()
    {
        fp = followPath.GetComponent<FollowPath>();
        if(rb == null)
        {
            rb = GetComponent<Rigidbody2D>();

        }

        jumpable = true;
    }
    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        //Debug.Log("touched collider");

        // Check if the collider belongs to a specific tag (e.g., "Player")
        if (collision.CompareTag("Player"))
        {
            player = collision.transform;
            EnteredTrigger = true;
        }

        // You can add other checks for different objects as needed
    }
    void OnTriggerExit2D (UnityEngine.Collider2D collision)
    {
        //Debug.Log("touched collider");

        // Check if the collider belongs to a specific tag (e.g., "Player")
        if (collision.CompareTag("Player"))
        {
            if (!jumping)
            {
                EnteredTrigger = false;
            }
        }

        // You can add other checks for different objects as needed
    }
    public bool GroundCheck()
    {
        return Physics2D.BoxCast(groundCheck.position, GroundCheckArea, 0, Vector2.down, 0, groundLayer);
    }
    public void Update()
    {

        ground = GroundCheck();
        if(JumpCdTimer > JumpCd)
        {
            jumpable = true;
        }
        if (EnteredTrigger)
        {
            
            if (player.position.x < transform.position.x) 
            {
                //to the right of the player.
                //jumps left
                if (Ballistics.CheckPosition(JumpForce.y, -JumpForce.x, player.position - transform.position, rb.gravityScale * 9.8f, range, out landtime) && GroundCheck() && jumpable)
                {
                    Debug.Log("jump");

                    jumpable = false;
                    jumping = true;
                    ChaseObject.GetComponent<ChasePlayer>().Disabled = true;
                    rb.linearVelocity = new Vector2(-JumpForce.x, JumpForce.y); 
                }
            }
            else
            {
                if (Ballistics.CheckPosition(JumpForce.y, JumpForce.x, player.position - transform.position, rb.gravityScale * 9.8f, range, out landtime) && GroundCheck() && jumpable)
                {
                    Debug.Log("jump");

                    jumpable = false;
                    jumping = true;
                    ChaseObject.GetComponent<ChasePlayer>().Disabled = true;
                    rb.linearVelocity = new Vector2(JumpForce.x, JumpForce.y);
                }
            }
        }
        if (jumping)
        {
            Debug.Log("Disable");
            JumpTimer += Time.deltaTime;
            fp.disable = true;
            ChaseObject.GetComponent<ChasePlayer>().Disabled = true;
        }
        if (!jumpable && GroundCheck())
        {
            JumpCdTimer += Time.deltaTime;
            if (jumping && JumpTimer > 0.2f)
            {
                JumpTimer = 0;
                ChaseObject.GetComponent<ChasePlayer>().Disabled = false;
                jumping = false;
                JumpCdTimer = 0;
            }
        }
        if(JumpTimer > 2)
        {
            JumpTimer = 0;
            ChaseObject.GetComponent<ChasePlayer>().Disabled = false;
            jumping = false;
            JumpCdTimer = 0;
        }
        
    }
}
