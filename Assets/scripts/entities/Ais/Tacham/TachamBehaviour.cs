using UnityEditor.Networking.PlayerConnection;
using UnityEngine;

public class TachamBehaviour : MonoBehaviour
{
    FollowPath basic;
    Collider2D DetectionArea;
    GameObject player;
    public float chasespeed;
    bool chasing
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
                if (hit.collider.CompareTag("Playyr"))
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
    void Engage()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
