using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public TextMeshProUGUI timer;
    float time = 0;
    int levers = 0;
    [SerializeField]
    string scene;
    [SerializeField]
    GameObject[] leverObjects;
    [SerializeField]
    TextMeshProUGUI leverC;
    float updatetimer = 0;
    public bool force;
    bool intrigger;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        Debug.Log("entered");

        if (levers == 7 || force)
        {
            Data.time = time;
            SceneManager.LoadScene(scene);
        }
        else
        {
            intrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        intrigger = false;
    }
    // Update is called once per frame
    void Update()
    {

        int levercount = 0;
        foreach(GameObject go in leverObjects)
        {
            if (go.GetComponent<PullLever>().pulled)
            {
                levercount++;
            }
        }
        levers = levercount;
        if (intrigger)
        {
            leverC.text = "missing a few levers: " + levers.ToString() + "/7 !";
        }
        else
        {
            leverC.text = levers.ToString() + "/7 Levers";
        }
        timer.text = time.ToString();
        time += Time.deltaTime;
        updatetimer += Time.deltaTime;
    }
}
