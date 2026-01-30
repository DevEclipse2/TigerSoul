using System.Collections.Generic;
using UnityEngine;

public class Parralax : MonoBehaviour
{
    public GameObject[] TargetLayers;
    Transform[] layers; // List of layers (GameObjects) to apply parallax effect
    public float[] intensities; // Corresponding intensities for each layer // higher - more
    public Transform cameraTransform; // Reference to the camera
    private Vector3 previousCameraPosition; // Store the previous camera position

    void Start()
    {
        layers = new Transform[TargetLayers.Length];
        for(int i = 0; i < TargetLayers.Length; i++)
        {
            layers[i] = TargetLayers[i].transform;
        }
        // Initialize the previous camera position
        previousCameraPosition = cameraTransform.position;
    }

    void Update()
    {
        // Calculate the camera's movement since the last frame
        Vector3 cameraMovement = cameraTransform.position - previousCameraPosition;

        // Apply parallax effect to each layer
        for (int i = 0; i < layers.Length; i++)
        {
            float intensity = intensities[i];
            layers[i].position += new Vector3(cameraMovement.x, cameraMovement.y, 0) * intensity;
        }

        // Update the previous camera position
        previousCameraPosition = cameraTransform.position;
    }
}
