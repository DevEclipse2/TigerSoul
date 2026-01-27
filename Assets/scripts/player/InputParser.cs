using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputParser : MonoBehaviour
{
    public List<int> recentInput;
    public List<float> timeSinceInput;
    public List<bool> ongoing;
    public List<bool> cancelled;
    public List<bool> pressed;
    public int retainCount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        
    }
    public void clearInput()
    {
        for (int i = 0; i < recentInput.Count; i++)
        {
            if (!ongoing[i]) {
                recentInput.RemoveAt(i);
                timeSinceInput.RemoveAt(i);
                ongoing.RemoveAt(i);
                cancelled.RemoveAt(i);
                pressed.RemoveAt(i);
            }
        }
    }
    void KeepAlive( int inputid , InputAction.CallbackContext context)
    {
        int recent = 0;
        //check if its alive
        if (context.started)
        {
            //just pressed
            recentInput.Add(inputid);
            timeSinceInput.Add(0);
            ongoing.Add(true);
            cancelled.Add(false);
            pressed.Add(true);
        }
        else if (context.canceled)
        {
            if ((recent = recentInput.IndexOf(inputid)) != -1)
            {
                ongoing[recent] = false;
                cancelled[recent] = true;

            }
            else
            {
                Debug.Log("missing?! how!?");
            }
        }
        else if (context.duration > 0.05) {
            if ((recent = recentInput.IndexOf(inputid)) != -1)
            {
                ongoing[recent] = false;
                pressed[recent] = true;

            }
            else
            {
                Debug.Log("how");
            }
        }
        //else if ((recent = recentInput.IndexOf(inputid)) != -1 && context.duration > 0.05) 
        //{
        //    if (!ongoing[recent]) 
        //    {
        //        ongoing[recent] = true;
        //    }
        //}
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        KeepAlive(Input.Jump, context);
    }
    public int QueryInput( List<int> blacklist)
    {   
        for(int i = 0; i < recentInput.Count; i++)
        {
            
            if (!blacklist.Contains(recentInput[i]))
            {
                return i;
            }
        }
        //filters input and returns most current input in index 
        return -1; // no available
    }
    // Update is called once per frame

    private void Update()
    {
        for (int i = 0; i < timeSinceInput.Count; i++) {
            timeSinceInput[i] += Time.deltaTime;
        }
    }
}
