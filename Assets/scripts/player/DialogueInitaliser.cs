using UnityEngine;

public class DialogueInitaliser : MonoBehaviour
{
    public GameObject parser;
    private InputParser inputParser;
    private DialogBase dBase;
    public int index = -1;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DialogBox"))
        {
            dBase = collision.gameObject.GetComponent<DialogBase>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("DialogBox"))
        {
            dBase = null;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         inputParser = parser.GetComponent<InputParser>();
    }

    // Update is called once per frame
    void Update()
    {
        if(index == -1)
        {
            index = inputParser.recentInput.IndexOf(Input.Interact);
        }
        else
        {
            if(inputParser.pressed[index]) 
            {
                if(dBase != null) 
                { 
                    dBase.ProgressDialog();
                }
            }
        }
    }
}
