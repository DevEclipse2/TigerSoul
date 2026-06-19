using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class dash : baseUpgrade
{
    public float dashAmt = 0;
    Vector2 DashTarget;
    bool closestpt = false;
    public LayerMask dashLayer;
    bool dashing;
    public float dashTime;
    float timer;
    float gravScale = 0;
    public override void cooldown()
    {
        gravScale = movementscript.rb.gravityScale;
    }
    public override void init()
    {
    }
    public void dashtick()
    {
        Rigidbody2D rb = movementscript.rb;
        timer += Time.deltaTime;
        if (dashing)
        {

            if (timer >= dashTime)
            {
                dashing = false;
                movementscript.changeMove(true);

                rb.gravityScale = gravScale;
                rb.linearVelocity = Vector2.zero;
            }
            else
            {
                if (movementscript.isLeft)
                {
                    if (transform.position.x - dashAmt / dashTime * Time.deltaTime < DashTarget.x)
                    {
                        Debug.Log("dash ended positional");

                        transform.position = DashTarget;
                        dashing = false;
                        movementscript.changeMove(true);
                        rb.gravityScale = gravScale;
                        rb.linearVelocity = Vector2.zero;

                    }
                    else
                    {
                        transform.position = new Vector2(transform.position.x - dashAmt / dashTime * Time.deltaTime, transform.position.y);
                    }

                }
                else
                {
                    if (transform.position.x + dashAmt / dashTime * Time.deltaTime > DashTarget.x)
                    {
                        Debug.Log("dash ended positional");

                        transform.position = DashTarget;
                        dashing = false;
                        movementscript.changeMove(true);
                        rb.gravityScale = gravScale;
                        rb.linearVelocity = Vector2.zero;
                    }
                    else
                    {
                        transform.position = new Vector2(transform.position.x + dashAmt / dashTime * Time.deltaTime, transform.position.y);
                    }

                }

            }

        }
    }

    public override void useAbility()
    {
        Debug.Log(Active + " is active");
        Debug.Log(Available + " is available");
        if(!Active || !Available) return;
        Rigidbody2D rb = movementscript.rb;
        Available = false;
        cooldown();
        timer = 0;
        movementscript.changeMove(false);
        dashing = true;
        rb.gravityScale = 0;
        movementscript.resetLastWall();
        RaycastHit2D geometry;
        if (movementscript.isLeft)
        {
            geometry = Physics2D.BoxCast(this.gameObject.transform.position, new Vector2(2.16f, 0.18f), 0, Vector2.left, dashAmt, dashLayer);
            if (!geometry)
            {
                DashTarget = this.gameObject.transform.position + new Vector3(-dashAmt, 0, 0);
            }
            else
            {
                closestpt = true;
                DashTarget = geometry.collider.ClosestPoint(this.gameObject.transform.position);
                DashTarget = new Vector2(DashTarget.x + 1.2f, DashTarget.y);
            }

        }
        else
        {
            geometry = Physics2D.BoxCast(this.gameObject.transform.position, new Vector2(2.16f, 0.18f), 0, Vector2.right, dashAmt, dashLayer);
            if (!geometry)
            {
                DashTarget = this.gameObject.transform.position + new Vector3(dashAmt, 0, 0);


            }
            else
            {
                closestpt = true;
                DashTarget = geometry.collider.ClosestPoint(this.gameObject.transform.position);
                DashTarget = new Vector2(DashTarget.x - 1.2f, DashTarget.y);
            }
        }
        rb.linearVelocity = Vector2.zero;
    }
    
}
