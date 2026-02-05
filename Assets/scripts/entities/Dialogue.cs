using UnityEngine;

public class Dialogue : MonoBehaviour
{
    Collider2D collider;

    bool inrange;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }
    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inrange = true;
        }
    }
    void OnTriggerExit2D(UnityEngine.Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inrange = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
