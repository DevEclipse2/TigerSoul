using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public class PlayerHealth : MonoBehaviour
{
    //public List<Transform> FallPoint;
    public int health;
    public int basehealth = 75;
    public GameObject Inputcontroller;
    public GameObject SavepointContainer;
    public SavePoint savepointObject;
    public bool neardeath;
     float cleantimer;
    public float refreshneardeathTime = 18f;
    public float iFrames = 1.2f;
    float iframeTimer;
    public bool invulnerable;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (basehealth > health) { 
            health = basehealth;
        }
        
        savepointObject = SavepointContainer.GetComponent<SavePoint>();
    }
    void Update()
    {
        if(Inputcontroller.GetComponent<PlayerMove>() == null)
        {
            return;
        }
        if (!Inputcontroller.GetComponent<PlayerMove>().canMove && Inputcontroller.GetComponent<PlayerMove>().GroundCheck())
        {
            Inputcontroller.GetComponent<PlayerMove>().canMove = true;
        }
        if (invulnerable)
        {
            if(iframeTimer > iFrames)
            {
                invulnerable = false;
            }
            iframeTimer += Time.deltaTime;
        }
        if (neardeath) { 
            cleantimer += Time.deltaTime;
            if (cleantimer > refreshneardeathTime) { 
                neardeath = false;
                cleantimer = 0;
            }
        }
    }
    public void loadHealth(int loaded)
    {
        health = loaded;

        if (loaded < basehealth)
        {
            health = basehealth;

        }
    }
    public void DamageTaken(int amount)
    {
        if (invulnerable)
        {
            Debug.Log("Player is invulnerable");
            return;
        }
        invulnerable = true;
        iframeTimer = 0;
        health -= amount;
        if(health < 0)
        {
            if (!neardeath)
            {
                health = 0;
                neardeath = true;
            }
            else
            {
                health = basehealth; 
                neardeath = false;
                savepointObject.RecoverSave();
                //dies
            }

            //neardeath
        }

    }
    public void Pitfall( Transform recoverPoint , int damagetaken)
    {
        Inputcontroller.GetComponent<PlayerMove>().canMove = false;
        Inputcontroller.GetComponent<PlayerMove>().moveDir = Vector2.zero;
        Inputcontroller.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        DamageTaken(damagetaken);
        Inputcontroller.transform.position = recoverPoint.position;
    }

}
