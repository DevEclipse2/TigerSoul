using Unity.Mathematics.Geometry;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHealth : MonoBehaviour
{
    public PlayerHealth PlayerHealth;
    public Image Uivignette;
    public Image DigitsImg;
    public Image TensImg;
    public Image HundredsImg;
    public Sprite[] numbers;
    public Color sharpcol;
    public Color sharpcolNum;
    public Color fadecol;
    public Color fadecolNum;
    public Color BrightNum;
    int health = 0;
    bool invuln;
    bool vignette;
    float timer;
    int healthtrans;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        Uivignette.color = fadecol;
        DigitsImg.color = fadecolNum;
        TensImg.color = fadecolNum;
        HundredsImg.color = fadecolNum;
        health = PlayerHealth.health;
        updatehp(health);
        
    }
    private void updatehp(int hp)
    {
        int hundreds = hp / 100; // Integer division
        HundredsImg.sprite = numbers[hundreds];
        int tens = (hp / 10) % 10; // Integer division followed by modulo
        TensImg.sprite = numbers[tens];
        int units = hp % 10; // Modulo to get the last digit
        DigitsImg.sprite = numbers[units];


    }
    private IEnumerator IncrementDecrementValue(float a, float b, float duration, bool heal)
    {
        float elapsedTime = 0f; // Track time elapsed

        // Determine the step size for the increment/decrement
        float initialValue = a;
        float targetValue = b;

        // Loop until the elapsed time reaches the duration
        while (elapsedTime < duration)
        {
            // Calculate how far into the duration we are (from 0 to 1)
            float t = elapsedTime / duration;

            // Linearly interpolate between the two values
            float currentValue = Mathf.Lerp(initialValue, targetValue, t);
            if (heal) 
            {
                DigitsImg.color = Color.Lerp(BrightNum, fadecolNum, t);
                TensImg.color = Color.Lerp(BrightNum, fadecolNum, t);
                HundredsImg.color = Color.Lerp(BrightNum, fadecolNum, t);
            
            }

            
            // Output the current value (or use it as needed)
            healthtrans = Mathf.FloorToInt(currentValue);

            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            // Wait until the next frame
            yield return null;
        }

        // Ensure the final value is set
        healthtrans = health;

    }
    // Update is called once per frame
    void Update()
    {
        updatehp(healthtrans);
        if (health != PlayerHealth.health)
        {
            if (health < PlayerHealth.health)
            {

            }

            StartCoroutine(IncrementDecrementValue(health, PlayerHealth.health, 1.2f, health < PlayerHealth.health));
            health = PlayerHealth.health;

        }
        if ( !vignette && PlayerHealth.invulnerable)
        {
            vignette = true;
            Debug.Log("damage");
            //recently took damage

        }
        if (vignette)
        {
            timer += Time.deltaTime;
            float percentage = (float)timer / (float)PlayerHealth.iFrames;
            if (percentage > 1 && !PlayerHealth.invulnerable)
            {
                vignette=false;
                timer = 0;
                percentage = 1;
            }
            Uivignette.color =  Color.Lerp(sharpcol, fadecol, percentage);
            DigitsImg.color =  Color.Lerp(sharpcolNum, fadecolNum, percentage);
            TensImg.color =  Color.Lerp(sharpcolNum, fadecolNum, percentage);
            HundredsImg.color =  Color.Lerp(sharpcolNum, fadecolNum, percentage);

        }
    }
}
