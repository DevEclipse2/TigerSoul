using UnityEngine;

public static class Datapersistence
{
    public static int playerhealth { get; private set; }
    public static string Reloadscene { get; private set; }
    public static Vector2 ReloadPoint { get; private set; }
    public static void FerryHealth(int health)
    {
        playerhealth = health;
    }
    public static void SetSave(Vector2 point , string scene)
    {
        Reloadscene = scene;
        ReloadPoint = point;
    }
}
