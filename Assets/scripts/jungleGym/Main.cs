using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public TextMeshProUGUI timer;
    float time = 0;
    int levers = 0;
    [SerializeField]
    int scene = 0;
    [SerializeField]
    GameObject[] leverObjects;
    [SerializeField]
    TextMeshProUGUI leverC;
    float updatetimer = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(levers == 7)
        {
            Data.time = time;
            SceneManager.LoadScene(scene);
        }
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
        leverC.text = levers.ToString() + "/7 Levers";
        timer.text = time.ToString();
        time += Time.deltaTime;
        updatetimer += Time.deltaTime;
    }
}
