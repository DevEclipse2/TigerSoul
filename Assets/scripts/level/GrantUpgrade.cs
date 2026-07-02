using System;
using System.Reflection;
using System.Transactions;
using TreeEditor;
using UnityEngine;

public class GrantUpgrade : MonoBehaviour
{
    bool EnteredTrigger;
    public GameObject inputparserGameobject;
    private InputParser parser;
    public string name;
    public GameObject deleteObj;
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

                    Type upgradeloadertype = typeof(upgradeLoader);
                    PropertyInfo prop = upgradeloadertype.GetProperty(name, BindingFlags.Public|BindingFlags.Static);
                    prop.SetValue(null, true);
                    Destroy(deleteObj);
                }
            }
        }
    }
    
}
