//using UnityEditor.SearchService;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public string[] sceneToLoad;
    public GameObject player;
    public int[] loadPositions;
    public GameObject fade;
    LevelFade levelfade;
    public int targetpos;
    bool exit;
    int loadindex;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levelfade = fade.GetComponent<LevelFade>();
    }
    void Update()
    {
        if (exit)
        {
            if(levelfade.completed == true)
            {
                SceneManager.LoadScene(sceneToLoad[loadindex]);
                targetpos = loadPositions[loadindex];
            }
        }
    }
    public void LoadScene(int index)
    {
        loadindex = index;
        exit = true;
        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to the sceneLoaded event
        levelfade.fadeOut = true;
        
    }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        GameObject Player = GameObject.Find("PlayerRoot"); // Change to your specific spawn point name
        if (Player != null)
        {
            Player.GetComponent<PlayerLoadPosition>().setPosition(targetpos);
        }
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe to prevent repeated calls
    }
    // Update is called once per frame
}
