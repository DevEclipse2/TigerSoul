using UnityEngine;
using System.Collections.Generic;
public class CheckCartridge : MonoBehaviour
{
    public GameObject platform;
    public GameObject CounterBalance;

    
    public GameObject groundedTrigger;
    public GameObject platformTrigger;
    public GameObject counterbalanceDeactivationTrigger;
    
    
    public Transform part1Weight1Range1;
    public Transform part1Weight1Range2;
    public Transform part1Weight2Range1;
    public Transform part1Weight2Range2;
    public Transform part2Weight1Range1;
    public Transform part2Weight1Range2;
    public Transform part2Weight2Range1;
    public Transform part2Weight2Range2;
    float percentage;

    public GameObject lineObject;
    private LineRenderer linerenderer;
    public bool updateDraw = false;
    public List<Transform> pointA;
    public List<Transform> pointB;
    public Color linecolor;

    public Transform HighPosition;
    bool part1;
    bool part2;

    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        linerenderer = lineObject.GetComponent<LineRenderer>();
        groundedTrigger.GetComponent<Collider2D>().OnTriggerEnter2D += onCounterbalanceGrounded();
        linerenderer.startColor = linecolor;
        linerenderer.endColor = linecolor;
    }
    public void CounterbalanceDown()
    {
          part1 = true;
    }
    public void CounterbalanceCleared()
    {
        part2 = true;
        part1 = false;
    }
    public void EquilibriumReached()
    {
        updateDraw = false;
        part2 = false;
        part1 = false;
    }
    void onCounterbalanceGrounded(UnityEngine.Collider2D collision)
    {
        if(collider.CompareTag("Box")
        {
            Debug.Log("box landed");
            RigidBody2D rb = platform.GetComponent<RigidBody2D>();  
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY
            //unfreezes the thing
        }
    }
    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if(collider.CompareTag("Box")
        {
            Debug.Log("box landed");
            RigidBody2D rb = platform.GetComponent<RigidBody2D>();  
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY
            //unfreezes the thing
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(updateDraw)
        {
            for(int i = 0; i < pointA.Count; i++)
            {
                lineRenderer.positionCount = 2;
                lineRenderer.SetPosition(0, pointA[i]);
                lineRenderer.SetPosition(1, pointB[i]);
            }
        }
        if(part1)
        {
        }
    }
}
