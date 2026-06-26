using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
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
    bool AttackIn = false;
    SavePoint savepointObject;
    PlayerMove move; // used for ground check
    List<int> Priorities = new List<int>
    {
        //lowest
        Input.Sleep,
        Input.Move,
        Input.Attack,
        Input.Dash,
        Input.Jump,
        Input.Down


    };
    List<int> allPriorities = new List<int>
    {
        //lowest
        Input.Down,
        Input.Attack,
        Input.Dash,
        Input.Jump


    };
    bool Externals;
    List<int> inputs;
    List<bool> active;
    bool falling;
    float falltimer;
    int tick;
    int Landtick;
    int Hittick;
    public GameObject landingcloud;
    public Vector3 LandingCloudOffset;
    public bool ovveride;
    bool dying;
    public bool deathanimcompleted;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void Reload()
    {
        if (!dying)
        {
            dying = true;
            animator.SetInteger("Action", -1);
            ovveride = true;
            Controller.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        }
        
    }

    public void StopDoubleJump()
    {
        animator.SetInteger("Action", 0);
    }
    public void StopDash()
    {
        animator.SetInteger("Action", 0);
    }
    public void StopFalling()
    {
        animator.SetBool("Falling", false);
    }

    public void StopAttack()
    {
        animator.SetInteger("Attack", 0);
    }
    void Start()
    {
        animator.SetBool("Attack", false);
        savepointObject = new SavePoint();
        move = Controller.GetComponent<PlayerMove>();
        parser = inputParser.GetComponent<InputParser>();
    }
    // Update is called once per frame
    void Update()
    {
        if (dying && deathanimcompleted)
        {
            savepointObject.RecoverSave();
        }
        if (!ovveride)
        {
            float velocity = Controller.GetComponent<Rigidbody2D>().linearVelocity.x;
            isLeft = Controller.GetComponent<PlayerMove>().isLeft;
            tick++;
            if (falling) falltimer += Time.deltaTime;
            Externals = false;
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
            //if (inputs.IndexOf(Input.Move) != -1 && !active[inputs.IndexOf(Input.Move)]) {
            //    animator.SetBool("Idle", true);
            //    animator.SetInteger("Action", 0);
            //    //Debug.Log("stop");

            //}
            if (!move.GroundCheck())
            {
                animator.SetBool("Falling", true);
                falling = true;
            }

            for (int i = 0; i < inputs.Count; i++)
            {
                //Debug.Log("inputs" + i);
                if (active[i])
                {
                    input = inputs[i];
                    int bufferindex = 0;
                    bufferindex = Priorities.IndexOf(input);
                    if (bufferindex == -1)
                    {
                        Externals = true;
                        bufferindex = allPriorities.IndexOf(input);
                    }
                    switch (input)
                    {
                        case Input.Left: isLeft = true; break;
                        case Input.Right: isLeft = false; break;
                    }
                    if (bufferindex > selectedIndex)
                    {
                        selectedIndex = bufferindex;
                    }


                }
            }


            //Debug.Log(selectedIndex);
            if (falling && falltimer >= 0.3)
            {
                animator.SetBool("FallInterp", true);
            }
            switch (Priorities[selectedIndex])
            {
                case Input.Sleep:
                    if (move.GroundCheck())
                    {
                        if (falling)
                        {
                            animator.SetBool("Falling", true);
                            animator.SetBool("Grounded", true);
                            landingcloud.transform.position = move.transform.position + LandingCloudOffset;
                            //Debug.Log("Groundcontact");
                            Landtick = tick;

                        }
                        else
                        {

                            if (velocity <= 0.2)
                            {
                                animator.SetInteger("Action", 0);
                                break;
                            }
                            //Debug.Log("sliding");
                            animator.SetInteger("Action", 4);

                        }

                    }
                    else if (!falling)
                    {
                        //Debug.Log("falling");
                        animator.SetBool("Falling", true);
                        falling = true;
                    }
                    break;
                case Input.Move: /* any additional checks*/
                    if (move.GroundCheck())
                    {
                        if (falling)
                        {
                            animator.SetBool("Grounded", true);
                            animator.SetBool("Falling", true);
                            landingcloud.transform.position = move.transform.position + LandingCloudOffset;
                            //Debug.Log("Groundcontact");
                            Landtick = tick;
                        }
                        animator.SetInteger("Action", 1);
                    }
                    else if (!falling)
                    {
                        Debug.Log("falling");
                        animator.SetBool("Falling", true);
                        falling = true;
                    }
                    break;
                case Input.Attack: /* any additional checks*/

                    AttackIn = !AttackIn;
                    if (AttackIn)
                    {
                        animator.SetInteger("Attack", 1);
                    }
                    else
                    {
                        animator.SetInteger("Attack", 2);
                    }
                    Hittick = tick;
                    break;
                //case Input.Dash: /* any additional checks*/ AttackIn = !AttackIn; animator.SetBool("AttackIn", AttackIn); animator.SetBool("Attack", true);  break;
                case Input.Jump: /* any additional checks*/
                    if (move.GroundCheck())
                    {
                        
                        if(animator.GetInteger("Action") != 2 && parser.timeHeld[inputs.IndexOf(Input.Jump)] < 0.2f)
                        {
                            //Debug.Log(parser.timeHeld[inputs.IndexOf(Input.Jump)]);
                            animator.SetInteger("Action", 2);
                        }
                        else
                        {
                            if (inputs.Contains(Input.Move))
                            {
                                if (active[inputs.IndexOf(Input.Move)])
                                {
                                    animator.SetInteger("Action", 1);
                                }
                                else
                                {
                                    animator.SetInteger("Action", 0);
                                }
                            }
                        }
                        //Debug.Log("jump");
                        
                    }
                    break;
                default: animator.SetInteger("Action", 0); break;
            }
            //if (!falling && animator.GetBool("GroundContact") && tick > (Landtick + 40))
            //{
            //    animator.SetBool("GroundContact", false);

            //}
            if (move.GroundCheck())
            {
                if (falling)
                    falltimer = 0;
                falling = false;
                animator.SetBool("FallInterp", false);
                animator.SetBool("Falling", false);
                animator.SetBool("Grounded", true);

            }
            else
            {
                animator.SetBool("Grounded", false);

            }

            //check for falling 
        }
        else
        {
            //Debug.Log("override");
            animator.SetBool("Falling", false);
            animator.SetBool("FallInterp", false);
            animator.SetBool("Grounded", true);
            animator.SetInteger("Action", 0 ) ;
            falling = false;
        }
    }
}
