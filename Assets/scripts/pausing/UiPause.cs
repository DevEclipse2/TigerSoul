using UnityEngine;
using UnityEngine.Rendering.Universal;

public class UiPause : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject inputParser;
    private InputParser parser;
    public GameObject pauseMenu;
    public float checkPause = 0.3f;// cooldown of 0.3f
    public float timescale;
    public bool shouldPause{get;private set;} = false;
    void Start()
    {
        timescale = Time.timeScale;
        pauseMenu.SetActive(false);
        parser = inputParser.GetComponent<InputParser>();
    }

    // Update is called once per frame
    void Update()
    {
        if(checkPause > 0 &&!shouldPause)
        {
            checkPause -= Time.deltaTime;
            return;
        }
        if (parser != null) {
            if (parser.CheckPress(Input.Pause))
            {
                if (shouldPause)
                {
                    shouldPause = false;
                    Time.timeScale = timescale;

                }
                else
                {
                    shouldPause = true;
                    Time.timeScale = 0;
                }
                //shouldPause = !shouldPause;
                Pause.SetGlobalPause(shouldPause);
                pauseMenu.SetActive(shouldPause);
                    checkPause = 0.3f;
                return;
            }
            
        }        
    }
}
