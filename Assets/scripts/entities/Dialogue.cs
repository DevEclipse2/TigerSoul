using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class Dialogue : MonoBehaviour
{
    Collider2D collider;
    public string[] dialogue;
    public string[] dialogue1;
    public string[] dialogue2;
    public string[] repeating;
    string[] currentbranch;
    public float characterRate; //amount of characters to spew per second
    int currentline;
    int currentchar;
    int currentbranchid;
    bool advance;
    float chartimer;
    int count;
    bool inrange;
    bool spew;
    string buffer;
    float timer;
    int stringlength;
    [SerializeField] private TMP_Text textMeshPro;
    bool exit;

    public void UpdateTMP(char character)
    {
        if (textMeshPro != null)
        {
            textMeshPro.text = buffer + character; // Update the text
        }
    }
    public void ScrubTMP(string val)
    {
        if(textMeshPro != null)
        {
            textMeshPro.text = val;
        }
    }
    public void ClearTMP()
    {
        if (textMeshPro != null)
        {
            textMeshPro.text = " "; // Update the text
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collider = GetComponent<Collider2D>();
        chartimer = 1/characterRate;
        currentbranch = dialogue;
    }
    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inrange = true;
        }
    }
    void skipDiag()
    {

    }
    void OnTriggerExit2D(UnityEngine.Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inrange = false;
        }
    }
    
    public void OnInteract(InputValue value)
    {
        if(exit)
        {
            exit = false;
            spew = true;
        }
        
        if (inrange) {
            if (advance)
            {
                //this switches to the next line
                currentline++;
                curretchar = 0;
                buffer = " ";
                ClearTMP();
                if(currentline == currentbranch.Length){
                    currentbranchid++;
                    currentline = 0;
                    switch(currentbranchid)
                    {
                        case 0:
                            currentbranch = dialogue;
                        break;
                        case 1:
                            currentbranch = dialogue1;
                        break;
                        case 2:
                            currentbranch = dialogue2;
                        break;
                        default :
                            currentbranch = repeating;
                        break;
                    }
                    exit = true;
                    return;
                }
            }
            
            else
            {
                // this makes the entire thing show up
                advance = true;
                spew = false;
                if(buffer.Length < currentbranch[currentline]){
                    buffer = currentbranch[currentline];
                    ScrubTMP(buffer);
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (spew)
        {
            timer += Time.deltaTime;
            if (timer > chartimer && currentline <= (currentbranch.Length - 1))
            {
                timer = 0;
                UpdateTMP(currentbranch[currentline][currentchar]);
                currentchar++;
            }
            if(currentchar == currentbranch[currentline].Length)
            {
                spew = false;
                advance = true;
            }
        }
    }
}
