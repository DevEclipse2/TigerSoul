using NUnit.Framework.Constraints;
using System.Collections;
using UnityEngine;

public class baseUpgrade : MonoBehaviour
{
    [SerializeField]
    public GameObject playerController;
    protected PlayerMove movementscript;
    public bool Active;
    public bool Available;
    [SerializeField]
    protected float AbilityCooldown = 0.1f;
    private Coroutine cooldownCoroutine;
    protected virtual void Start()
    {
        movementscript = playerController.GetComponent<PlayerMove>();
    }
    public virtual void activate()
    {

    }
    public virtual void useAbility()
    {

    }
    public virtual void deactivate()
    {

    }
    public virtual void cooldown()
    {
        if(cooldownCoroutine == null) 
        {
            cooldownCoroutine = StartCoroutine(doCD()); 
        }
    }
    public IEnumerator doCD()
    {
       
        if (Available || !Active) { yield return null; }
        yield return new WaitForSeconds(AbilityCooldown);
        Available = true;
        cooldownCoroutine = null;
        yield return null;
    }
    public virtual void init()
    {

    }

}
