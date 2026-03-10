using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelFade : MonoBehaviour
{
    public static LevelFade instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Image fadeImage;
    public float FadeOutTime = 0.4f;
    public float FadeInTime = 0.6f;
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
        timer = 0;
        fadeImage.color = Color.black;
        fadeColor = fadeImage.color;

        fadeIn = true;

    }
    
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer < 0.2f)
        {
            completed = false;
        }
        if (fadeOut) {
            fadetimer += Time.deltaTime;
            fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 1.2f * (fadetimer / FadeOutTime));
        }
        else if (fadeIn) {
            fadetimer += Time.deltaTime;
            fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 1.2f * (FadeInTime - fadetimer) / FadeInTime);

        }
        if((fadetimer > FadeInTime && fadeIn)|| (fadetimer > FadeOutTime && fadeOut))
        {   
            fadeIn = false;
            fadeOut = false;
            timer = 0;
            fadetimer = 0;
            completed = true;
           
            
        }
    }
}
