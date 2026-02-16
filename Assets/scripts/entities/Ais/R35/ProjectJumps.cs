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
        float maxtime = -(Offset.y - Vely) * 2 / g;
        if (maxtime * Mathf.Abs(Velx) > Mathf.Abs(Offset.x)) {

            float targetTime = Mathf.Abs(Offset.x) / Mathf.Abs(Velx);
            if(Mathf.Abs(Offset.y - (-0.5f * g * Mathf.Pow( targetTime , 2 ) + Vely * targetTime )) < MaxDist)
            {
                time = targetTime;
                return true;
            }
        }
            //error
        time = -1;
        return false;

    }
    // projectile trajectories
}
