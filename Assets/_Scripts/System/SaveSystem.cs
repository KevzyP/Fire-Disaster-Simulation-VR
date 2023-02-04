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
}
