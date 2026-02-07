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
    public float DashCd;
    public float AttackCd;
    float attacktimer;
    bool attacking;
    public float dashtimer;
    public Rigidbody2D rb;
    public GameObject Attacktrigger;
    public GameObject Hurtbox;
    private CollisionHandle triggerenter;
    public GameObject Dashpoint;
    private CollisionHandle Dashtrigger;
    public Transform Retreatpoint;
    Vector2 Destination;
    public bool dashleft;
    public bool dash = false;
    public Sprite Jab;
    public Vector2 Jaboff;
    public Sprite Anticipate;
    public Vector2 Anticipateoff;
    public Sprite Back;
    public Sprite Clean;
    public Vector2 Backoff;
    public bool predash;
    float breakouttime = 1.8f;
    public bool AnimatorSetAttackComplete;
    public Collider2D moveArea;
    public GameObject superstructure;
    public bool backpedal;
    public Vector2 Jump;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        DetectionArea = GetComponent<Collider2D>();
        basic = GetComponent<FollowPath>();
        triggerenter = Attacktrigger.GetComponent<CollisionHandle>();
        Dashtrigger = Dashpoint.GetComponent<CollisionHandle>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            if(player == null)
            {
                player = collision.gameObject;
            }
            Debug.Log("player");
            Chase();
            // Perform the raycast

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
        basic.disable = false;
        chasing = false;
        animator.speed = 1;

    }
    void Chase()
    {
        basic.disable = true;
        chasing = true;
        if(!(predash || dash))
        {
            rb.linearVelocity = new Vector2(chasespeed * CheckDir(), 0);
        }
    }
    void MoveTowardsPlayer()
    {
        rb.linearVelocity = new Vector2(chasespeed * CheckDir() , rb.linearVelocityY);
        
    }
    public float CheckDir()
    {
        if (player.transform.position.x - entity.transform.position.x < 0)
        {
            return -1;
        }
            return 1;
    }
    void Engage()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {

        if (!attacking){attacktimer += Time.deltaTime;}
        else if (AnimatorSetAttackComplete)
        {    
                animator.SetInteger("Attacking", 0);

            if (triggerenter.IsTriggered && Hurtbox.GetComponent<ContactDamage>().success)
            {
                attacktimer = 0;
                attacking = true;
                animator.SetInteger("Attacking", Random.Range(0, 2));
            }
            else
            {
                attacking = false;
                superstructure.SetActive(true);

                if (dashtimer > DashCd - 1)
                {
                    dashtimer = DashCd - 1;
                    backpedal = true;
                    rb.linearVelocity = Jump;
                }
            }
            
            
        }
        if(backpedal)
        {
            dashtimer += Time.deltaTime;
            if(dashtimer > DashCd)
            {
                dashtimer = 0;
                backpedal = false;
            }
            
        }
        
        if (predash || dash)
        {
            Chase();
        }
        if(!dash) {dashtimer += Time.deltaTime; } else
        {
            spriteobj.GetComponent<SpriteRenderer>().sprite = Jab;

        }
        if (!attacking)
        {
            superstructure.SetActive(true);

        }
        else
        {
            superstructure.SetActive(false);

        }
        if (chasing)
        {
            if (!dash)
            {
                if (!predash)
                {
                    //Debug.Log("move");
                    if (!attacking && !backpedal)
                    {
                        MoveTowardsPlayer();
                    }


                }
                else
                {

                    rb.linearVelocity = new Vector2(chasespeed * CheckDir() * -1 * 0.3f, 0);
                    dashtimer += Time.deltaTime;
                    Debug.Log("adding time to charge");

                }
                if (!backpedal)
                {
                    if ((Dashtrigger.IsTriggered && dashtimer > DashCd) || predash)
                    {
                    
                        Debug.Log("Inrange");
                        if (!predash && !dash)
                        {
                            Debug.Log("predash");
                            predash = true;
                            Destination = (Vector2)Dashpoint.transform.position;

                            if (!moveArea.OverlapPoint(Destination))
                            {
                                Destination = moveArea.ClosestPoint(Destination);
                            }
                            dashtimer = DashCd - 0.8f;
                            spriteobj.GetComponent<SpriteRenderer>().sprite = Anticipate;
                            spriteobj.transform.localPosition = Anticipateoff;
                            animator.SetBool("Hide", true);
                        }
                        else if (predash)
                        {
                            if (dashtimer > DashCd)
                            {
                                superstructure.SetActive(true);
                                Debug.Log("dash");
                                predash = false;
                                dashtimer = 0;
                                dashleft = false;
                                rb.linearVelocity = new Vector2(Dashspeed, 0);
                                if (player.transform.position.x < entity.transform.position.x)
                                {
                                    rb.linearVelocity = new Vector2(Dashspeed * -1, 0);
                                    dashleft = true;
                                }
                                dash = true;
                                spriteobj.GetComponent<SpriteRenderer>().sprite = Jab;
                                spriteobj.transform.localPosition = Jaboff;
                                animator.SetBool("Hide", true);
                            }

                        }
                    }
                }
                
            }
            else
            {
                breakouttime -= Time.deltaTime;
                if (breakouttime < 0)
                {

                    Debug.Log("breakout");
                    breakouttime = 1.8f;
                    dash = false;
                    spriteobj.GetComponent<SpriteRenderer>().sprite = Clean;
                    animator.SetBool("Hide", false);

                }
                if (dashleft)
                {
                    if (entity.transform.position.x < Destination.x)
                    {
                        rb.linearVelocity = Vector2.zero;
                        dash = false;
                        Debug.Log("exit");
                        dashtimer = 0;
                        predash = false;
                        spriteobj.GetComponent<SpriteRenderer>().sprite = Clean;
                        animator.SetBool("Hide", false);
                    }
                }
                else
                {
                    if (entity.transform.position.x > Destination.x)
                    {
                        rb.linearVelocity = Vector2.zero;
                        dash = false;
                        Debug.Log("exit");
                        dashtimer = 0;
                        predash = false;
                        spriteobj.GetComponent<SpriteRenderer>().sprite = Clean;
                        animator.SetBool("Hide", false);
                    }
                }
            }

            if (triggerenter.IsTriggered && !dash && !predash)
            {
                //in attack range
                if (attacktimer > AttackCd)
                {
                    Debug.Log("attack");
                    attacktimer = 0;
                    attacking = true;
                    animator.SetBool("Hide", false);
                    superstructure.SetActive(false);
                    animator.speed = 1;

                    animator.SetInteger("Attacking", Random.Range(1, 2));
                }
            }
        }
        
        
    }
}
