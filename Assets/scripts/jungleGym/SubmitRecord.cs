using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;
using TMPro; // Required for TextMeshPro UI elements
using UnityEngine.UI;

public class ScoreSubmitter : MonoBehaviour
{
    [Header("UI References")]
    public TMP_InputField nameInputField;
    public Button submitButton;
    public TextMeshProUGUI statusText;

    [Header("Database Settings")]
    public string databaseEndpoint = "https://your-database-api.com/submit-score";

    // Assume this is being tracked elsewhere in your game
    public float currentRecordTime = 42.5f;

    // A basic HashSet for exact word matching. 
    // In a real game, you would populate this from a JSON file or use an external API.

    // Struct to format our data into JSON
    [Serializable]
    private struct ScorePayload
    {
        public string playerName;
        public float time;
    }

    private void Start()
    {
        currentRecordTime = Data.time;
        // Hook up the button click event via code
        if (submitButton != null)
        {
            submitButton.onClick.AddListener(OnSubmitClicked);
        }

        if (statusText != null) statusText.text = "Waiting for name...";
    }

    private void OnSubmitClicked()
    {
        string rawName = nameInputField.text;

        // 1. Validate Input
        if (string.IsNullOrWhiteSpace(rawName))
        {
            statusText.text = "Please enter a valid name!";
            return;
        }

        // Prevent button spamming (using the logic we discussed earlier!)
        submitButton.interactable = false;
        statusText.text = "Processing...";

        // 2. Filter the name
        // 3. Send the web request
        StartCoroutine(SendScoreToDatabase(rawName, currentRecordTime));
    }


    private IEnumerator SendScoreToDatabase(string playerName, float time)
    {
        // Package the data into our struct and convert it to JSON
        ScorePayload payload = new ScorePayload { playerName = playerName, time = time };
        string jsonData = JsonUtility.ToJson(payload);

        // Create the web request. We use Put and then change it to POST to easily pass raw JSON data.
        using (UnityWebRequest request = UnityWebRequest.Put(databaseEndpoint, jsonData))
        {
            request.method = UnityWebRequest.kHttpVerbPOST;
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Accept", "application/json");

            // Optional: Add authorization headers if your database requires an API key
            // request.SetRequestHeader("Authorization", "Bearer YOUR_API_KEY");
            // Wait for the request to finish
            yield return request.SendWebRequest();

            // Handle the response
            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error sending score: {request.error}");
                statusText.text = "Failed to submit. Check connection.";

                // Re-enable the button so they can try again
                submitButton.interactable = true;
            }
            else
            {
                Debug.Log($"Successfully submitted! Server response: {request.downloadHandler.text}");
                statusText.text = $"Success! Name: {playerName} | Time: {time}s";

                // Leave the button disabled to prevent duplicate submissions
                nameInputField.interactable = false;
            }
        }
    }

    private void OnDestroy()
    {
        // Clean up listeners when the object is destroyed
        if (submitButton != null) submitButton.onClick.RemoveListener(OnSubmitClicked);
    }
}
