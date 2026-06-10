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
    private int[] compiledFreqs;
    [SerializeField]
    private byte animationCount;
    private int sum;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //these 2 add boilerplate values in case you forget to do so yourself

        if(animationFreq.Length < animationCount)
        {
            Debug.LogError("you are missing a few frequencies for animations");
            byte[] newfreq= new byte[animationCount];
            Array.Fill<byte>(newfreq, 10);
            animationFreq.CopyTo(newfreq, 0);
            animationFreq = newfreq;
        }
        if(speedVariation.Length < animationCount)
        {
            Debug.LogError("you are missing a few speed variations for animations");

            Vector2[] newspeeds = new Vector2[animationCount];
            Array.Fill<Vector2>(newspeeds, Vector2.one);
            speedVariation.CopyTo(newspeeds, 0);
            speedVariation = newspeeds;
        }
        if(animator == null)

        {
            animator = GetComponent<Animator>();
        }
        compiledFreqs = new int[animationCount];
        for (int i = 0; i < animationCount; i++)
        {
            sum += animationFreq[i];
            compiledFreqs[i] = sum;   
        }
    }
    public void OnAnimationFinishedTrigger()
    {
        int chosenAnim = 0;
        int result = UnityEngine.Random.Range(0, sum);
        for (int i = 0; i < animationCount; i++)
        {
            if(result < compiledFreqs[i])
            {
                chosenAnim = i;
                break;
            }
        }
        animator.SetInteger("Animation", chosenAnim);
        animator.speed = UnityEngine.Random.Range(speedVariation[chosenAnim].x, speedVariation[chosenAnim].y);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
