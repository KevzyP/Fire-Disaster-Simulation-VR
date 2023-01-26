using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    static string filePath = Application.persistentDataPath;
    static string fileName = "savedConfigs";
    static string fileFormat = ".bin";

    public static void SaveData(GameManager manager)
    {
        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }

        BinaryFormatter formatter = new BinaryFormatter();

        Debug.Log("Saving to " + filePath + "/" + fileName + fileFormat);

        FileStream fileStream = File.Create(filePath + "/" + fileName + fileFormat);

        SavedData data = new SavedData(manager);

        formatter.Serialize(fileStream, data);

        fileStream.Close();

    }

    public static SavedData LoadData()
    {
        if (File.Exists(filePath + "/" + fileName + fileFormat))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = File.Open(filePath + "/" + fileName + fileFormat, FileMode.Open); //new FileStream(path, FileMode.Open);

            SavedData data = formatter.Deserialize(fileStream) as SavedData;
            fileStream.Close();

            Debug.Log("Loaded data: \n" + data.tutFinishedFreeRoam + data.tutFinishedEvac + data.tutFinishedEx);

            return data;
        }
        else
        {
            Debug.LogWarning("Savefile not found in path!");
            return null;
        }
    }


    //private static PlayerPrefsHandler instance = null;

    //public static PlayerPrefsHandler Instance
    //{
    //    get
    //    {
    //        if (instance == null)
    //        {
    //            instance = new PlayerPrefsHandler();
    //        }
    //        return instance;
    //    }
    //}

    //public static void SaveCompletedTutorials(string key, bool value)
    //{
    //    PlayerPrefs.SetInt(key, value ? 1 : 0);
    //    PlayerPrefs.Save();
    //}

    //public static bool LoadCompletedTutorials(string key)
    //{
    //    return PlayerPrefs.GetInt(key) != 0;
    //}

    //public static void LoadData(GameManager manager)
    //{
    //    Debug.Log("Loading data...");
    //    manager.tutFinishedFreeRoam = LoadCompletedTutorials("FreeRoamTut");
    //    manager.tutFinishedEx = LoadCompletedTutorials("FireExTut");
    //    manager.tutFinishedEvac = LoadCompletedTutorials("FireEvacTut");
    //}

    //public static void SaveData(GameManager manager)
    //{
    //    Debug.Log("Saving data...");
    //    SaveCompletedTutorials("FreeRoamTut", manager.tutFinishedFreeRoam);
    //    SaveCompletedTutorials("FireExTut", manager.tutFinishedEx);
    //    SaveCompletedTutorials("FireEvacTut", manager.tutFinishedEvac);
    //    PlayerPrefs.Save();
    //}

    //void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(this.gameObject);
    //    }
    //    else
    //    {
    //        Destroy(this.gameObject);
    //    }
    //}
}
