using System.Collections;
using UnityEngine;

public class PullLever : MonoBehaviour
{
    Destructible d;
    Animator animator;
    bool disable;
    public bool pulled { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        d = GetComponent<Destructible>();
    }
    public void AlreadyPulled()
    {
        animator.SetInteger("Value", 3);
        disable = true;
        pulled = true;
    }
    public void InvokedPullLever()
    {
        animator.SetInteger("Value", 1);
        disable = true;
        pulled = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (d != null)
        {
            if (!disable)
            {
                if (d.health < 30)
                {
                    d.health = 30;
                    animator.SetInteger("Value", 1);
                    disable = true;
                    pulled = true;
                }
            }
        }
    }
}
