using UnityEngine;

public class Destructible : MonoBehaviour
{
    public int health = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void destroyThis()
    {
        health--;
        if(health <= 0)
        {
            Debug.Log("destroy");
            Destroy(gameObject);
            // cool stuff here later
            Destroy( this );


        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
