using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.Rendering;

public class Bt7 : BaseEnemy
{
    [SerializeField]
    float chaseSpeed;
    [SerializeField]
    float wanderSpeed;
    [SerializeField]
    float maxViewDist;
    [SerializeField]
    Transform Player;
    [SerializeField]
    GameObject HomePoint;
    [SerializeField]
    GameObject NavArea;
    private Collider2D navCollider;
    int maxAttempts = 256;
    Vector2 MoveTarget;
    float actionTimer;
    Animator animator;
    [SerializeField]
    LayerMask barricade;

    bool chaseSuccess;

    float targetRot;
    float idleMaxTimer;
    float AlertMaxTimer;
    float SearchTimer = 1.2f;
    float alertTime;
    private bool EnteredViewLf;
    enum States
    {
        background,
        idle,
        transition,
        wander,
        alert,
        uneasy,
        chasing,
        attack,
        prepRetreat,
        retreat,
        returnHome,
    };
    private States currentState = States.idle;
    private States cachedState = States.background;
    private Quaternion rotQuat;
    bool uneasyPickedPoint;
    bool didUneasy;


    Rigidbody2D rb;
    /// <summary>
    /// behaviour of the bt7
    /// the bt7 wanders an area until an unwitting player enters
    /// the bt7 will then attempt to give chase, and  start circling the player
    /// the regular bt7 variant circles tighter and tighter until it hits you, then retreats
    /// the 76 bt will do rapid thrusting attacks that knock you off balance
    /// if not chasing the player, the bt7 will return to it's home point and chill out 
    /// 
    /// other variants of this will run at and through you directly
    /// the ground variant baits the player with fakeout attacks
    /// </summary>

