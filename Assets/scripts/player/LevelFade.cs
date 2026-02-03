using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelFade : MonoBehaviour
{
    public static LevelFade instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Image fadeImage;
    public float FadeTime = 0.5f;
    public float fadetimer = 0;
    Color fadeColor;
    public bool fadeIn;
    public bool fadeOut;
    public bool completed;
    float timer;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {

        fadeColor = fadeImage.color;
        fadeIn = true;

    }
    
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 0.2f)
        {
            completed = false;
        }
        if (fadeOut) {
            fadetimer += Time.deltaTime;
            fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 1.2f * (fadetimer / FadeTime));
        }
        else if (fadeIn) {
            fadetimer += Time.deltaTime;
            fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 1.2f * (FadeTime - fadetimer) / FadeTime);

        }
        if(fadetimer > FadeTime)
        {   
            fadeIn = false;
            fadeOut = false;
            timer = 0;
            fadetimer = 0;
            completed = true;
           
            
        }
    }
}
