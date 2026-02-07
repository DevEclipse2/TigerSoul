using UnityEngine;

public class Spawnenemy : MonoBehaviour
{
    public GameObject spawnObject;
    float spawntimer;
    int spawncount = 22;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawntimer += Time.deltaTime;
        if (spawntimer > 0.3f) {
            GameObject spawnobj = Instantiate(spawnObject);
            spawnobj.transform.position = this.transform.position + new Vector3(Random.Range(-3.0f,3.0f) , Random.Range(-1.0f,2.0f));
            spawnobj.transform.localScale = new Vector3(spawnobj.transform.localScale.x * -1, spawnobj.transform.localScale.y, spawnobj.transform.localScale.z);
            spawnobj.GetComponent<FollowPath>().patrolspeed = Random.Range(9f, 10f);
            spawntimer = 0;
            spawncount--;
        }
        
        if (spawncount == 0) {

            Destroy(this.gameObject);
        }
        
    }
}
