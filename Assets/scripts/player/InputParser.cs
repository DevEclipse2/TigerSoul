using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputParser : MonoBehaviour
{
    public List<int> recentInput;
    public List<float> timeSinceInput;
    public List<float> timeHeld;
    List<float> StartTime;
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
        recent = recentInput.IndexOf(inputid);
        
        if (context.started)
        {
            if (recent != -1)
            {
                ongoing[recent] = true;
                cancelled[recent] = false;
                timeSinceInput[recent] = 0;
            }
            else { 
                //new entry
                recentInput.Add(inputid);
                timeSinceInput.Add(0);
                timeHeld.Add(0);
                ongoing.Add(true);
                cancelled.Add(false);
                pressed.Add(true);

            }
            
        }
        else if (context.canceled)
        {
            if (recent != -1)
            {
                ongoing[recent] = false;
                cancelled[recent] = true;
                timeHeld[recent] = 0;
                pressed[recent] = false;
            }
            else
            {
                recentInput.Add(inputid);
                timeSinceInput.Add(0);
                timeHeld.Add(0);
                ongoing.Add(true);
                cancelled.Add(true);
                pressed.Add(false);
            }
        }
        else if (context.duration > 0.05) {
            Debug.Log("being held");
            if (recent != -1)
            {
                ongoing[recent] = true;
                pressed[recent] = false;
                timeHeld[recent]= (float)context.duration;
                timeSinceInput[recent] = 0;

            }
            else
            {
                recentInput.Add(inputid);
                timeSinceInput.Add(0);
                timeHeld.Add(0);
                ongoing.Add(true);
                cancelled.Add(false);
                pressed.Add(false);
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
    public void onJump(InputAction.CallbackContext context)
    {
        //Debug.Log("jump");
        KeepAlive(Input.Jump, context);
    }
    public void onMove(InputAction.CallbackContext context)
    {
        //Debug.Log("move");
        Vector2 val = context.ReadValue<Vector2>();
        KeepAlive (Input.Move, context);
        if (val.x > 0)
        {
            KeepAlive(Input.Right, context);
        }
        else if (val.x < 0)
        {
            KeepAlive(Input.Left, context);

        }
        else 
        {
            KeepAlive(Input.Sleep, context);
            KeepAlive(Input.Left, context);
            KeepAlive(Input.Right, context);

        }
    }
    public void onAttack(InputAction.CallbackContext context)
    {
        Debug.Log("smack");
        KeepAlive(Input.Attack, context);
    }
    public void onInteract(InputAction.CallbackContext context)
    {
        //Debug.Log("talk");
        KeepAlive(Input.Interact, context);
    }
    public void onDash(InputAction.CallbackContext context)
    {
        //Debug.Log("zoom");
        KeepAlive(Input.Dash, context);
    }
    public void onCrouch(InputAction.CallbackContext context)
    {
        //Debug.Log("zoom");
        KeepAlive(Input.Down, context);
    }
    public int QueryInput( List<int> blacklist)
    {   
        if(blacklist == null)
        {
            if(recentInput.Count > 0) return recentInput[recentInput.Count - 1];
            return -2;
        }
        for(int i = recentInput.Count - 1; i >= 0; i--)
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
            if (!ongoing[i])
            {
                timeSinceInput[i] += Time.deltaTime;
            }
        }
        if (recentInput.Count > retainCount) {

            clearInput();
        }
    }
}
