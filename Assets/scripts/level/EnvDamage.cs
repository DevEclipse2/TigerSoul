using UnityEngine;

public class EnvDamage : MonoBehaviour
{
    public GameObject playerhealth; 
    PlayerHealth Health;
    public Transform recoverpoint;
    public int damage = 25;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Health = playerhealth.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        //Debug.Log("touched collider");

        // Check if the collider belongs to a specific tag (e.g., "Player")
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player fallen");
            Health.Pitfall(recoverpoint, damage);

        }

        // You can add other checks for different objects as needed
    }
}
