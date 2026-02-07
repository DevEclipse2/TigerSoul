using UnityEngine;
using System.Collections.Generic;
public class Flash : MonoBehaviour
{
    public SpriteRenderer[] renderer;
    public bool flash;
    public float flashtime;
    public float flashrate;
    float pulsetime;
    float timer;
    float intervaltimer;
    public Color flashcolor = Color.red;
    public Color defaultcolor = Color.white;
    bool flashcol;
    bool flashing;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pulsetime = 1/flashrate;
        if(renderer == null)
        {
            renderer     = GetComponentsInChildren<SpriteRenderer>();
        }    
    }
    public void Damageflash( float flashtime)
    {
        if (timer <= 0)
        {
            timer = flashtime;
        intervaltimer = 0;
        //Debug.Log(pulsetime);
        }
        
    }
    // Update is called once per frame
    void Update()
    {

        intervaltimer += Time.deltaTime;
        timer -= Time.deltaTime;
        if (timer > 0) {
            flash = true;
        }
        else
        {
            flash = false;
        }
        if (flash)
        {
            if (intervaltimer > pulsetime)
            {
                intervaltimer = 0;
                flashcol = !flashcol;
                //Debug.Log( flashcol);
                Color targetcolor = defaultcolor;
                if (flashcol)
                {
                    targetcolor = flashcolor;
                }
                foreach (SpriteRenderer component in renderer)
                {
                    component.color = targetcolor;
                }
            }
        }
        if (!flash) {
            foreach (SpriteRenderer component in renderer)
            {
                component.color = defaultcolor;
            }
            flashing = false;
            timer = 0;
            intervaltimer = 0;
        }
    }
}
