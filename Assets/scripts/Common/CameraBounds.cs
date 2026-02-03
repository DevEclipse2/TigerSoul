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
    private void Start()
    {
        Collider = colliderObject.GetComponent<Collider2D>();
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

        // Smoothly transition the camera's position to the desired position
        if (Valid)
        {
            
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        }
        
    }
}

