using UnityEngine;

public class CollisionHandle : MonoBehaviour
{
    Collider2D collider;
    public bool IsTriggered;
    public string filter;
    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.CompareTag(filter))
        {
            IsTriggered = true;
            Debug.Log("enter");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(filter)) {
            IsTriggered = false;
        }

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }
    // Update is called once per frame
}
