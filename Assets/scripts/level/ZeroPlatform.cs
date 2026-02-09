using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ZeroPlatform : MonoBehaviour
{
    Collider2D collider;
    public GameObject Target;
    public bool inrange;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inrange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inrange = false;
            Target.SetActive(true);
        }
    }
    public void OnCrouch()
    {
        Debug.Log("crouch");
        if (inrange)
        {
            Target.SetActive(false);
        }
        //if (context.canceled)
        //{
        //    Target.SetActive(true);
        //}
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
