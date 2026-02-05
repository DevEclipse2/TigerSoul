using UnityEditor.Networking.PlayerConnection;
using UnityEngine;

public class TachamBehaviour : MonoBehaviour
{
    FollowPath basic;
    Collider2D DetectionArea;
    GameObject player;
    public GameObject entity;
    public float chasespeed;
    bool chasing;
    public Transform scanbox;
    float Scanheight;
    bool targetground;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Scanheight = scanbox.localScale.y;
        DetectionArea = GetComponent<Collider2D>();
        basic = GetComponent<FollowPath>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector3 direction = player.transform.position - transform.position;

            // Perform the raycast
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction.normalized, out hit))
            {
                if (hit.collider.CompareTag("Player") && Mathf.Abs(hit.transform.position.y - entity.transform.position.y) < Scanheight)
                {
                    //within line of sight
                    Chase();
                    
                }
            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Disengage();

        }
    }
    void Disengage()
    {
        basic.enabled = true;
    }
    void Chase()
    {
        basic.enabled = false;
        
    }
    void MoveTowardsPlayer(){
        if(targetground)
        {
            //float left = Mathf.Clamp(player.transform.position.x - entity.transform.position , -1 ,1 );
            
        }
    
    }
    void Engage()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
