//using UnityEditor.Networking.PlayerConnection;
using UnityEngine;

public class TachamBehaviour : MonoBehaviour
{
    FollowPath basic;
    Collider2D DetectionArea;
    public GameObject player;
    public GameObject entity;
    public GameObject spriteobj;
    public Animator animator;
    public float chasespeed;
    public float Dashspeed;
    bool chasing;
    public Transform scanbox;
    float Scanheight;
    bool targetground;
    public float DashCd;
    public float AttackCd;
    float attacktimer;
    bool attacking;
    float dashtimer;
    public Rigidbody2D rb;
    public GameObject Attacktrigger;
    private CollisionHandle triggerenter;
    public GameObject Dashpoint;
    private CollisionHandle Dashtrigger;
    public transform Retreatpoint;
    Vector2 Destination;
    bool dashleft;
    bool dash = false;
    public Sprite Jab;
    public Vector2 Jaboff;
    public Sprite Anticipate;
    public Vector2 Anticipateoff;
    public Sprite Back;
    public Vector2 Backoff;
    bool predash;
    float breakouttime = 1.8f;
    public bool AnimatorSetAttackComplete;


    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        Scanheight = scanbox.localScale.y;
        DetectionArea = GetComponent<Collider2D>();
        basic = GetComponent<FollowPath>();
        triggerenter = Attacktrigger.GetComponent<CollisionHandle>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector3 direction = player.transform.position - transform.position;

            // Perform the raycast
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction.normalized, out hit))
            {
                if (hit.collider.CompareTag("Player") && Mathf.Abs(hit.transform.position.y - entity.transform.position.y) < Scanheight)
                {
                    //within line of sight
                    Chase();
                }
            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Disengage();

        }
    }
    void Disengage()
    {
        basic.enabled = true;
        chasing = false;
    }
    void Chase()
    {
        basic.enabled = false;
        chasing = true;
        
    }
    void MoveTowardsPlayer()
    {
        float left = Mathf.Clamp(player.transform.position.x - entity.transform.position.x , -1 ,1 );
        rb.linearVelocity = new Vector2(chasespeed * left , 0);
        
    }
    void Engage()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if(!attacking){attacktimer += Time.deltaTime;}
        else
        {    
            if(AnimatorSetAttackComplete)
            {
                attacking = false;
                if(dashtimer > DashCd){
                    Destination = Retreatpoint.position;
                    dash = true;
                    dashtimer = 0;
                    float left = Mathf.Clamp(player.transform.position.x - entity.transform.position.x , -1 ,1 );
                    rb.linearVelocity = new Vector2(chasespeed * left * -1 * 0.8f , 0);
                    dashleft = (entity.transform.position.x < Destination.x);
                    spriteobj.GetComponent<SpriteRenderer>().Sprite = Back;
                    spriteobj.transform.position = Backoff;
                    animator.SetBool("Hide", true);
                }
            }
        }
        if(!dash || predash) {dashtimer += Time.deltaTime;}
        if(chasing)
        {
            if(!dash)
            {
                if(!predash){
                    MoveTowardsPlayer();
                }
                else{
                    float left = Mathf.Clamp(player.transform.position.x - entity.transform.position.x , -1 ,1 );
                    rb.linearVelocity = new Vector2(chasespeed * left * -1 * 0.3f , 0); 
                }
                
                if(Dashtrigger.IsTriggered && dashtimer > DashCd)
                {
                    if(!predash)
                    {
                        predash = true;
                        dashtimer = DashCd - 0.4f;
                        spriteobj.GetComponent<SpriteRenderer>().Sprite = Anticipate;
                        spriteobj.transform.position = Anticipateoff;
                        animator.SetBool("Hide", true);
                    }
                    else 
                    {
                        predash = false;
                        Destination = Dashpoint.transform.position;
                        dashtimer = 0;
                        dashleft = false;
                        rb.linearVelocity = new Vector2(Dashspeed , 0);
                        if(player.transform.position.x < entity.transform.position.x)
                        {
                            rb.linearVelocity = new Vector2(Dashspeed * -1, 0);
                            dashleft = true;
                        }
                        dash = true;
                        spriteobj.GetComponent<SpriteRenderer>().Sprite = Jab;
                        spriteobj.transform.position = Jaboff;
                        animator.SetBool("Hide" , true);
                    }
                }
            }
            else
            {
                breakouttime -= Time.deltaTime;
                if(breakouttime < 0)
                {
                    breakouttime = 1.8f;
                    dash = false;
                    spriteobj.GetComponent<SpriteRenderer>().Sprite = Clean;
                    animator.SetBool("Hide" , false);
                    
                }
                if(dashleft)
                {
                    if(transform.position.x < Destination.x)
                    {
                        rb.linearVelocity = Vector2.zero;
                        dash = false;
                        spriteobj.GetComponent<SpriteRenderer>().Sprite = Clean;
                        animator.SetBool("Hide" , false);
                    }
                }
                else
                {
                    if(transform.position.x > Destination.x)
                    {
                        rb.linearVelocity = Vector2.zero;
                        dash = false;
                        spriteobj.GetComponent<SpriteRenderer>().Sprite = Clean;
                        animator.SetBool("Hide" , false);
                    }
                }
            }
            
            if(triggerenter.IsTriggered)
            {
                //in attack range
                if(attacktimer > AttackCd)
                {
                    attacktimer = 0;
                    attacking =  true;
                    animator.SetInt("Attacking" , Random.Range(0,2));
                }
            }
        }
    }
}
