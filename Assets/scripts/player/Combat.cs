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
    public float cooldown;
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
        cooldown -= Time.deltaTime;
    }
    public void Attack(InputAction.CallbackContext context)
    {
        
        //clear buffer first;
        //attack button pressed
        if (context.started && cooldown <= 0)
        {
            buffercontainer = null;
            int index = parser.QueryInput(inputFilter);
            int action;
            if (index >= 0) { action = parser.recentInput[index]; }
            else if(index == -1){ action = Input.Sleep; }
            else { action = Input.Sleep; }
                Debug.Log(action);

            switch (action)
                {
                    case -1:
                        //error
                        Debug.Log("Yo twin this is kinda bad");
                        break;
                    case Input.Sleep:
                        // default attack
                        CurrentAttack = 0;
                        buffercontainer = tapAttacks[CurrentAttack];
                        Debug.Log("basic Attack");
                        break;
                    case 1:
                        CurrentAttack = 0;
                        buffercontainer = tapAttacks[CurrentAttack];
                        Debug.Log("basic Attack");
                    break;
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

            //buffercontainer = holdAttacks[CurrentAttack];
            // hold attack
        }
        if (buffercontainer != null)
        {
            Debug.Log("Container OK");
            buffercontainer.DealDamage();
            cooldown = (buffercontainer.cutIn + buffercontainer.cutOut + buffercontainer.hold);
        }
        else
        {
            Debug.LogError("Invalid Container");
        }
    }
}
