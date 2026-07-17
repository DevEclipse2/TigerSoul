using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class StopDeathAnim : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    [SerializeField]
    Rigidbody2D rb;
    public bool turretToss;
    public float forceMult = 1;
    public void StopAnim()
    {
        animator.SetBool("Dead", false);
        rb.angularVelocity = Random.Range(-82f, 114.2f);
        rb.linearVelocityX = Random.Range(-1.3f, 1.3f);
        rb.linearVelocityY = Random.Range(6.2f*forceMult, 13.3f*forceMult);
    }
}
