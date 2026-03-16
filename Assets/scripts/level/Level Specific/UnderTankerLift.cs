using UnityEngine;

public class UnderTankerLift : MonoBehaviour
{
    bool player = false;
    public GameObject collisionTop;
    public GameObject collisionBottom;
    private CollisionHandle cHTop;
    private CollisionHandle cHBot;
    bool bottom;
    bool top;
    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        cHTop = collisionTop.GetComponent<CollisionHandle>();
        cHBot = collisionBottom.GetComponent<CollisionHandle>();
    }

    // Update is called once per frame
    void Update()
    {
        if(cHTop.IsTriggered && !bottom)
        {
            if (!top)
            {
                top = true;
                animator.SetInteger("Value", 1 );
            }
            //if this is triggered second, do nothing
        }
        else if(cHBot.IsTriggered && !top) 
        {
            bottom = true;
            animator.SetInteger("Value", 2);
        }

    }
}
