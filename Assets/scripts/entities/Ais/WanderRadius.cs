using UnityEngine;
public class FollowPath : MonoBehaviour
{
    public Transform home;
    public float radius;
    public bool disable; // if this function is overriden by other more important events
    public Vector2 target;
    public float distance;
    public float patrolspeed;
    public LayerMask groundLayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    void selectrandomPoint()
    {
      bool possible = false;
      do
      {
          float dist = Random.Range(radius * 0.3f , radius);
          float dir = Random.Range(0 , 2 * Mathf.PI);
          Vector2 direction = new Vector2(Mathf.Cos(dir) , Mathf.Sin(dir));
          target = direction * dist + home.position;
          Vector3 directionToTarget = target.position - transform.position;
              // Optional: Add raycast for further line-of-sight checking
              RaycastHit hit;
              if (Physics.Raycast(transform.position, directionToTarget, out hit, dist ,groundLayer))
              {
                 possible = false; // Check if the ray hit the target
              }
              else
              {
                possible = true;
              }            
        } while (!possible)
      }
    
    // Update is called once per frame
    void Update()
    {
        if (!disable)
        {
            Vector3 direction = (target - transform.position).normalized;
            direction *= patrolspeed;
            transform.position = transform.position + direction; 
             
            if(Vector2.Distance(this.gameObject.transform.position, target) < distance )
            {
              selectRandomPoint();
            }
        }
    }
}
