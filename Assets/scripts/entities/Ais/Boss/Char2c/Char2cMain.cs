using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine.Rendering.Universal;

public class Char2cMain : MonoBehaviour
{
    [Header("basic components")]
    Health bosshealth;
    public int[] PhaseHp;
    public int[] PhaseAp;
    int CurrentPhase = -1;
    public bool BossDefeated;
    public GameObject PlayerInput;
    float delta;
    AudioSource bossmusic;
    public GameObject doorstopper;
    public GameObject Cloud1;
    public GameObject Cloud2;
    bool CleanExit;
    bool exiting;
    [Header("Enter Fight")]

    public GameObject EnterFightTrigger;
    public bool Fightbegin;
    public GameObject Intro;
    AnimFinishRelay IntroRelay;
    float originalmovespeed;
    public GameObject LeaveChange;

    [Header("Phase 0")]
    public GameObject Phase0Object;
    public GameObject Phase0ForegroundObject;
    Animator Phase0Animator;
    AnimFinishRelay Phase0AnimFinishRelay;
    AnimFinishRelay Phase0AnimTrigRelay;
    public float Phase0TrampleCd;
    public float Phase0NoneCd;
    public float Phase0WormCd;
    private float Phase0selectedCd;
    public float Phase0Attacktimer;
    Vector2 Phase0OriginalPos;
    int Phase0NextAttack;
    bool left;
    public Collider2D Phase0FreeRange;
    public Transform[] SpawnLocation;
    public GameObject SpawnableTacham;
    public GameObject SpawnableVickers;
    bool timerPhase0;
    GameObject Exittacam;

    [Header("Phase 1")]
    public GameObject Phase1Object;
    Animator Phase1Animator;

