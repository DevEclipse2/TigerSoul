using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class TriggerAnim : MonoBehaviour
{
    Animator animator;
    public string notes;
    public string Animname;
    public Transform Entertrigger;
    public Vector2 size;
    public LayerMask mask;
    public Transform Exittrigger;
    public bool requireExit = false;
    public List<float> cutInPoint = new List<float>();
    public List<float> cutOutPoint = new List<float>();
    public AnimationClip clip;
    public List<string> AnimatorProperties;
    public List<bool> TargetValue;
    public List<bool> TargetExitValue;
    public List<int> properties = new List<int>();


    //need to fix linkedactivate



    public TriggerAnim linkedActivate;
    public GameObject reply;
    public bool tree;
    public int index;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        while (AnimatorProperties.Count > TargetValue.Count) {
            TargetValue.Add(false);
        }
        while (AnimatorProperties.Count > TargetExitValue.Count)
        {
            TargetExitValue.Add(false);
        }
    }
    public void runAnim(Animator anim ,bool overload)
    {
        if (overload)
        {
            //force change
            anim.Play(clip.name);
        }
        else
        {
            for (int i = 0; i < AnimatorProperties.Count; i++) {
                anim.SetBool(AnimatorProperties[i], TargetValue[i]);
            }
        }
    }
    public void quitAnim(Animator anim)
    {
        for (int i = 0; i < AnimatorProperties.Count; i++)
        {
            anim.SetBool(AnimatorProperties[i], TargetExitValue[i]);
        }
    }
    public bool breakOut()
    {   

        return !properties.Contains(AnimProperties.locked);
    }
    public bool Scan()
    {
        return Physics2D.BoxCast(Entertrigger.position, size, 0, transform.up, mask);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
