///
/// movement options include
/// walljumping
/// doublejumping
/// dashing
/// crouch / sliding

using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class movement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool[]   isAvailable;
    public string[] moveNames;
    public Animator animator;

    [SerializeField]
    string actionIdName;

    void Start()
    {
        
    }

    public void OnMove(InputValue value)
    {
        
        if (isAvailable[Array.IndexOf(moveNames,"walking")])
        {
            //can walk
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
