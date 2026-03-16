using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public class PlayerHealth : MonoBehaviour
{
    //public List<Transform> FallPoint;
    public int health;
    public int basehealth = 75;
    public GameObject Inputcontroller;
    Rigidbody2D Rb;
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
    public Vector2 pushforce;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Rb = Inputcontroller.GetComponent<Rigidbody2D>();
        if (basehealth > health) { 
            health = basehealth;
        }
        
        health = Datapersistence.playerhealth;
        animation = AnimationController.GetComponent<SimpleAnimation>();

    }
    void Update()
    {
        if (!Inputcontroller.GetComponent<PlayerMove>().canMove && Inputcontroller.GetComponent<PlayerMove>().GroundCheck())
        {
            Inputcontroller.GetComponent<PlayerMove>().canMove = true;
        }
        Inputcontroller.GetComponent<PlayerMove>().Damage = invulnerable;
        if (invulnerable)
        {
            Time.timeScale = 0.9f;
            flashhull.Damageflash(1.2f);
            flashturret.Damageflash(1.2f);
            flashhull.flash = true;
            flashturret.flash = true;
            if (iframeTimer < 0.3f)
            {
                Time.timeScale = 0.6f;
            }
            if (iframeTimer > iFrames)
            {
                flashhull.flash = false;
                flashturret.flash = false;
                invulnerable = false;
                Time.timeScale = 1.0f;
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
    public void DamageTaken(int amount , Vector2 direction)
    {
        if (invulnerable)
        {
            Debug.Log("Player is invulnerable");
            return;
        }
        invulnerable = true;
        //Debug.Log(direction * pushforce);
        Rb.linearVelocity = pushforce * direction;
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
                invulnerable = true;
                iframeTimer = -10;
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
        DamageTaken(damagetaken,Vector2.zero);
        Inputcontroller.transform.position = recoverPoint.position;
    }

}
