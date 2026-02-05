using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int health = 1;
    public int armour = 1;
    public bool isBoss = false;
    public Flash spriteflash;
    public float flashtime = 0.8f;
    void Start()
    {
        spriteflash = GetComponent<Flash>();
    }
    public void takeDamage(int damage)
    {
        if (armour <= 0 )
        {
            if ((health -= damage) <= 0) { 
                Destroy(this.gameObject);
            }
        }
        else
        {
            armour -= damage;
            spriteflash.Damageflash(flashtime);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
