using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLoadPosition : MonoBehaviour
{
    public Transform[] Spawns;
    public GameObject Input;
    public GameObject saveContainer;

    public string Levelid;
    public bool isdead;
    public Vector2 location;
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void setPosition(int index)
    {
        Input.GetComponent<Transform>().position = new Vector3(Spawns[index].position.x , Spawns[index].position.y , Input.GetComponent<Transform>().position.z);
    }
    public void setPositionVector(Vector2 Position)
    {
        Input.GetComponent<Transform>().position = new Vector3(Position.x, Position.y, Input.GetComponent<Transform>().position.z);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
