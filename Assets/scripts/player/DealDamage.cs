using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public class DealDamage : MonoBehaviour
{
    public bool isleft = false;
    public Transform root;
    public Vector2 size;
    public LayerMask enemies;
    public LayerMask Destructibles;
    List<GameObject> targetedEnemy;
    List<GameObject> targetedDestructible;
    public List<GameObject> Invokable;
    public int damage = 0;
    bool isEnabled = false;
    bool candeal = false;
    float timer = 0;
    float maxtimer = 0;
    AttackContainer atkcontainer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }
    public void deal(AttackContainer container)
    {
        atkcontainer = container;
        isEnabled = true;
        candeal = false;
        damage = atkcontainer.damage;
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
            hit = Physics2D.BoxCastAll(root.position, size, 0, transform.right * -1 , enemies);
            hitdes = Physics2D.BoxCastAll(root.position, size , 0 , transform.right * -1, enemies);
        }
        else
        {
            hit = Physics2D.BoxCastAll(root.position, size, 0, transform.right, enemies);
            hitdes = Physics2D.BoxCastAll(root.position, size, 0, transform.right, enemies);
        }

            foreach (RaycastHit2D ray in hit)
            {
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
            timer = 0;
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
