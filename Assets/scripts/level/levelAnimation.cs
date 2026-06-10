using System;
using Unity.VisualScripting;
using UnityEngine;

public class levelAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Vector2[] speedVariation;
    [SerializeField]
    private byte[] animationFreq;
    [SerializeField]
    private byte animationCount;
    private int sum;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(animationFreq.Length < animationCount)
        {
            byte[] newfreq= new byte[animationCount];
            Array.Fill(newfreq, 1);
            newfreq.AddRange(animationFreq);
            animationFreq = newfreq;
        }
        if(animator == null)
        {
            animator = GetComponent<Animator>();
        }
        foreach(byte chance in animationFreq)
        {
            sum += chance;
        }
    }
    public void OnAnimationFinishedTrigger()
    {
        int chosenAnim = 0;
        animator.speed = Random.Range(speedVariation[chosenAnim].x, speedVariation[chosenAnim].y);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
