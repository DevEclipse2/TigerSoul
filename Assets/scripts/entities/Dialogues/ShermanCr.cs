using TMPro;
using UnityEngine;

public class ShermanCr : DialogBase
{
    public string[] dialog1;
    public string[] dialogLoop1;
    public string[] dialogLoop2;
    public string[] dialogLoop3;
    public string[] dialogLoop4;
    public string[] dialogLoop5;
    int lastLoop = 0;
    void Start()
    {
        chartimer = 1 / CharRate;
        currentBranch = dialog1;
    }
    public override void ProgressDialog()
    {
        if(!TryNextLine())
        {
            int nextloop = Random.Range(1, 5);
            while(lastLoop == nextloop)
            {
                nextloop = Random.Range(1, 5);
            }
            // this branch has concluded
            switch(lastLoop = Random.Range(1, 5))
            {
                case 1:
                    currentBranch = dialogLoop1;
                break;
                case 2:
                    currentBranch = dialogLoop2;
                break;
                case 3:
                    currentBranch = dialogLoop3;
                    break;
                case 4:
                    currentBranch = dialogLoop4;
                    break;
                case 5:
                    currentBranch = dialogLoop5;
                    break;
            }
        }
    }
        
    
    void Update()
    {
        Refresh();
    }
}
