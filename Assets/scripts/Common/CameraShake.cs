using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform Shake;
    public Vector3 offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //offset = transform.position - this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.position = transform.position - offset;
        this.transform.position = Shake.position;
    }
}
