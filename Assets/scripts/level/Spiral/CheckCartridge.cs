using UnityEngine;
using System.Collections.Generic;
using System;
public class CheckCartridge : MonoBehaviour
{
    public GameObject platform;
    public GameObject CounterBalance;

    
    public GameObject groundedTrigger;
    public GameObject AirTrigger;
    public GameObject platformTrigger;
    public GameObject counterbalanceDeactivationTrigger;
    
    
    public Transform part1Weight1Range1;
    public Transform part1Weight1Range2;
    Vector2 p1w1Original;
    float distance1;
    public Transform part1Weight2Range1;
    Vector2 p1w2Original;
    public Transform part1Weight2Range2;
    float distance2;

    Vector2 p2w2Original;
    Vector2 p2w1Original;

    public Transform part2Weight1Range1;
    public Transform part2Weight2Range1;
    float percentage;
    float dist3;
    float dist4;
    public GameObject lineObject;
    private LineRenderer linerenderer;
    public bool updateDraw = false;
    public Transform[] pointA;
    public Transform[] pointB;
    LineRenderer[] lines;
    public Color linecolor;

    public Transform HighPosition;
    bool part1;
    bool part1complete;
    bool part2;
    bool part2complete;
    public LineRenderer linePrefab;
    public GameObject Squisher;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        linerenderer = lineObject.GetComponent<LineRenderer>();
        linerenderer.startColor = linecolor;
        linerenderer.endColor = linecolor;
        distance1 = Mathf.Abs(part1Weight1Range1.position.y - part1Weight1Range2.position.y);
        distance2 = Mathf.Abs(part1Weight2Range1.position.y - part1Weight2Range2.position.y);
        p1w1Original = part1Weight1Range1.position;
        p1w2Original = part1Weight2Range1.position;
        lines = new LineRenderer[pointA.Length];
        for (int i = 0; i < pointA.Length; i++)
        {
            lines[i] = Instantiate(linePrefab);
            
        }
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
    public void drawline(int index)
    {

        LineRenderer lineRenderer = lines[index];
        lineRenderer.positionCount = 2; // 2 points for the line

        // Set start and end points of the line
        lineRenderer.SetPosition(0, pointA[index].position);
        lineRenderer.SetPosition(1, pointB[index].position);
    }
    // Update is called once per frame
    void Update()
    {
        if(updateDraw)
        {
            for(int i = 0; i < pointA.Length; i++)
            {
                drawline(i);
            }
        }
        if (!part1 && !part1complete && !AirTrigger.GetComponent<CollisionHandle>().IsTriggered) {
            part1 = true;
            
        }
        if(part1)
        {
            //Debug.Log(percentage);
            percentage =  Mathf.Abs(p1w2Original.y - part1Weight2Range1.position.y) / distance2;
            platform.transform.position = new Vector2(platform.transform.position.x, p1w1Original.y + distance1 * percentage);
            if (groundedTrigger.GetComponent<CollisionHandle>().IsTriggered)
            {
                part1 = false;
                part1complete = true;
                //platform.transform.position = Vector2.Lerp(part1Weight1Range1.position, part1Weight1Range2.position , 0.4f);
            }
        }
        else if (part2)
        {
            //Debug.Log(percentage);
            percentage = Mathf.Abs(p2w1Original.y - platform.transform.position.y) / dist3;
            CounterBalance.transform.position =  new Vector2(CounterBalance.transform.position.x , p2w2Original.y + dist4 * percentage);

        }
        if (part1complete && !groundedTrigger.GetComponent<CollisionHandle>().IsTriggered && platformTrigger.GetComponent<CollisionHandle>().IsTriggered && !part2)
        {
            part2 = true;
            platform.AddComponent<Rigidbody2D>();
            Rigidbody2D rb =  platform.GetComponent<Rigidbody2D>();
            rb.mass = 600;
            rb.gravityScale = 0.4f;
            rb.freezeRotation = true;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            if(CounterBalance.GetComponent<Rigidbody2D>() != null)
            {
                Destroy(CounterBalance.GetComponent<Rigidbody2D>());
            }
            else
            {
                Debug.Log("missing");
            }
            //this is where stuff happens
            p2w1Original = platform.transform.position;
            p2w2Original = CounterBalance.transform.position;
            dist3 = MathF.Abs( p2w1Original.y - part2Weight1Range1.position.y);
            dist4 = MathF.Abs( p2w2Original.y - part2Weight2Range1.position.y);
            Debug.Log(dist3);
            Debug.Log(dist4);
            Destroy(Squisher);
        }

    }
}
