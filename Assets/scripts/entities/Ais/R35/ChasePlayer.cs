using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.Universal;
public class ChasePlayer : MonoBehaviour
{

    private GameObject player;
    bool EnteredTrigger;
    private FollowPath followPath;
    public GameObject followPathObject;
    int closestNode;
    float closestDist;
    public float chaseSpeed;
    private Rigidbody2D rb;
    public bool Disabled;
    public float stopDist = 0.4f;
    public float turnspeed = 1.3f;
    bool alreadyturning;
    
    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        //Debug.Log("touched collider");

        // Check if the collider belongs to a specific tag (e.g., "Player")
        if (collision.CompareTag("Player"))
        {
            player = collision.gameObject;
            EnteredTrigger = true;
            followPath.disable = true;

        }
        // You can add other checks for different objects as needed
    }
    void OnTriggerExit2D(UnityEngine.Collider2D collision)
    {
        //Debug.Log("touched collider");

        // Check if the collider belongs to a specific tag (e.g., "Player")
        if (collision.CompareTag("Player"))
        {
            EnteredTrigger = false;
            closestNode = 0;
            Vector2 node = Vector2.zero;
            closestDist = 99999;
            for(int i = 0; i < followPath.pointvec.Length; i++)
            {
                node = followPath.pointvec[i];
                float challengerdist = Vector2.Distance(node, followPathObject.transform.position);
                if (challengerdist <= closestDist)
                {
                    closestDist = challengerdist;
                    closestNode = i;
                }
            }
            followPath.target = closestNode;
            followPath.disable = false;
        }

        // You can add other checks for different objects as needed
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(followPathObject != null)
        {
            followPath = followPathObject.GetComponent<FollowPath>();
        }
        else
        {
            followPathObject = this.gameObject;
            followPath = GetComponent<FollowPath>();
        }
        rb = followPath.Rigidbodycontainer.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame

    void Update()
    {
        if (EnteredTrigger && !Disabled)
        {   
            if (Mathf.Abs(player.transform.position.x - followPathObject.transform.position.x) > stopDist) 
            { 
                if(player.transform.position.x < followPathObject.transform.position.x)
                {
                    if(rb.linearVelocityX >= 0 || Mathf.Abs(rb.linearVelocityX) <= chaseSpeed)
                    {
                        //Debug.Log("to The left" + rb.linearVelocityX + followPath.disable);

                        rb.linearVelocity = new Vector2(rb.linearVelocityX - turnspeed * Time.deltaTime, rb.linearVelocityY);
                    }

                }
                else
                {

                    if (rb.linearVelocityX <= 0 || Mathf.Abs(rb.linearVelocityX) <= chaseSpeed)
                    {
                        //Debug.Log("to The right" + rb.linearVelocityX);

                        rb.linearVelocity = new Vector2(rb.linearVelocityX + turnspeed * Time.deltaTime, rb.linearVelocityY);
                    }
                }
            }
            else
            {
               //rb.linearVelocity = new Vector2(0, rb.linearVelocityY);
            }
        }
    }
}
