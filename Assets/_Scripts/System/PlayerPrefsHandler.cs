using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsHandler : MonoBehaviour
{
    private static PlayerPrefsHandler instance = null;

    public static PlayerPrefsHandler Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlayerPrefsHandler();
            }
            return instance;
        }
    }

    public static void SaveCompletedTutorials(string key, bool value)
    {
        PlayerPrefs.SetInt(key, value ? 1 : 0);
        PlayerPrefs.Save();
    }

    public static bool LoadCompletedTutorials(string key)
    {
        return PlayerPrefs.GetInt(key) != 0;
    }

    public static void LoadData(GameManager manager)
    {
        Debug.Log("Loading data...");
        manager.tutFinishedFreeRoam = LoadCompletedTutorials("FreeRoamTut");
        manager.tutFinishedEx = LoadCompletedTutorials("FireExTut");
        manager.tutFinishedEvac = LoadCompletedTutorials("FireEvacTut");
    }

    public static void SaveData(GameManager manager)
    {
        Debug.Log("Saving data...");
        SaveCompletedTutorials("FreeRoamTut", manager.tutFinishedFreeRoam);
        SaveCompletedTutorials("FireExTut", manager.tutFinishedEx);
        SaveCompletedTutorials("FireEvacTut", manager.tutFinishedEvac);
        PlayerPrefs.Save();
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
