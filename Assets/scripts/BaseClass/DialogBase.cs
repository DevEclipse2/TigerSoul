using System;
using TMPro;
using UnityEngine;

public class DialogBase : MonoBehaviour
{
    public int dialogIndex;
    public int currentChar;
    public int dialogBranch;
    public TextMeshPro textMeshPro;
    public string buffer;
    public float CharRate;
    public string[] currentBranch;
    protected bool spew;
    protected bool exit;
    protected bool advance;
    protected float chartimer = 0;
    protected float timer = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void UpdateTMP(char character)
    {
        if (textMeshPro != null)
        {
            buffer += character;
            textMeshPro.text = buffer; // Update the text
        }
    }
    public virtual void ScrubTMP(string val)
    {
        if (textMeshPro != null)
        {
            textMeshPro.text = val;
        }
    }
    public virtual void ClearTMP()
    {
        if (textMeshPro != null)
        {
            textMeshPro.text = " "; // Update the text
        }
    }
    public void Refresh()
    {
        if (spew)
        {
            //Debug.Log("spew");
            timer += Time.deltaTime;
            if (timer > chartimer && dialogIndex <= (currentBranch.Length - 1))
            {
                timer = 0;
                UpdateTMP(currentBranch[dialogIndex][currentChar]);
                currentChar++;
            }
            if (dialogIndex < currentBranch.Length)
            {
                if (currentChar == currentBranch[dialogIndex].Length)
                {
                    spew = false;
                    advance = true;
                }
            }
            else
            {
                spew = false;
                advance = true;
            }
        }
    }
    public bool TryNextLine()
    {
        if (exit)
        {
            exit = false;
            spew = true;
        }
        if (advance)
        {
            //this switches to the next line
            dialogIndex++;
            //Debug.Log(currentline);
            spew = true;
            currentChar = 0;
            buffer = " ";
            ClearTMP();
            if (dialogIndex == currentBranch.Length)
            {
                dialogIndex = -1;
                exit = true;
                return false;
            }
            advance = false;
        }
        else
        {
            // this makes the entire thing show up
            advance = true;
            Debug.Log("advance");
            spew = false;
            buffer = currentBranch[dialogIndex];
            ScrubTMP(buffer);
        }
        return true;
    }

    public virtual void ProgressDialog()
    {
        
    }
    public virtual void Response( int response)
    {

    }
    
}
