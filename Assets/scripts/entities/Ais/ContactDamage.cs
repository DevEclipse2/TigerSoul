using UnityEngine;

public class ContactDamage : MonoBehaviour
{
    Collider2D collider;
    public GameObject contactcollider;
    public int damage;
    public Vector2 pushBack;
    public float force;
    public bool success;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collider = contactcollider.GetComponent<Collider2D>();
    }
    public void Retarget()
    {
        collider = contactcollider.GetComponent<Collider2D>();

    }
    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        //Debug.Log("touched collider");

        // Check if the collider belongs to a specific tag (e.g., "Player")
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player hit");
            if (collision.GetComponentInParent<PlayerHealth>() != null && !collision.GetComponentInParent<PlayerHealth>().invulnerable)
            {
                success = true;
                collision.gameObject.transform.position = collision.gameObject.transform.position  + new Vector3((force * pushBack.normalized * transform.localScale.normalized).x , (force * pushBack.normalized * transform.localScale.normalized).y);
            }
            collision.gameObject.GetComponentInParent<PlayerHealth>().DamageTaken(damage);

        }

        // You can add other checks for different objects as needed
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            success = false;

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
