using UnityEngine;

public class autoFlip : MonoBehaviour
{

    private Rigidbody2D rb;
    public float baseScale = 1;
    Transform transform;
    Vector3 scale;
    void Start()
    {

        transform = this.gameObject.transform;
        scale = transform.localScale;
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Check if the Rigidbody is not null
        if (rb != null)
        {
            // Get the velocity
            Vector2 velocity = rb.linearVelocity;
            if (velocity.x < 0)
            {
                transform.localScale = new Vector3(scale.x * -1 * baseScale,  scale.y , scale.z);
            }else if (velocity.x > 0)
            {
                transform.localScale = new Vector3(scale.x * baseScale, scale.y, scale.z);
            }
        }
        else
        {
            Debug.LogError("Rigidbody is not assigned!");
        }
    }
}
