using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class StopDeathAnim : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    [SerializeField]
    GameObject[] turretobject;
    public bool turretToss;
    public float forceMult = 1;
    public PhysicsMaterial2D material;
    public void StopAnim()
    {
        animator.SetBool("Dead", false);
        Destroy(animator);
    }
    public void TurretToss()
    {
        foreach (GameObject gameObject in turretobject)
        {
            gameObject.AddComponent<Rigidbody2D>();
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            rb.mass = 40;
            rb.sharedMaterial = material;
            rb.constraints = RigidbodyConstraints2D.None;
            rb.angularVelocity = Random.Range(-82f, 114.2f);
            rb.linearVelocityX = Random.Range(-1.3f, 1.3f);
            rb.linearVelocityY = Random.Range(6.2f * forceMult, 13.3f * forceMult);
        }
    }
}
