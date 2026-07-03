using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
[Serializable]
public static class Data
{
    static byte saveslot;
    public enum sections 
    {
        maintenance,
        catacombs
    }

    public static List<Dictionary<string,string>> kv { get; private set; } = new List<Dictionary<string,string>>();
    


    public static bool addKV(byte section,string key, string value)
    {
        while(kv.Count - 1  < section)
        {
            kv.Add(new Dictionary<string, string>());
        }
        if (!kv[section].ContainsKey(key))
        {
            kv[section].Add(key, value);
            return true;
        }
        return false;
    }
    public static bool readKV(byte section,string key,out string value)
    {
        while (kv.Count - 1 < section)
        {
            kv.Add(new Dictionary<string, string>());
        }
        if (!kv[section].ContainsKey(key))
        {
            value = null;
            return false;
        }
        kv[section].TryGetValue(key, out value);
        return true;
    }
   
}

