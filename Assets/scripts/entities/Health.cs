using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int health = 1;
    public int armour = 1;
    public bool isBoss = false;
    void Start()
    {
        
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
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
