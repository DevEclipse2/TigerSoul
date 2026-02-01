//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public string[] sceneToLoad;
    public GameObject player;
    public int[] loadPositions;

    public int targetpos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }
    public void LoadScene(int index)
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to the sceneLoaded event
        SceneManager.LoadScene(sceneToLoad[index]);
        targetpos = loadPositions[index];
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
