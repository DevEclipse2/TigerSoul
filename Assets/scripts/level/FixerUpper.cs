using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class FixerUpper : MonoBehaviour
{
    public int healingAmount = 5; // Amount of health to gain per second
    public int TargetHealth = 75;
    public PlayerHealth playerHealth; // Reference to the PlayerHealth component
    public GameObject root; // Reference to the PlayerHealth component
    bool heal = false;
    float aggregateheal = 0f;
    Collider2D collider;
    private void Start()
    {
        collider = GetComponent<Collider2D>();
        playerHealth = root.GetComponent<PlayerHealth>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Check if the collider belongs to the player
        {

            Debug.Log("enter");
            heal = true;
        }
        Datapersistence.SetSave(transform.position, SceneManager.GetActiveScene().name );
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Stop healing when player exits
        {
            heal = false;

        }
    }
    private void Update()
    {
        if (heal)
        {
            if(playerHealth.health < TargetHealth)
            {
                //Debug.Log(aggregateheal);

                aggregateheal += healingAmount * Time.deltaTime;
                // Heal the player by the integer healing amount every frame
                // Convert the healing amount to an integer
                int healingAmountInt = Mathf.FloorToInt(aggregateheal);

                // Increase health by the integer value
                if (healingAmountInt > 0) // Only apply positive healing
                {
                    aggregateheal -= healingAmountInt;
                    playerHealth.IncreaseHealth(healingAmountInt);
                }

            }
        }
    }
}
