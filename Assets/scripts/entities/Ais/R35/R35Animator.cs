using UnityEngine;

public class R35Animator : MonoBehaviour
{
    Animator animator;
    public float chaseSpeed;
    public float wanderSpeed;
    public GameObject chase;
    ChasePlayer cPlayer;
    public GameObject path;
    FollowPath fPath;
    public GameObject Jump;
    jump jmp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fPath = path.GetComponent<FollowPath>();
        cPlayer = chase.GetComponent<ChasePlayer>();
        animator = GetComponent<Animator>();
        jmp = Jump.GetComponent<jump>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Speed :" + animator.speed);
        if (cPlayer.EnteredTrigger)
        {
            if (!cPlayer.Disabled)
            {
                animator.speed = chaseSpeed;
            }
            else
            {
                animator.speed = wanderSpeed;
            }
        }
        else
        {
            animator.speed = wanderSpeed;
        }
        if(jmp.jumping && jmp.JumpTimer < 0.1f)
        {
            Debug.Log("jump");
            animator.SetInteger("Action", 1);
        }
        if (jmp.GroundCheck())
        {
            animator.SetInteger("Action", 0);

        }
    }
}
