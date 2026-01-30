using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Windows;

public class SimpleAnimation : MonoBehaviour
{
    bool isLeft;
    public GameObject Sprite;
    public GameObject inputParser;
    InputParser parser;
    public GameObject SpriteRoot;
    public Animator animator;
    int input;
    int selectedIndex;
    public GameObject Controller;
    PlayerMove move; // used for ground check
    List<int> Priorities = new List<int>
    {
        //lowest
        Input.Sleep,
        Input.Move,
        Input.Attack,
        Input.Dash,
        Input.Jump


    };
    List<int> inputs;
    List<bool> active;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isLeft)
        {
            Sprite.transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            Sprite.transform.localScale = new Vector2(1, 1);
        }

        input = Input.Sleep;
       
        //regular animation loop
        //copies recent inputs, sorts them by currently active, then uses ranking
        inputs = parser.recentInput;
        active = parser.ongoing;
        selectedIndex = Priorities.IndexOf(Input.Sleep);
        for (int i = 0; i < inputs.Count; i++)
        {
            Debug.Log("inputs" + i);
            if (active[i])
            {
                int input = inputs[i];
                int bufferindex = 0; 
                bufferindex = Priorities.IndexOf(input);
                if (bufferindex == -1) {
                
                
                }
                switch (input)
                {
                    case Input.Left:    isLeft = true   ; break;
                    case Input.Right:   isLeft = false  ; break;
                }
                if (bufferindex > selectedIndex)
                {
                    selectedIndex = bufferindex;
                }


            }
        }
        switch (Priorities[selectedIndex]) {
            case Input.Sleep: animator.SetBool("Walk", false); break;
            case Input.Move: /* any additional checks*/ animator.SetBool("Walk", true); break;
            case Input.Attack: /* any additional checks*/ animator.SetBool("Walk", true); break;


        }
        //check for falling 
    }
}
