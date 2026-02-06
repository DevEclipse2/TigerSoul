using JetBrains.Annotations;
using System;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePoint : MonoBehaviour
{
    public string levelId;
    public string thisId;
    public Vector2 location;
    public bool isDeath;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void RecoverSave()
    {
        isDeath = true;
        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to the sceneLoaded event
        SceneManager.LoadScene(Datapersistence.Reloadscene);

    }
    // Update is called once per frame
    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        GameObject Player = GameObject.Find("PlayerRoot"); // Change to your specific spawn point name
        if (Player != null)
        {
            if (isDeath)
            {
                Debug.Log("positon" + Datapersistence.ReloadPoint);
                Player.GetComponent<PlayerLoadPosition>().setPositionVector(Datapersistence.ReloadPoint);
                isDeath = false;
            }
        }
    }
}
