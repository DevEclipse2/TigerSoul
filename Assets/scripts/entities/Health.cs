using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int health;
    public int armour;
    public bool isBoss;
    void Start()
    {
        
    }
    public void takeDamage(int damage)
    {
        if (armour <=0 )
        {
            if ((health -= damage) <= 0) { 
            Destroy(this);
            }
        }
        else
        {
            armour -= damage;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
