using UnityEngine;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// Save/Load Manager with PlayerPrefs and JSON save data
/// </summary>
public class SaveLoadManager : MonoBehaviour
{
    [Header("Save Settings")]
    public string saveFileName = "savegame.json";
    public bool useEncryption = false;

    private static SaveLoadManager instance;
    private string savePath;

    public static SaveLoadManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SaveLoadManager>();
            }
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Set save path
        savePath = Path.Combine(Application.persistentDataPath, saveFileName);
    }

    // PlayerPrefs Methods
    public void SaveInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();
    }

    public int LoadInt(string key, int defaultValue = 0)
    {
        return PlayerPrefs.GetInt(key, defaultValue);
    }

    public void SaveFloat(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
        PlayerPrefs.Save();
    }

    public float LoadFloat(string key, float defaultValue = 0f)
    {
        return PlayerPrefs.GetFloat(key, defaultValue);
    }

    public void SaveString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
    }

    public string LoadString(string key, string defaultValue = "")
    {
        return PlayerPrefs.GetString(key, defaultValue);
    }

    public void SaveBool(string key, bool value)
    {
        PlayerPrefs.SetInt(key, value ? 1 : 0);
        PlayerPrefs.Save();
    }

    public bool LoadBool(string key, bool defaultValue = false)
    {
        return PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) == 1;
    }

    // JSON Save Data
    [System.Serializable]
    public class SaveData
    {
        public int level;
        public int score;
        public float health;
        public Vector3 playerPosition;
        public List<string> inventory;
        public Dictionary<string, object> customData;

        public SaveData()
        {
            level = 1;
            score = 0;
            health = 100f;
            playerPosition = Vector3.zero;
            inventory = new List<string>();
            customData = new Dictionary<string, object>();
        }
    }

    public void SaveGame(SaveData data)
    {
        try
        {
            string json = JsonUtility.ToJson(data, true);

            if (useEncryption)
            {
                json = EncryptString(json);
            }

            File.WriteAllText(savePath, json);
            Debug.Log("Game saved successfully!");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to save game: {e.Message}");
        }
    }

    public SaveData LoadGame()
    {
        try
        {
            if (!File.Exists(savePath))
            {
                Debug.Log("No save file found, creating new game.");
                return new SaveData();
            }

            string json = File.ReadAllText(savePath);

            if (useEncryption)
            {
                json = DecryptString(json);
            }

            SaveData data = JsonUtility.FromJson<SaveData>(json);
            Debug.Log("Game loaded successfully!");
            return data;
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to load game: {e.Message}");
            return new SaveData();
        }
    }

    public bool HasSaveFile()
    {
        return File.Exists(savePath);
    }

    public void DeleteSaveFile()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
            Debug.Log("Save file deleted.");
        }
    }

    // Simple encryption (for demonstration)
    private string EncryptString(string text)
    {
        string encrypted = "";
        foreach (char c in text)
        {
            encrypted += (char)(c + 1);
        }
        return encrypted;
    }

    private string DecryptString(string text)
    {
        string decrypted = "";
        foreach (char c in text)
        {
            decrypted += (char)(c - 1);
        }
        return decrypted;
    }
}
