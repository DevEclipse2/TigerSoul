using UnityEngine;

public class InteractTransition : MonoBehaviour
{
    public GameObject transitionManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int index;
    bool EnteredTrigger;
    public GameObject inputparserGameobject;
    private InputParser parser;
    
    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        //Debug.Log("touched collider");

        // Check if the collider belongs to a specific tag (e.g., "Player")
        if (collision.CompareTag("Player"))
        {
            EnteredTrigger = true;
        }

        // You can add other checks for different objects as needed
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EnteredTrigger = false;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        parser = inputparserGameobject.GetComponent<InputParser>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EnteredTrigger) 
        {
            if (parser.recentInput.Contains(Input.Interact))
            {
                if (parser.pressed[parser.recentInput.IndexOf(Input.Interact)])
                {
                    transitionManager.GetComponent<LevelTransition>().LoadScene(index);

                }
            }
        }
    }
}
