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
    public float characterRate; //amount of characters to spew per second
    int currentline;
    int currentchar;
    int currentbranch;
    bool advance;
    float chartimer;
    int count;
    bool inrange;
    bool spew;
    string buffer;
    float timer;
    int stringlength;
    [SerializeField] private TMP_Text textMeshPro;

    public void UpdateTMP(char character)
    {
        if (textMeshPro != null)
        {
            textMeshPro.text = buffer + character; // Update the text
        }
    }
    public void ClearTMP(char character)
    {
        if (textMeshPro != null)
        {
            textMeshPro.text = buffer + character; // Update the text
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collider = GetComponent<Collider2D>();
        chartimer = 1/characterRate;
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
        //}
        if (inrange) {
            if (advance)
            {

            }
            else
            {

            }
            
        
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (spew)
        {
            timer += Time.deltaTime;
            if (timer > chartimer)
            {
                currentchar++;
                timer = 0;
                UpdateTMP( 's');
            }

        }
    }
}
