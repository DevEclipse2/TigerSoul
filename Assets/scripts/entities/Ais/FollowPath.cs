using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public Transform[] Point;
    public Vector2[] pointvec;
    public bool disable; // if this function is overriden by other more important events
    public bool gravity; // this is for flying enemies
    public LayerMask groundlayer;
    public int target;
    public float distance;
    public float patrolspeed;
    Rigidbody2D rb;
    public bool random; // randomly goes to any node
    public GameObject Rigidbodycontainer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pointvec = new Vector2[Point.Length];
        if(Rigidbodycontainer == null)
        {
            rb = GetComponent<Rigidbody2D>();

        }
        else
        {
            rb = Rigidbodycontainer.GetComponent<Rigidbody2D>();
        }

        if (gravity)
        {
            for (int i = 0; i < pointvec.Length; i++)
            {
                pointvec[i] = GetClosestPointBelow(Point[i]);
            }
        }
        else
        {
            for (int i = 0; i < pointvec.Length; i++)
            {
                pointvec[i] = Point[i].position;
            }
        }

        target = 0;
    }
    private Vector3 GetClosestPointBelow(Transform targetTransform)
    {
        // Define a ray pointing downwards from the transform's position
        // Perform the raycast
        Vector3 rayOrigin = targetTransform.position;
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, 99, groundlayer))
        {
            // If the ray hits a collider, calculate the closest point on that collider
            Vector3 closestPoint = hit.collider.ClosestPoint(targetTransform.position);
            return closestPoint; // Return the closest point found
        }

        // Return the original position if nothing is hit
        return targetTransform.position;
    }
    public void patrol()
    {
        //Debug.Log(this.gameObject.name + Vector2.Distance(this.gameObject.transform.position, pointvec[target]));
        if (Vector2.Distance(this.gameObject.transform.position, pointvec[target]) < distance)
        {
            if (!random)
            {
                target++;
            }
            else
            {
                target = Random.Range(0,pointvec.Length - 1);
            }
            target %= pointvec.Length;
        }
        Vector2 current = this.gameObject.transform.position;

        if (gravity) 
        {
            float direction = pointvec[target].x - current.x;

            // Determine the new x velocity
            Vector2 newVelocity = new Vector2(direction, 0).normalized * patrolspeed;

            // Set the Rigidbody2D's velocity
            rb.linearVelocity = newVelocity;
        }
        else
        {
            Vector2 direction = (pointvec[target] - current).normalized;

            // Move the Rigidbody towards the target
            //Debug.Log(direction);
            rb.linearVelocity = direction * patrolspeed;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!disable)
        {
            patrol();
        }
    }
}
