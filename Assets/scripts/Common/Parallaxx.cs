using System.Collections.Generic;
using UnityEngine;

public class Parralax : MonoBehaviour
{
    public GameObject[] TargetLayers;
    Transform[] layers; // List of layers (GameObjects) to apply parallax effect
    public float[] intensities; // Corresponding intensities for each layer // higher - more
    public bool[] LockX;
    public bool[] LockY;
    public Transform cameraTransform; // Reference to the camera
    private Vector3 previousCameraPosition; // Store the previous camera position

    void Start()
    {
        if(LockX == null || LockY == null)
        {
            LockX = new bool[1];
            LockY = new bool[1];
        }
        layers = new Transform[TargetLayers.Length];
        for(int i = 0; i < TargetLayers.Length; i++)
        {
            layers[i] = TargetLayers[i].transform;
        }
        // Initialize the previous camera position
        previousCameraPosition = cameraTransform.position;
        bool[] added = new bool[TargetLayers.Length];
        bool[] addedY = new bool[TargetLayers.Length];
        for (int i = 0; i < LockX.Length; i++)
        {
            added[i] = LockX[i];
        }
        for (int i = 0; i < LockY.Length; i++)
        {
            addedY[i] = LockY[i];
        }
        LockX = added;
        LockY = addedY;
    }

    void Update()
    {
        // Calculate the camera's movement since the last frame
        Vector3 cameraMovement = cameraTransform.position - previousCameraPosition;

        // Apply parallax effect to each layer
        for (int i = 0; i < layers.Length; i++)
        {
            float intensity = intensities[i];
            Vector3 vec = cameraMovement;
            if (LockX[i])
            {
                vec = new Vector3(0, vec.y, 0);
            }
            if (LockY[i])
            {
                vec = new Vector3(vec.x,0, 0);
            }
            layers[i].position += new Vector3(vec.x, vec.y, 0) * intensity;
        }

        // Update the previous camera position
        previousCameraPosition = cameraTransform.position;
    }
}
