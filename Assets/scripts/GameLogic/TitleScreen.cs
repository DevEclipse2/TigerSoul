using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class TitleScreen : MonoBehaviour
{
    public Transform origin;
    Vector3 mousePosition;
    float mouseScreenSpaceX;
    float mouseScreenSpaceY;
    Vector3 mouseScreenPos;
    public Transform Button0;
    public Transform Button1;
    public GameObject StrongGlow;
    Color initialCol;
     float timer;
    public string SceneToSwap;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialCol =  StrongGlow.GetComponent<SpriteRenderer>().color;
    }
    public void OnMouseX(InputValue value)
    {
        mouseScreenSpaceX = value.Get<float>();
    }
    public void OnMouseY(InputValue value)
    {
        mouseScreenSpaceY = value.Get<float>();
    }
    public void OnAny()
    {
        SceneManager.LoadScene(SceneToSwap);
    }
    // Update is called once per frame
    void Update()
    {
        mouseScreenPos = new Vector3(mouseScreenSpaceX, mouseScreenSpaceY, 0);
        //Debug.Log(mouseScreenPos);
        mousePosition = Camera.main.ScreenToWorldPoint(mouseScreenPos);

        // Create a direction vector from the GameObject's position to the mouse position
        Vector3 direction = mousePosition - transform.position;

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Vector3 min = Vector3.Min(Button0.position, Button1.position); // Minimum corner
        Vector3 max = Vector3.Max(Button0.position, Button1.position); // Maximum corner

        // Check if the mouse is within the defined rectangle
        if (mousePosition.x >= min.x && mousePosition.x <= max.x &&
            mousePosition.y >= min.y && mousePosition.y <= max.y)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer-= Time.deltaTime;
        }
        timer = Mathf.Clamp01(timer);
        StrongGlow.GetComponent<SpriteRenderer>().color = new Color(initialCol.r , initialCol.g , initialCol.b , timer);
        // Set the rotation of the GameObject
        origin.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
