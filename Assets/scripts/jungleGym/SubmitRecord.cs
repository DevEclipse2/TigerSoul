using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;

public class ScoreSubmitter : MonoBehaviour
{
    [Header("UI References")]
    public TMP_InputField nameInputField;
    public Button submitButton;
    public TextMeshProUGUI statusText;
    public TextMeshProUGUI Time;

    [Header("Database Settings")]
    public string databaseEndpoint = "https://jgym-record-keeper.anticlankerhammer.org";

    // change to reflect data
    public float currentRecordTime = 42.5f;

    [Serializable]
    private struct ScorePayload
    {
        public string playerName;
        public float time;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        currentRecordTime = Data.time;
        Time.text = currentRecordTime.ToString();
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

        submitButton.interactable = false;
        statusText.text = "Processing...";

       
        StartCoroutine(SendScoreToDatabase(rawName, currentRecordTime));
    }


    private IEnumerator SendScoreToDatabase(string playerName, float time)
    {
        ScorePayload payload = new ScorePayload { playerName = playerName, time = time };
        string jsonData = JsonUtility.ToJson(payload);
        using (UnityWebRequest request = UnityWebRequest.Put(databaseEndpoint, jsonData))
        {
            request.method = UnityWebRequest.kHttpVerbPOST;
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Accept", "application/json");


            statusText.text = "Sending to server...";

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error sending score: {request.error}");
                statusText.text = "Failed to submit. Check connection.";

                submitButton.interactable = true;
            }
            else
            {
                Debug.Log($"Successfully submitted! Server response: {request.downloadHandler.text}");
                statusText.text = $"Success! Name: {playerName} | Time: {time}s";
                nameInputField.interactable = false;
            }
        }
    }

    private void OnDestroy()
    {
        if (submitButton != null) submitButton.onClick.RemoveListener(OnSubmitClicked);
    }
}
