using UnityEngine;

public class DataMuleCatacombs: MonoBehaviour
{
    public bool pulledleverRite;
    public GameObject Ritelever;
    public GameObject[] RiteGates;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Ritelever != null)
        {
            if (Catacombs.PulledLeverRite)
            {
                Ritelever.GetComponent<PullLever>().AlreadyPulled();
                foreach(GameObject gO in RiteGates)
                {
                    gO.GetComponent<PullLever>().AlreadyPulled();
                }
            }
        }
    }
    private void Update()
    {
        if(Ritelever != null)
        {
            if(Ritelever.GetComponent<PullLever>().pulled) 
            {
                pulledleverRite = true;
                Catacombs.PulledLeverRite = true;
                if (!RiteGates[0].GetComponent<PullLever>().pulled)
                {
                    foreach (GameObject gO in RiteGates)
                    {
                        gO.GetComponent<PullLever>().InvokedPullLever();
                    }
                }
            }
        }
    }
}
