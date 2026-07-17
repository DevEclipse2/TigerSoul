using System;
using System.Reflection;
using UnityEngine;

public class AreaAlert : MonoBehaviour
{
    //area alert is a script that triggers something or other to do a purpose
    public string PropName;
    public GameObject Alert;
    public GameObject[] AlertList;
    [SerializeField]
    bool fireEnter;
    [SerializeField]
    bool fireExit;
    [SerializeField]
    bool EnterValue;
    [SerializeField]
    bool ExitValue;

    [SerializeField]
    string Tag;

    bool useList;
    private void Start()
    {
        if(Alert == null && AlertList.Length > 0)
        {
            useList = true;
        }
        else if (AlertList.Length == 0 && Alert!= null )
        {
            useList = false;
        }
        else
        {
            Debug.LogError("both alertList and alert gameobject are empty!");
        }
    }

    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {

        if (collision.CompareTag(tag)&&fireEnter)
        {
            Type baseEnemy_T = typeof(BaseEnemy);
            PropertyInfo prop = baseEnemy_T.GetProperty(PropName, BindingFlags.Public);
            if(useList)
            {
                foreach(GameObject gameobject in AlertList)
                {
                    object obj = gameobject.GetComponent<BaseEnemy>();
                    prop.SetValue(obj, EnterValue);
                }
            }
            else
            {
                object obj = Alert.GetComponent<BaseEnemy>();
                prop.SetValue(obj, EnterValue);
            }
                
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(tag) && fireExit)
        {
            Type baseEnemy_T = typeof(BaseEnemy);
            PropertyInfo prop = baseEnemy_T.GetProperty(PropName, BindingFlags.Public);
            if (useList)
            {
                foreach (GameObject gameobject in AlertList)
                {
                    object obj = gameobject.GetComponent<BaseEnemy>();
                    prop.SetValue(obj, ExitValue);
                }
            }
            else
            {
                object obj = Alert.GetComponent<BaseEnemy>();
                prop.SetValue(obj, ExitValue);
            }


        }
    }

}
