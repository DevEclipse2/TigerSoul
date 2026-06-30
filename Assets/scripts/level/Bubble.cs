using UnityEngine;

public class Bubble : MonoBehaviour
{
    public bool permanent;
    public void selfDestruct()
    {
        if (permanent) return;
        Destroy(this);
    }
}
