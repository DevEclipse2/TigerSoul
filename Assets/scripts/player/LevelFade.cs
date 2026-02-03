using System.Threading;
using UnityEngine;
using UnityEngine.UI;
public class LevelFade : MonoBehaviour
{
    public static LevelFade instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Image fadeImage;
    public float FadeTime = 0.3f;
    float fadetimer = 0;
    Color fadeColor;
    public bool fadeIn { get; private set; }
    public bool fadeOut { get; private set; }
    public bool completed;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        fadeColor = fadeImage.GetComponent<Color>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeOut) {
            fadetimer += Time.deltaTime;
            fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 1 * fadetimer / FadeTime);
        }
        else if (fadeIn) {
            fadetimer += Time.deltaTime;
            fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 1 - (1 * fadetimer / FadeTime));

        }
        if(fadetimer > FadeTime)
        {
            completed = true;
        }
    }
}
