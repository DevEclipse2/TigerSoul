using UnityEngine;

public class CollisionHandle : MonoBehaviour
{
    public delegate void MultiTriggerHandle(UnityEngine.Collider2D collision)
    public event MultiTriggerHandle triggerEnter;
    Collider2D collider;
    protected virtual void triggerEnter( UnityEngine.Collider2d collision)
    {
        triggerEnter?.Invoke(collision);
    }
    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        triggerEnter(collision);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }
    // Update is called once per frame
}
