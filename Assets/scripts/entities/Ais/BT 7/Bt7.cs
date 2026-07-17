using UnityEngine;

public class Bt7 : BaseEnemy
{
    [SerializeField]
    GameObject locationChecker;
    [SerializeField]
    Transform Player;
    [SerializeField]
    GameObject HomePoint;
    [SerializeField]
    GameObject NavArea;
    Vector2 MoveTarget;

    Animator animator;

    enum States
    {
        idle,
        wander,
        alert,
        chasing,
        attack,
        retreat,
        returnHome,
    };
    private States currentState = States.idle;

    /// <summary>
    /// behaviour of the bt7
    /// the bt7 wanders an area until an unwitting player enters
    /// the bt7 will then attempt to give chase, and  start circling the player
    /// the regular bt7 variant circles tighter and tighter until it hits you, then retreats
    /// the 76 bt will do rapid thrusting attacks that knock you off balance
    /// if not chasing the player, the bt7 will return to it's home point and chill out 
    /// 
    /// other variants of this will run at and through you directly
    /// the ground variant baits the player with fakeout attacks
    /// </summary>

    void Start()
    {
        
    }

    void CheckPoint(Vector2 targetPoint)
    {
        //overlaps and checks if its in nav and also not in ground
    }

    void Pursue()
    {

    }

    void Circle()
    {

    }
    void Attack()
    {
    }
    void Statemachine()
    {
        switch (currentState)
        {
            case States.idle:
                return;
            case States.wander:
                return;
            case States.alert:
                return;
            case States.chasing:
                return;
            case States.attack: return;
            case States.retreat: return;
            default:
                return;
        }
    }
    void Update()
    {
        if(EnteredView)
        {

        }
        Statemachine();
    }
}
