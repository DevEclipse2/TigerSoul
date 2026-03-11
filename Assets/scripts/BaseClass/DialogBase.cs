using System;
using TMPro;
using UnityEngine;

public class DialogBase : MonoBehaviour
{
    public int dialogIndex;
    public int dialogBranch;
    public TextMeshPro textMeshPro;
    public string buffer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void UpdateTMP(char character)
    {
        if (textMeshPro != null)
        {
            buffer += character;
            textMeshPro.text = buffer; // Update the text
        }
    }
    public void ScrubTMP(string val)
    {
        if (textMeshPro != null)
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
    
    public virtual void ProgressDialog()
    {
        
    }
    public virtual void Response( int response)
    {

    }
    
}
