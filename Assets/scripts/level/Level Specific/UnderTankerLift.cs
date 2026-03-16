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
        if(cHTop.IsTriggered)
        {
            if (!top && !bottom)
            {
                top = true;
                animator.SetInteger("Value", 1 );
            }
            else if(bottom) { 
                animator.SetInteger("Value", 0);


            }
            //if this is triggered second, do nothing
        }
        else if(cHBot.IsTriggered && !top) 
        {
            if (!bottom)
            {
                bottom = true;
                animator.SetInteger("Value", 2);
            }
        }
        if(!cHTop.IsTriggered && !cHBot.IsTriggered)
        {
            if (bottom)
            {
                bottom = false;
                //resets
                animator.SetInteger("Value", 0);    
            }
        }
    }
}
