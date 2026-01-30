using UnityEngine;

public class callbackTrigger : MonoBehaviour
{
    public GameObject transitionManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int index;
    void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to a specific tag (e.g., "Player")
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has entered the trigger zone!");
            transitionManager.GetComponent<LevelTransition>().LoadScene(index);
            // Implement logic for when the player enters the trigger
        }

        // You can add other checks for different objects as needed
    }

}
