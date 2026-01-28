using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class AttackContainer : MonoBehaviour
{
    // this stores variables
    public bool groundedAttack;
    public int damage;
    public GameObject BasicDeal;
    public GameObject[] invokeFunction; //this function gets called when you deal damage
    public List<int> BranchKey = new List<int>();
    public List<GameObject> BranchAttack = new List<GameObject>();
    public float cutIn;
    public float hold;
    public float cutOut;
    public string args;
    public int DealDamage()
    {
        if (BasicDeal.TryGetComponent<DealDamage>(out DealDamage deal))
        {
            deal.deal(this);
        }
        
         foreach (GameObject func in invokeFunction)
        {
            if (func != null)
            {
                
                    try
                    {
                        func.SendMessage("invoke");
                    }
                    catch
                    {

                    }
            }

        }
        return 0;
    }
}
