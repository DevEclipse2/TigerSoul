using Unity.VisualScripting;
using UnityEngine;

public class PlayerLoadPosition : MonoBehaviour
{
    public Transform[] Spawns;
    public GameObject Input;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void setPosition(int index)
    {
        Input.GetComponent<Transform>().position = new Vector3(Spawns[index].position.x , Spawns[index].position.y , Input.GetComponent<Transform>().position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
