using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
public class Combat : MonoBehaviour
{
    //public List<int> inputType = new List<int>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool grounded = false;
    public AttackContainer[] tapAttacks;
    public AttackContainer[] holdAttacks;
    public GameObject tap;
    public GameObject hold;
    AttackContainer buffercontainer;
    public GameObject InputController;
    InputParser parser;
    public int CurrentAttack;
    public List<int> inputFilter = new List<int> {
      Input.Attack,
    };
    void Start()
    {
        parser = InputController.GetComponent<InputParser>();
        tapAttacks = tap.GetComponentsInChildren<AttackContainer>();
        holdAttacks = hold.GetComponentsInChildren<AttackContainer>();
        //searches for all 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onAttack(InputAction.CallbackContext context)
    {
        buffercontainer = null;
        Debug.Log("attack");
        //clear buffer first;
        //attack button pressed
        if (context.started)
        {
            int button = parser.QueryInput(inputFilter);
            switch (button) {
                case -1:
                    //error
                    break;
                    case Input.Sleep:
                    // default attack
                    CurrentAttack = 0;

                    break;
                    case 1:
                    break ;
                    default:
                    CurrentAttack = 0;
                    break;

            }
            // this one begins listening for any input
        }
        if (context.canceled && context.duration <= 0.2)
        {
            //tap attack
            //find most current input (with filtering)
            //use generic attack as fallback if inputs conflict
            buffercontainer = tapAttacks[CurrentAttack];
        }
        else if(!context.started && context.duration > 0.2 && !context.canceled)
        {
            int button = parser.QueryInput(inputFilter);
            if (button >= 0)
            {

            }
            buffercontainer = holdAttacks[CurrentAttack];
            // hold attack
        }
        if (buffercontainer != null)
        {
            buffercontainer.DealDamage();
        }
    }
}
