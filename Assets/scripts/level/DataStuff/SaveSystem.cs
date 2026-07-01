using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEditor.Overlays;
using UnityEngine;

public static class SaveSystem
{
    public struct Savedata
    {
        string CreationDate;
        string LastPlayedDate;
        float Playtime;
        List<Dictionary<string, string>> levelDat;
        Dictionary<string, bool> playerUpgrades;

    }
    private readonly static string encryptionKey = "4NjqEvg4sYBfE2bP";
    private readonly static string encryptionIV = "2pjAcj0UkGOHHut7";

    static byte saveslot;
    static Savedata savedata;
    static void loadGame(string filepath)
    {

    }

    public static void SaveGame()
    {
        string saveFilePath = Application.persistentDataPath + "/savegame" + ".slot" + saveslot.ToString();
        string json = JsonUtility.ToJson(savedata);
        string encryptedData = EncryptData(json);
        // 3. Write it to a file
        Debug.Log("Game saved securely at: " + saveFilePath);
    }

    public static Savedata LoadGame(byte slot)
    {
        string saveFilePath = Application.persistentDataPath + "/savegame" + ".slot" + saveslot.ToString();
        if (File.Exists(saveFilePath))
        {
            try
            {
                string encryptedData = File.ReadAllText(saveFilePath);
                string decryptedJson = DecryptData(encryptedData);
                Debug.Log("Game loaded successfully!");
                return JsonUtility.FromJson<Savedata>(decryptedJson);
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to load save file (it might be corrupted): " + e.Message);
            }
        }

        // If no save file exists, return a brand new PlayerData object
        Debug.Log("No save file found. Creating new data.");
        return new Savedata();
    }



    private static string EncryptData(string plainText)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(encryptionKey);
        byte[] ivBytes = Encoding.UTF8.GetBytes(encryptionIV);

        using (Aes aes = Aes.Create())
        {
            aes.Key = keyBytes;
            aes.IV = ivBytes;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                    {
                        streamWriter.Write(plainText);
                    }
                }
                // Convert to Base64 so it can be safely saved as text
                return Convert.ToBase64String(memoryStream.ToArray());
            }
        }
    }
    

    private static string DecryptData(string encryptedText)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(encryptionKey);
        byte[] ivBytes = Encoding.UTF8.GetBytes(encryptionIV);
        byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);

        using (Aes aes = Aes.Create())
        {
            aes.Key = keyBytes;
            aes.IV = ivBytes;

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new MemoryStream(cipherTextBytes))
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader streamReader = new StreamReader(cryptoStream))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }
    }
}
