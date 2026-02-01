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
    SavePoint save;
    SavePoint load;
    bool init = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(load != null)
        {
            levelId = load.levelId;
            location = load.location;
            isDeath = load.isDeath;
        }
    }
    public void LoadData(SavePoint point)
    {
        load = point;
    }
    void Update()
    {
        if (!init)
        {
            SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to the sceneLoaded event
            save = this.gameObject.GetComponent<SavePoint>();
        }
        
        init = true;
    }
    public void RecoverSave()
    {
        isDeath = true;
        SceneManager.LoadScene(levelId);
    }
    // Update is called once per frame
    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        GameObject Player = GameObject.Find("PlayerRoot"); // Change to your specific spawn point name
        if (Player != null)
        {
            if (isDeath)
            {

                Player.GetComponent<PlayerLoadPosition>().setPositionVector(location);
                isDeath = false;
            }
            if(scene.name != thisId)
            {
                Debug.Log(thisId);
                Player.GetComponent<PlayerLoadPosition>().PassSaveData(save);
            }
        }
    }
}