    void Start()
    {
        navCollider = NavArea.GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    
    bool SeekPoint()
    {
        Bounds bounds = navCollider.bounds;
        for (int i = 0; i < maxAttempts; i++)
        {
            float randomX = Random.Range(bounds.min.x, bounds.max.x);
            float randomY = Random.Range(bounds.min.y, bounds.max.y);
            float hitboxSize = bounds.max.x - bounds.min.x;
            if(bounds.max.y - bounds.min.y < hitboxSize)
            {
                hitboxSize = bounds.max.y - bounds.min.y;
            }
            if(hitboxSize > 8.0f)
            {
                hitboxSize = 8.0f;
            }
            Vector2 randomPoint = new Vector2(randomX, randomY);
            if( CheckPoint(randomPoint)&&CheckLineOfSight(transform.position,randomPoint,barricade) && Vector2.Distance(randomPoint,transform.position) > 0.45 * hitboxSize)
            {
                MoveTarget = randomPoint;
                return true;
            }
        }
        return false;
    }

    bool CheckPoint(Vector2 targetPoint)
    {
        //overlaps and checks if its in nav and also not in ground
        if (navCollider.OverlapPoint(targetPoint))
        {
            // Success! We found a valid point.
            return true;
        }
        return false;
    }
    bool CheckLineOfSight(Vector2 initialpoint, Vector2 targetPoint, LayerMask mask)
    {
        RaycastHit2D hit = Physics2D.Raycast(initialpoint, targetPoint - initialpoint,Mathf.Clamp( Vector2.Distance(initialpoint,targetPoint),0.0f,maxViewDist), mask);
        if (hit.collider != null)
        {
            return false;
        }
        return true;
    }
    void Pursue()
    {

    }

    void Circle()
    {

    }
    void Attack()
    {
    }
    void Statemachine()
    {
        switch (currentState)
        {
            case States.background:
                actionTimer = 0;
                currentState = States.idle;
                Debug.Log("main state should not be background state");
                break;
            case States.idle:
                if(actionTimer > idleMaxTimer)
                {
                    actionTimer = 0;
                    if(SeekPoint())
                    {
                        currentState = States.transition;
                        Vector2 vecDir = Vector2.Normalize(MoveTarget - (Vector2)transform.position);
                        targetRot = Mathf.Atan2(vecDir.y, vecDir.x) * Mathf.Rad2Deg;
                        rotQuat = (Quaternion.Euler(0, 0, targetRot));
                        cachedState = States.wander;
                    }
                }
                break;
            case States.transition:
                
                if(cachedState != States.uneasy && cachedState != States.chasing)
                {
                    if(actionTimer > 0.4f)
                    {
                        goto end;
                        
                    }
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotQuat, Mathf.Clamp01( actionTimer/ 0.4f));

                }
                else
                {
                    if (actionTimer > 0.2f)
                    {
                        goto end;
                    }
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotQuat, Mathf.Clamp01( actionTimer / 0.2f));
                }
            end:
                actionTimer = 0;
                if(cachedState != States.background)
                {
                    currentState = cachedState;
                    cachedState = States.background;
                }
                else
                {
                    Debug.LogError("Missing valid state to move out of!");
                }
                break;
            case States.wander:
                rb.linearVelocity = transform.up * wanderSpeed;
                if(Vector2.Distance(transform.position,MoveTarget) < 0.3f)
                {
                    actionTimer = 0;
                    idleMaxTimer = Random.Range(2.8f, 6.3f);
                    currentState = States.idle;
                }
                break;
            case States.alert:
                alertTime += Time.deltaTime;
                if(EnteredView)
                {
                    alertTime = 0;
                }
                else if(alertTime > AlertMaxTimer)
                {
                    currentState = States.idle;
                    actionTimer = 0;
                    alertTime = 0;
                }
                if(actionTimer > SearchTimer)
                {
                    actionTimer = 0;
                    Vector2 playerpos = Player.position;
                    if(!CheckLineOfSight(transform.position , playerpos,barricade))
                    {
                        Bounds bounds = navCollider.bounds;
                        bool HasPoint = false;
                        for (int i = 0; i < maxAttempts; i++)
                        {
                            float randomX = Random.Range(bounds.min.x, bounds.max.x);
                            float randomY = Random.Range(bounds.min.y, bounds.max.y);
                            Vector2 randomPoint = new Vector2(randomX, randomY);
                            if (CheckPoint(randomPoint) && CheckLineOfSight(playerpos, randomPoint, barricade))
                            {
                                MoveTarget = randomPoint;
                                currentState = States.transition;
                                Vector2 VDir = Vector2.Normalize(MoveTarget - (Vector2)transform.position);
                                targetRot = Mathf.Atan2(VDir.y, VDir.x) * Mathf.Rad2Deg;
                                rotQuat = (Quaternion.Euler(0, 0, targetRot));
                                cachedState = States.wander;
                                HasPoint = true;
                                break;
                            }
                        }
                        if(!HasPoint && !didUneasy)
                        {
                            if(!didUneasy)
                            {
                                currentState = States.uneasy;
                                AlertMaxTimer += 3.2f;
                                actionTimer = 0;
                                uneasyPickedPoint = false;

                            }
                            else
                            {
                                idleMaxTimer = 0.0f;
                                currentState = States.idle;
                                actionTimer = 0;
                            }
                        }
                       
                    }
                    else
                    {
                        MoveTarget = playerpos;
                        currentState = States.chasing;
                        actionTimer = 0;
                        alertTime = 0;
                    }
                }
                break;
            case States.uneasy:
                if (!uneasyPickedPoint)
                {
                    if (SeekPoint())
                    {
                        currentState = States.transition;
                        Vector2 movedir = Vector2.Normalize(MoveTarget - (Vector2)transform.position);
                        targetRot = Mathf.Atan2(movedir.y, movedir.x) * Mathf.Rad2Deg;
                        rotQuat = (Quaternion.Euler(0, 0, targetRot));
                        cachedState = States.uneasy;
                        uneasyPickedPoint = true;
                    }
                }
                else
                {
                    rb.linearVelocity = transform.up * wanderSpeed * 1.3f;
                    if (Vector2.Distance(transform.position, MoveTarget) < 0.8f)
                    {
                        actionTimer = 0;
                        idleMaxTimer = Random.Range(2.8f, 6.3f);
                        currentState = States.alert;
                        didUneasy = true;
                    }
                }
                break;
            case States.chasing:
                Vector2 direction = Vector2.Normalize((Vector2)Player.position - (Vector2)transform.position);
                targetRot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = (Quaternion.Euler(0, 0, targetRot));
                rb.linearVelocity = transform.up * chaseSpeed;
                break;
            case States.attack:
                break;
            case States.prepRetreat:
                actionTimer = 0;
                Vector2 pPos = HomePoint.transform.position;
                if (!CheckLineOfSight(transform.position, pPos, barricade))
                {
                    Bounds bounds = navCollider.bounds;
                    bool HasPoint = false;
                    for (int i = 0; i < maxAttempts; i++)
                    {
                        float randomX = Random.Range(bounds.min.x, bounds.max.x);
                        float randomY = Random.Range(bounds.min.y, bounds.max.y);
                        Vector2 randomPoint = new Vector2(randomX, randomY);
                        if (CheckPoint(randomPoint) && CheckLineOfSight(pPos, randomPoint, barricade))
                        {
                            MoveTarget = randomPoint;
                            currentState = States.transition;
                            Vector2 VDir = Vector2.Normalize(MoveTarget - (Vector2)transform.position);
                            targetRot = Mathf.Atan2(VDir.y, VDir.x) * Mathf.Rad2Deg;
                            rotQuat = (Quaternion.Euler(0, 0, targetRot));
                            cachedState = States.retreat;
                            break;
                        }
                    }
                }
                else
                {
                    MoveTarget = pPos;
                    currentState = States.transition;
                    Vector2 VDir = Vector2.Normalize(MoveTarget - (Vector2)transform.position);
                    targetRot = Mathf.Atan2(VDir.y, VDir.x) * Mathf.Rad2Deg;
                    rotQuat = (Quaternion.Euler(0, 0, targetRot));
                    cachedState = States.retreat;
                    actionTimer = 0;
                    alertTime = 0;
                }
                break;
            case States.retreat:
                rb.linearVelocity = transform.up * wanderSpeed;
                if (Vector2.Distance(transform.position, MoveTarget) < 0.3f)
                {
                    actionTimer = 0;
                    idleMaxTimer = Random.Range(2.8f, 6.3f);
                    currentState = States.idle;
                }
                break;
            default:
                break;
        }
    }
    void Update()
    {
        if (CheckLineOfSight(transform.position, Player.position, barricade) && currentState != States.chasing && cachedState != States.chasing && currentState != States.retreat && cachedState != States.retreat)
        {
            MoveTarget = Player.position;
            currentState = States.transition;
            Vector2 direction = Vector2.Normalize(MoveTarget - (Vector2)transform.position);
            targetRot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rotQuat = (Quaternion.Euler(0, 0, targetRot));
            cachedState = States.chasing;
        }
        if(chaseSuccess)
        {
            chaseSuccess = false;
            currentState = States.prepRetreat;
        }
        if (EnteredView && !EnteredViewLf)
        {
            didUneasy = false;
            EnteredViewLf = true;
            currentState = States.alert;
            AlertMaxTimer = Random.Range(3,6);
        }
        
        actionTimer += Time.deltaTime;
        Statemachine();
    }
}
