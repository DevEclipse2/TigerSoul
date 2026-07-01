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

    public static List<Dictionary<string,string>> kv { get; private set; }
    


    static void addKV(byte section,string key, string value)
    {
        if (!kv[section].ContainsKey(key))
        {
            kv[section].Add(key, value);
        }
    }
   
}

