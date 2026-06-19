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
        base.cooldown();
    }
    public override void init()
    {
        gravScale = movementscript.rb.gravityScale;
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
                    if (movementscript.gameObject.transform.position.x - dashAmt / dashTime * Time.deltaTime < DashTarget.x)
                    {


                        movementscript.gameObject.transform.position = DashTarget;
                        dashing = false;
                        movementscript.changeMove(true);
                        rb.gravityScale = gravScale;
                        rb.linearVelocity = Vector2.zero;

                    }
                    else
                    {
                        movementscript.gameObject.transform.position = new Vector2(movementscript.gameObject.transform.position.x - dashAmt / dashTime * Time.deltaTime, movementscript.gameObject.transform.position.y);
                    }

                }
                else
                {
                    if (movementscript.gameObject.transform.position.x + dashAmt / dashTime * Time.deltaTime > DashTarget.x)
                    {


                        movementscript.gameObject.transform.position = DashTarget;
                        dashing = false;
                        movementscript.changeMove(true);
                        rb.gravityScale = gravScale;
                        rb.linearVelocity = Vector2.zero;
                    }
                    else
                    {
                        movementscript.gameObject.transform.position = new Vector2(movementscript.gameObject.transform.position.x + dashAmt / dashTime * Time.deltaTime, movementscript.gameObject.transform.position.y);
                    }

                }

            }

        }
        
    }

    public override void useAbility()
    {
        Debug.Log("Dash");
        if(!Active || !Available) return;
        Rigidbody2D rb = movementscript.rb;
        rb.linearVelocity = Vector2.zero;
        Available = false;
        timer = 0;
        movementscript.changeMove(false);
        dashing = true;
        rb.gravityScale = 0;
        movementscript.resetLastWall();
        RaycastHit2D geometry;
        if (movementscript.isLeft)
        {
            geometry = Physics2D.BoxCast(movementscript.gameObject.transform.position, new Vector2(2.16f, 0.18f), 0, Vector2.left, dashAmt, dashLayer);

            try
            {
                Debug.Log(geometry.collider.gameObject.name);
            }
            catch (Exception e)
            {

            }
            if (!geometry)
            {
                DashTarget = movementscript.gameObject.transform.position + new Vector3(-dashAmt, 0, 0);
            }
            else
            {
                Debug.Log(geometry.collider.gameObject.name);
                closestpt = true;
                DashTarget = geometry.collider.ClosestPoint(movementscript.gameObject.transform.position);
                DashTarget = new Vector2(DashTarget.x + 1.2f, DashTarget.y);
            }

        }
        else
        {
            geometry = Physics2D.BoxCast(movementscript.gameObject.transform.position, new Vector2(2.16f, 0.18f), 0, Vector2.right, dashAmt, dashLayer);

            try
            {
                Debug.Log(geometry.collider.gameObject.name);
            }
            catch (Exception e) { 
            
            }
            if (!geometry)
            {
                DashTarget = movementscript.gameObject.transform.position + new Vector3(dashAmt, 0, 0);


            }
            else
            {

                closestpt = true;
                DashTarget = geometry.collider.ClosestPoint(movementscript.gameObject.transform.position);
                DashTarget = new Vector2(DashTarget.x - 1.2f, DashTarget.y);
            }
        }
        rb.linearVelocity = Vector2.zero;
    }
    
}
