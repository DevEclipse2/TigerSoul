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
                health = basehealth; neardeath = false;
                savepointObject.RecoverSave();
                //dies
            }

            //neardeath
        }

    }
    public void Pitfall( Transform recoverPoint , int damagetaken)
    {
        DamageTaken(damagetaken);
        Inputcontroller.transform.position = recoverPoint.position;
    }

}
