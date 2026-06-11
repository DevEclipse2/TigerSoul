///
/// movement options include
/// walljumping
/// doublejumping
/// dashing
///

using System.Collections.Generic;
using UnityEngine;

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
    // Update is called once per frame
    void Update()
    {
        
    }
}
