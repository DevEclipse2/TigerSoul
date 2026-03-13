using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Threading;
public static class Ballistics
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static bool CheckPosition(float Vely , float Velx, Vector2 Offset , float g , float MaxDist, out float time)
    {
        // solve for time t
        float targetTime = Mathf.Abs(Offset.x / Velx);
        //Debug.Log("can Arrive" + targetTime);

        // y offset  - 1/2at^2
        // s = vt - 1/2at^2
        float totalOffset = -0.5f * g * Mathf.Pow(targetTime, 2) + Vely * targetTime;
        //Debug.Log(totalOffset + " actual offset" + Offset);
        if (totalOffset < Offset.y + MaxDist || totalOffset > Offset.y - MaxDist)
        {
            time = targetTime;
            return true;
        }
        
        //error
        time = -1;
        return false; 

    }
    // projectile trajectories
}
