using UnityEngine;

public class AreaTrigger : MonoBehaviour
{
    public Transform selectedPoint;
    public GameObject target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            target.GetComponent<EnvDamage>().recoverpoint = selectedPoint;
        }
    }

}
