using UnityEngine;

public class Lockdown : MonoBehaviour
{
    public GameObject bind;
    public float timer;
    private float time;
    public Vector2 topPos;
    Vector2 original;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        original = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (bind == null) {
            time += Time.deltaTime;
            if (time > timer)
            {
                Destroy(this.gameObject);
            }
            transform.position = original + topPos * time/timer;
        }
        
    }
}
