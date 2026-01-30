using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationController : MonoBehaviour
{
    public GameObject inputParser;
    public GameObject Sprite;
    InputParser parser;
    public bool isLeft = false;
    public GameObject SpriteRoot;
    public Animator animator;
    bool overrideAnim = false; //this one is triggered by specific stuff
    TriggerAnim currentAnim;
    public GameObject triggered;
    public GameObject tree;

    TriggerAnim[] triggeredAnimations;
    TriggerAnim[] triggeredArranged;
    TriggerAnim[] AnimTree;
    TriggerAnim[] AnimTreeArranged;
    
    public List<string> orderTriggered;
    public List<string> orderTree;

    public bool[] playingtree;
    public bool[] playingtrig;
    int input;
    List<int> inputs;
    List<bool> active;
    int selectedIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        parser = inputParser.GetComponent<InputParser>();
        triggeredAnimations = triggered.GetComponents<TriggerAnim>();
        AnimTree = tree.GetComponents<TriggerAnim>();

        var objectDictionary = triggeredAnimations.ToDictionary(o => o.name);

        // Create a new array based on the order list
        triggeredArranged = new TriggerAnim[orderTriggered.Count];

        for (int i = 0; i < orderTriggered.Count; i++)
        {
            // Find the object corresponding to the name in the order list
            if (objectDictionary.TryGetValue(orderTriggered[i], out TriggerAnim obj))
            {
                triggeredArranged[i] = obj;
            }
        }


        objectDictionary = AnimTree.ToDictionary(o => o.name);

        // Create a new array based on the order list
        AnimTreeArranged = new TriggerAnim[orderTree.Count];

        for (int i = 0; i < orderTree.Count; i++)
        {
            // Find the object corresponding to the name in the order list
            if (objectDictionary.TryGetValue(orderTree[i], out TriggerAnim obj))
            {
                AnimTreeArranged[i] = obj;
            }
        }
        playingtree = new bool[orderTree.Count];
        playingtrig = new bool[orderTriggered.Count];

    }
    void sudoAnim(TriggerAnim anim)
    {
        // this breaks out of the regular animation tree in order to dynamically update things.
        if(anim.Scan())
        {
            anim.runAnim(animator, true);
            overrideAnim = true;
        }
    }
    void Exitflag(int index, bool tree)
    {
        if (!tree)
        {
            playingtrig[index] = false;
        }
        else
        {
            playingtree[index] = false;
        }
    }
    void tryinterrupt()
    {
        //this tries to override an animation WITHOUT breaking fron the scene tree
        //priorities : regular < interrupt <  sudoanim
        
    
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("move");
        if (context.performed || context.canceled)
        {
            isLeft = (context.ReadValue<Vector2>().x < 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isLeft)
        {
            Sprite.transform.localScale = new Vector2(-1,1);
        }
        else
        {
            Sprite.transform.localScale = new Vector2(1, 1);
        }

        input = Input.Sleep;
        if (overrideAnim)
        {
            if(currentAnim.breakOut())
            {
                overrideAnim = false;
                currentAnim.quitAnim(animator);
                currentAnim = null;
            }
        }
        else
        {
            //regular animation loop
            //copies recent inputs, sorts them by currently active, then uses ranking
            inputs = parser.recentInput;
            active = parser.ongoing;
            selectedIndex = orderTree.IndexOf("sleep");
            for (int i = 0; i < inputs.Count; i++) {
                if (active[i])
                {
                    int input = inputs[i];
                    int bufferindex = 0;
                    switch (input) { 
                        case Input.Sleep    : bufferindex = orderTree.IndexOf("sleep");                 break;
                        case Input.Left     : bufferindex = orderTree.IndexOf("move"); isLeft = true;   break;
                        case Input.Right    : bufferindex = orderTree.IndexOf("move"); isLeft = false;  break;
                        case Input.Down     : bufferindex = orderTree.IndexOf("down");                  break;
                        case Input.Jump     : bufferindex = orderTree.IndexOf("jump");                  break;
                        case Input.Dash     : bufferindex = orderTree.IndexOf("dash");                  break;
                        case Input.Attack   : bufferindex = orderTree.IndexOf("attack");                break;
                        default             : bufferindex = orderTree.IndexOf("sleep");                 break;
                    }
                    if (bufferindex > selectedIndex) { 
                        selectedIndex = bufferindex;
                    }
                }
            }
            if (!playingtree[selectedIndex])
            {
                playingtree[selectedIndex] = true;
                AnimTreeArranged[selectedIndex].reply = this.gameObject;
                AnimTreeArranged[selectedIndex].index = selectedIndex;
                AnimTreeArranged[selectedIndex].tree = false;
                AnimTreeArranged[selectedIndex].runAnim(animator , false);
            }

        }
    }
}
