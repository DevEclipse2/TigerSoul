using UnityEngine;
using System.Collections.Generic;

public class AnimationController : MonoBehaviour
{

    public bool isLeft = false;
    public GameObject SpriteRoot;
    private Animator animator;
    bool overrideAnim = false; //this one is triggered by specific stuff
    TriggerAnim currentAnim;
    List<TriggerAnim> triggeredAnimations = new List<TriggerAnim>();
    List<TriggerAnim> animTree = new List<TriggerAnim>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    void sudoAnim(TriggerAnim anim)
    {
        // this breaks out of the regular animation tree in order to dynamically update things.
        if(anim.Scan())
        {
            anim.runAnim(animator);
            overrideAnim = true;
        }
    }

    void tryinterrupt()
    {
        //this tries to override an animation WITHOUT breaking fron the scene tree
        //priorities : regular < interrupt <  sudoanim
        
    
    }
    // Update is called once per frame
    void Update()
    {
        if (overrideAnim)
        {
            if(currentAnim.Break())
            {
                overrideAnim = false;
                anim.quitAnim(animator)
                currentAnim = null;
            }
        }
    }
}
