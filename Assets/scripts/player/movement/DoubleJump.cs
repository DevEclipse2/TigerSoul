using System.Collections;
using UnityEngine;

public class DoubleJump : baseUpgrade
{
    public Animator animator;
    [SerializeField]
    float speed = 19.0f;
    [SerializeField]
    float disableMoves = 0.2f;
    public override void init()
    {
        
    }
    public IEnumerator pauseMovement()
    {
        yield return new WaitForSeconds(disableMoves);
        movementscript.excludeDash(true);
        movementscript.changeMove(true);
        yield return null;
    }

    public override void useAbility()
    {
        if(!Active||!Available) return;
        if (!movementscript.GroundCheck())
        {
            Debug.Log("Double Jump");
            movementscript.rb.linearVelocity = new Vector2(0,speed);
            Available = false;
            movementscript.excludeDash(false);
            movementscript.changeMove(false);
            StartCoroutine(pauseMovement());
            animator.SetInteger("Action", 3);
        }
    }
}
