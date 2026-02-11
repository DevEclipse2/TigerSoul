using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
public class DealDamage : MonoBehaviour
{
    public bool isleft = false;
    public Transform root;
    public Vector2 size;
    public LayerMask enemies;
    public LayerMask destructibles;
    List<GameObject> targetedEnemy = new List<GameObject>();
    List<GameObject> targetedDestructible = new List<GameObject>();
    public List<GameObject> Invokable = new List<GameObject>();
    public int damage = 0;
    bool isEnabled = false;
    bool candeal = false;
    public float timer = 0;
    float maxtimer = 0;
    AttackContainer atkcontainer;
    float slowtimer;
    PlayerHealth PlayerHealth;
    public GameObject EnemyHit;
    public GameObject DestructibleHit;
    public float lifespan;
    private bool direction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private System.Collections.IEnumerator DestroyParticleSystemAfterDelay(float delay, GameObject particleObject)
    {
        // Wait for the duration of the particle system
        yield return new WaitForSeconds(delay);

        // Destroy the particle system GameObject
        Destroy(particleObject);
        yield return null;
    }

    void Start()
    {
        PlayerHealth = GetComponentInParent<PlayerHealth>();
    }
    public void deal(AttackContainer container)
    {
        timer = 0;
        atkcontainer = container;
        isEnabled = true;
        candeal = false;
        damage = atkcontainer.damage;
        direction = isleft;
        dealDamage();
    }
    public void clear()
    {
        targetedEnemy.Clear();
        targetedDestructible.Clear();
    }
    public void dealDamage()
    {
        RaycastHit2D[] hit;
        RaycastHit2D[] hitdes;
        if (direction)
        {
            //Debug.Log("left");
            hit = Physics2D.BoxCastAll(root.position, size, 0, Vector2.right ,-size.x - 2, enemies);
            hitdes = Physics2D.BoxCastAll(root.position, size , 0 , Vector2.right, -size.x - 2, destructibles);
        }
        else
        {
            //Debug.Log("rigth");
            hit = Physics2D.BoxCastAll(root.position, size, 0, Vector2.right, 2, enemies);
            hitdes = Physics2D.BoxCastAll(root.position, size, 0, Vector2.right, 2 , destructibles);
        }
        
        foreach (RaycastHit2D ray in hit)
        {
            //Debug.Log("iterate");
                if (!targetedEnemy.Contains(ray.collider.gameObject))
                {
                    targetedEnemy.Add(ray.collider.gameObject);
                    if (ray.collider.gameObject.TryGetComponent<Health>(out Health health))
                    {
                        
                        health.takeDamage(damage);
                        Time.timeScale = 0.9f;

                        GameObject particleObject = Instantiate( EnemyHit , ray.collider.gameObject.transform.position, Quaternion.identity);
                        StartCoroutine(DestroyParticleSystemAfterDelay(lifespan, particleObject));


                    PlayerHealth.IncreaseHealth(Random.Range(6, 8));
                        if (ray.collider.gameObject.CompareTag("Boss"))
                        {
                            PlayerHealth.IncreaseHealth(4);

                        }
                    }
                    else
                    {
                        Debug.Log("object missing valid health script!" + gameObject.name);
                    }
                }
        }
        foreach (RaycastHit2D ray in hitdes)
        {
            if (!targetedDestructible.Contains(ray.collider.gameObject))
            {
                targetedDestructible.Add(ray.collider.gameObject);
                if (ray.collider.gameObject.TryGetComponent<Destructible>(out Destructible destructible)) {
                    destructible.destroyThis();
                    PlayerHealth.IncreaseHealth(Random.Range(2, 4));
                    GameObject particleObject = Instantiate(DestructibleHit, ray.collider.gameObject.transform.position, Quaternion.identity);
                    particleObject.GetComponent<ParticleSystem>().Play();

                    StartCoroutine(DestroyParticleSystemAfterDelay(lifespan, particleObject));
                }
                else
                {
                    Debug.Log("object missing destructible!" + gameObject.name);
                }
            }
            
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale < 1.0f)
        {
            slowtimer += Time.deltaTime;
            if(slowtimer > 0.3f)
            {
                slowtimer = 0;
                Time.timeScale = 1.0f;
            }
        }
        if (isEnabled)
        {
            if (candeal)
            {
                dealDamage();
                if ((timer += Time.deltaTime) > atkcontainer.hold)
                {
                    candeal = false;
                    isEnabled = false;
                    atkcontainer = null;
                    clear();
                }
            }
            else
            {
                if((timer += Time.deltaTime) > atkcontainer.cutIn)
                {
                    candeal = true;
                    timer = 0;
                }
            }
        }
    }
}
