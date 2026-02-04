using UnityEngine;
using System.Collections.Generic;
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


    public Transform part2Weight1Range1;
    public Transform part2Weight2Range1;
    float percentage;

    public GameObject lineObject;
    private LineRenderer linerenderer;
    public bool updateDraw = false;
    public List<Transform> pointA;
    public List<Transform> pointB;
    public Color linecolor;

    public Transform HighPosition;
    bool part1;
    bool part1complete;
    bool part2;

    
    
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
    public void drawline()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if(updateDraw)
        {
            for(int i = 0; i < pointA.Count; i++)
            {
                linerenderer.positionCount = 2;
                linerenderer.SetPosition(0, pointA[i].position);
                linerenderer.SetPosition(1, pointB[i].position);
                
            }
        }
        if (!part1 && !part1complete && !AirTrigger.GetComponent<CollisionHandle>().IsTriggered) {
            part1 = true;
            
        }
        if(part1)
        {
            Debug.Log(percentage);
            percentage =  Mathf.Abs(p1w2Original.y - part1Weight2Range1.position.y) / distance2;
            platform.transform.position = new Vector2(platform.transform.position.x, p1w1Original.y + distance1 * percentage);
            if (groundedTrigger.GetComponent<CollisionHandle>().IsTriggered)
            {
                part1 = false;
                part1complete = true;
                //platform.transform.position = Vector2.Lerp(part1Weight1Range1.position, part1Weight1Range2.position , 0.4f);
            }
        }
    }
}
