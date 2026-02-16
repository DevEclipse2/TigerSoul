
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
    bool EnteredTrigger;
    public Transform player;
    float landtime;
    public float range = 0.5f;
    public GameObject ChaseObject;
    public float JumpCd;
    bool jumpable;
    bool jumping;


    private IEnumerator Cd()
    {
        yield return new WaitForSeconds(JumpCd);
        jumpable = true;
        yield return null;
    }
    private void Start()
    {
        if(rb == null)
        {
            rb = GetComponent<Rigidbody2D>();

        }
        
    }
    bool Isleft()
    {
        if(this.gameObject.transform.localScale.x > 0)
        {
            return false;
        }
        return true;
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
            EnteredTrigger = false;
        }

        // You can add other checks for different objects as needed
    }
    public bool GroundCheck()
    {
        return Physics2D.BoxCast(groundCheck.position, GroundCheckArea, 0, Vector2.down, 0, groundLayer);
    }
    public void Update()
    {
        if (EnteredTrigger)
        {
            if(Ballistics.CheckPosition(JumpForce.y, JumpForce.x, player.position - this.transform.position, rb.gravityScale * 9.8f, range , out landtime) && GroundCheck() )
            {
                if (Isleft()) 
                {
                    rb.linearVelocity = new Vector2(JumpForce.x * -1 , JumpForce.y);
                }
                else
                {
                    rb.linearVelocity = new Vector2(JumpForce.x * -1, JumpForce.y);

                }
                jumpable = false;
                jumping = true;
                ChaseObject.GetComponent<ChasePlayer>().Disabled = true;
            }

        }
        if(jumping && GroundCheck())
        {
            ChaseObject.GetComponent<ChasePlayer>().Disabled = false;

            jumping = false;
            StartCoroutine(Cd());


        }
    }
}
