using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering;
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }
    public void deal(AttackContainer container)
    {
        timer = 0;
        atkcontainer = container;
        isEnabled = true;
        candeal = false;
        damage = atkcontainer.damage;
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
        if (isleft)
        {
            hit = Physics2D.BoxCastAll(root.position, size, 0, Vector2.right ,-size.x - 2, enemies);
            hitdes = Physics2D.BoxCastAll(root.position, size , 0 , Vector2.right, -size.x - 2, destructibles);
        }
        else
        {

            hit = Physics2D.BoxCastAll(root.position, size, 0, Vector2.right, enemies);
            hitdes = Physics2D.BoxCastAll(root.position, size, 0, Vector2.right, destructibles);
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
