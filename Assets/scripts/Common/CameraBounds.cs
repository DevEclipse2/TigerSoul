using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    public Transform player;           // Reference to the player
    public float smoothSpeed = 0.125f; // Smoothing speed
    public Vector3 offset;             // Offset position for the camera
       // Maximum boundary for camera movement
    public Transform[] CameraDimensions;
    public GameObject colliderObject;
    Collider2D Collider;
    bool Valid;
    public float snapspeed = 15.0f;
    public float snapspeedLowBound = 4.0f;
    private Rigidbody2D rb;
    public bool snap = false;

    private void Start()
    {
        Collider = colliderObject.GetComponent<Collider2D>();
        rb= player.gameObject.GetComponent<Rigidbody2D>();
        snapspeed = 20;
    }
    void LateUpdate()
    {
        
        // Calculate the desired position based on the player's position and offset
        Vector3 desiredPosition = player.position + offset;

        Valid = true;
        // Clamp the position to the defined boundaries (if desired)
        foreach (Transform t in CameraDimensions)
        {
            Vector2 camposition = new Vector2(desiredPosition.x + t.localPosition.x, desiredPosition.y + t.localPosition.y);

            if (!Collider.OverlapPoint(camposition))
            {
                Vector2 point = Collider.ClosestPoint(camposition);
                desiredPosition = new Vector3(point.x - t.localPosition.x , point.y - t.localPosition.y, desiredPosition.z);
            }
        }
        if (Time.timeSinceLevelLoad < 0.1f)
        {
            transform.position = desiredPosition;
        }
        // Smoothly transition the camera's position to the desired position
        if (Valid)
        {
            if( Mathf.Abs(rb.linearVelocityY) > snapspeed)
            {

                snap = true;
            }
            if (snap)
            {
                transform.position = desiredPosition;
                if(Mathf.Abs(rb.linearVelocityY) < snapspeedLowBound)
                {
                    snap = false;
                }
            }
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        }
        
    }
}

