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
    public GameObject target;
    public GameObject flashtarget;

    public GameObject DeathAnimator;
    public GameObject[] toDisable;
    public GameObject[] toEnable;
    void Start()
    {
        if(DeathAnimator == null)
        {
            Debug.LogError("missing deathAnimator!");
        }
        if(target == null)
        {
            target = gameObject;

        }
        if(flashtarget == null)
        {
            spriteflash = target.GetComponent<Flash>();
        }
        else
        {
            spriteflash = flashtarget.GetComponent<Flash>();

        }
    }
    public void takeDamage(int damage)
    {
        if (armour <= 0 )
        {
            if ((health -= damage) <= 0) {
                if (!isBoss)
                {
                    DeathAnimator.GetComponent<Animator>().SetBool("Dead", true);
                    DeathAnimator.GetComponent<StopDeathAnim>().turretToss = true;
                    if(health < -1)
                    {
                        DeathAnimator.GetComponent<StopDeathAnim>().forceMult = -health * 0.35f + 1;
                    }
                    else
                    {
                        DeathAnimator.GetComponent<StopDeathAnim>().forceMult = 0.35f;

                    }
                    foreach (GameObject gameObject in toDisable)
                        {
                            gameObject.SetActive(false);

                        }

                    foreach (GameObject gameObject in toEnable)
                    {
                        gameObject.SetActive(true);

                    }
                }
            }
            else
            {
                spriteflash.Damageflash(flashtime);

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
