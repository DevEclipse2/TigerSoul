using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public class PlayerHealth : MonoBehaviour
{
    //public List<Transform> FallPoint;
    public int health;
    public int basehealth = 75;
    public GameObject Inputcontroller;
    
    public GameObject AnimationController;
    SimpleAnimation animation;
    public bool neardeath;
     float cleantimer;
    public float refreshneardeathTime = 18f;
    public float iFrames = 1.2f;
    float iframeTimer;
    public bool invulnerable;
    public Flash flashhull;
    public Flash flashturret;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (basehealth > health) { 
            health = basehealth;
        }
        
        health = Datapersistence.playerhealth;
        animation = AnimationController.GetComponent<SimpleAnimation>();

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
            flashhull.flash = true;
            flashturret.flash = true;
            if (iframeTimer > iFrames)
            {
                flashhull.flash = false;
                flashturret.flash = false;
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
    private void OnDestroy()
    {
        Datapersistence.FerryHealth(health);
    }
    //public void loadHealth(int loaded)
    //{
    //    health = loaded;

    //    if (loaded < basehealth)
    //    {
    //        health = basehealth;

    //    }
    //    health = Datapersistence.playerhealth;
    //}
    public void IncreaseHealth(int amount)
    {
        health += amount;
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
                animation.Reload();
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
