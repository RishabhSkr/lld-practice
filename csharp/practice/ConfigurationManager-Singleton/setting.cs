/*
Task 1: Configuration Manager (Singleton Pattern)
Concept: Har badi application mein kuch 'settings' ya 'configurations' hoti hain 
(jaise Database URL, App Theme, API Keys). Hum chahte hain ki yeh settings puri app mein sirf ek hi jagah memory mein load hon aur sab jagah wahi se read/write hon.

Aapko kya banana hai:

Ek ConfigurationManager class banaiye jo Singleton ho.
Is class ke andar ek Dictionary<string, string> honi chahiye jo settings store karegi.
Usme yeh methods hone chahiye:
GetInstance() -> Singleton object return kare.
SetSetting(string key, string value) -> Dictionary mein key-value add kare (e.g., SetSetting("Theme", "Dark")).
GetSetting(string key) -> Dictionary se value return kare. Agar key nahi hai toh "Not Found" return kare.
Test: Program.cs mein ConfigurationManager ka instance lijiye, 2-3 settings save kariye. 
Phir ek naya instance fetch (GetInstance) kijiye aur check kariye ki purani settings wahan milti hain ya nahi (milenchi chahiye kyunki instance ek hi hai!).
*/
using System;
using System.Collections.Generic;

namespace  LldPractice.CSharp.ConfigurationManagerSingleton;

public class ConfigurationManager
{
    private static ConfigurationManager instance;
    private Dictionary<string, string> settings;
    private static readonly object lockObj = new object();

    private ConfigurationManager()
    {
        // Yahan console pe print kar dena "Configuration Manager Initialized!"
        Console.WriteLine("Configuration Manager Initialized!");
        settings = new Dictionary<string, string>();
    }

    public static ConfigurationManager GetInstance()
    {
        if(instance == null)
        {
            lock (lockObj)
            {
                if(instance == null)
                {
                    instance = new ConfigurationManager();
                }
            }
        }
        return instance;
    }

    public void SetSetting(string key, string value)
    {
        settings[key] = value;
    }

    public string GetSetting(string key)
    {
        if (settings.ContainsKey(key))
        {
            return settings[key];
        }
        return "Not Found";
    }
}