    // 0 nothign
    //1 worm
    //2 trample
    // -1 none queued

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bosshealth = GetComponent<Health>();
        IntroRelay = Intro.GetComponent<AnimFinishRelay>();
        bossmusic = GetComponent<AudioSource>();
        Phase0AnimFinishRelay = Phase0Object.GetComponent<AnimFinishRelay>();
        Phase0AnimTrigRelay = Phase0Object.GetComponentInChildren<AnimFinishRelay>();
        Phase0Animator = Phase0Object.GetComponent<Animator>();
        Phase0OriginalPos = Phase0Object.GetComponent<Transform>().position;
        Phase0NextAttack = 1;
        Phase0Attacktimer = Phase0WormCd - 6;
        Phase0selectedCd = Phase0WormCd;
        bosshealth = Phase0ForegroundObject.GetComponent<Health>();
    }
    
    private IEnumerator Phase0Timer( float time)
    {
        timerPhase0 = true;
        yield return new WaitForSeconds(time);
        timerPhase0 = false;
        yield return null;
    }
    
    void Phase0()
    {
        if(exiting)
        {
            if (Phase0Animator.GetCurrentAnimatorStateInfo(0).IsName("End Phase") && Phase0AnimTrigRelay.Finished)
            {
                Phase0ForegroundObject.GetComponent<PolygonCollider2D>().enabled = false;
                Debug.Log("tacam");
                if (!timerPhase0)
                {
                    Instantiate(Phase0Object.transform);
                    GameObject tacam = Instantiate(SpawnableTacham);
                    tacam.transform.position = SpawnLocation[0].position;
                    StartCoroutine(Phase0Timer(12f));
                    Exittacam = tacam;
                }

            }
            return;
        }
        Sprite sprite = Phase0ForegroundObject.GetComponent<SpriteRenderer>().sprite;
        if (sprite != null) {
            Phase0ForegroundObject.GetComponent<PolygonCollider2D>().enabled = true;
             var pointsList = new List<Vector2>();
            sprite.GetPhysicsShape(0 , pointsList);
            Phase0ForegroundObject.GetComponent<PolygonCollider2D>().points = pointsList.ToArray();
        }
        else
        {
            Phase0ForegroundObject.GetComponent<PolygonCollider2D>().enabled = false;
        }
        Phase0Attacktimer += delta;
        if (left)
        {
            Phase0Object.transform.localScale = new Vector2( Mathf.Abs(Phase0Object.transform.localScale.x) * -1 , Phase0Object.transform.localScale.y);
        }
        else
        {
            Phase0Object.transform.localScale = new Vector2(Mathf.Abs(Phase0Object.transform.localScale.x), Phase0Object.transform.localScale.y);
        }
        Phase0Object.transform.position = Phase0OriginalPos;
        if (Phase0AnimTrigRelay.Finished) 
        {
            if(Phase0NextAttack == 2)
            {
                Vector2 targetPos = new Vector2(PlayerInput.transform.position.x, Phase0Object.transform.position.y);
                left = PlayerInput.GetComponent<Rigidbody2D>().linearVelocityX < 0;
                if (Phase0FreeRange.OverlapPoint(targetPos))
                {
                    Phase0Object.transform.position = targetPos;
                }
                else
                {
                    Phase0Object.transform.position = new Vector2 (Phase0FreeRange.ClosestPoint(targetPos).x , targetPos.y);

                }
            }
            else if (Phase0Animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                if (!timerPhase0)
                {
                    GameObject vickers = Instantiate(SpawnableVickers);
                    vickers.transform.position = SpawnLocation[Random.Range(0, SpawnLocation.Length)].position;
                    //vickers.transform.rotation = new Vector3(-0.5f, 0.5f, 1);
                    StartCoroutine(Phase0Timer(1));
                }
                
            }
            
        }

        if (Phase0AnimFinishRelay.Finished)
        {
            Debug.Log("finished" + Phase0Animator.GetInteger("Attack"));
            if(Phase0NextAttack == 2 || Phase0Animator.GetInteger("Attack") == 4 || Phase0Animator.GetInteger("Attack") == 3)
            {
                Phase0AnimFinishRelay.Finished = false;
                //stuff
                int followup = Random.Range(0, 11);
                // 50% chance : exit
                // 20% chance : spin
                // 30% chance : secondary slam
                // 10% chance : 
                if(followup <= 5)
                {
                    Phase0NextAttack = -1;
                    Phase0Animator.SetBool("Run", false);
                    Phase0Animator.SetInteger("Attack", 5);

                }
                else if (followup <= 7)
                {
                    Phase0Animator.SetInteger("Attack", 3);
                }
                else
                {
                    Phase0Animator.SetInteger("Attack", 4);
                    if (Phase0selectedCd != 1)
                    {
                        left = PlayerInput.transform.position.x < Phase0Object.transform.position.x;
                    }
                }

            }
            else
            {
                Debug.Log("Finished Attack");
                Phase0Animator.SetBool("Run", false);
                Phase0AnimFinishRelay.Finished = false;
                Phase0NextAttack = -1;
            }
            
        }

        if (Phase0NextAttack == -1)
        {
            Phase0NextAttack = Random.Range(0,3);
            Debug.Log("Next attack " + Phase0NextAttack);
            switch (Phase0NextAttack) {
                case 0:
                    Phase0selectedCd = Phase0NoneCd;
                    
                break;
                case 1:
                    Phase0selectedCd = Phase0WormCd;
                    if(Phase0Animator.GetInteger("Attack") != 4)
                    {
                        left = !left;
                    }
                    break;
                case 2:
                    Phase0selectedCd = Phase0TrampleCd;
                break;
                default:

                break;
            }
        }
        if (Phase0Attacktimer > Phase0selectedCd) 
        { 
            Phase0Animator.SetInteger("Attack", Phase0NextAttack);
            Phase0Animator.SetBool("Run", true);
            Phase0Attacktimer = 0;
            

        }
    }
    private  IEnumerator DisablePhase0()
    {
        StopCoroutine(Phase0Timer(1));
        Phase0Animator.SetInteger("Attack", 6);
        Phase0AnimFinishRelay.Finished = false;
        Phase0selectedCd = 9000;
        Phase0NextAttack = -3;
        yield return new WaitForSeconds(5.8f);

        while (!Phase0AnimFinishRelay.Finished && Phase0Animator.GetCurrentAnimatorStateInfo(0).IsName("End Phase")) {
            yield return new WaitForSeconds(0.1f);
            Debug.Log("wait");

        }
        Debug.Log("exit");

        Debug.Log(Phase0AnimFinishRelay.Finished);
        Phase0Object.SetActive(false);
        Destroy(doorstopper);
        CurrentPhase++;
        CleanExit = true;
        exiting = false;
        yield return null ;
    }

    void Phase1()
    {

    }

    void BossPhase()
    {
        if (bosshealth != null) { 
            if(bosshealth.health <=0 || CurrentPhase == -1)
            {
                if (!exiting)
                {
                    switch (CurrentPhase)
                    {
                        case 0:
                            exiting = true;
                            StartCoroutine( DisablePhase0());
                            //switch bosshealth
                            //destroy

                        break;
                    }

                    if (CurrentPhase == -1)
                    {

                        CurrentPhase++;
                        CleanExit = true;
                    }
                    if (CurrentPhase >= PhaseHp.Length) {
                        BossDefeated = true;
                        return;
                    }
                    if (CleanExit)
                    {
                        bosshealth.health = PhaseHp[CurrentPhase];
                        bosshealth.armour = PhaseAp[CurrentPhase];
                        CleanExit = false;
                    }
                }
                
                
            }
            switch (CurrentPhase)
            {
                case 0:
                    Phase0();
                    break;

                case 1:
                    if(Exittacam == null)
                    {

                    }
                    break;
            }

        }
    }
    void CheckIntro()
    {
        if (EnterFightTrigger.GetComponent<CollisionHandle>().IsTriggered && !Intro.GetComponent<Animator>().GetBool("Start") && !Fightbegin)
        {
            //yes
            Intro.GetComponent<Animator>().SetBool("Start", true);
            originalmovespeed = PlayerInput.GetComponent<PlayerMove>().moveSpeed;
            
            PlayerInput.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            PlayerInput.GetComponent<PlayerMove>().moveSpeed = 0f;
            Debug.Log("cantmove");
            bossmusic.Play();
            bossmusic.time = 1.8f;
        }
        if(Intro.GetComponent<Animator>().GetBool("Start"))
        {
            PlayerInput.GetComponent<Rigidbody2D>().AddForce( new Vector2(300 , 100));
        }
    }

    // Update is called once per frame
    void Update()
    {
        delta = Time.deltaTime;
        if (IntroRelay.Finished)
        {

            if (Fightbegin) 
            { 

                BossPhase();
                if (BossDefeated)
                {

                }
            }
            else
            {
                Fightbegin = true;
                Intro.GetComponent<Animator>().SetBool("Hide", true);
                LeaveChange.GetComponent<callbackTrigger>().index = 0;
                PlayerInput.GetComponent<PlayerMove>().moveSpeed = originalmovespeed;
                Destroy(Intro.GetComponent<Animator>());
                Destroy(Cloud1);
                Destroy(Cloud2);


            }
        }
        else
        {
            CheckIntro();

        }

        
    }
}
