using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public GameState State;
    public static event UnityAction<GameState> OnGameStateChanged;

    public int currentScenario;

    public bool tutFinishedFreeRoam = false;
    public bool tutFinishedEvac = false;
    public bool tutFinishedEx = false;

    public string s_FreeRoam = "FreeRoam";
    public string s_PrepRoom = "PrepRoom";
    public string s_EvacScene = "EvacuationRoom";
    public string s_FireExScene = "FireExSimulation";

    public string extSimulationTitle = "Extinguishing Simulation";
    public string evacSimulationTitle = "Evacuation Simulation";

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.MainMenu:

                break;
            case GameState.FreeRoamMode:
                break;
            case GameState.PrepRoomMode:
                break;
            case GameState.EvacMode:
                break;
            case GameState.ExMode:
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }

    public void SaveData()
    {
        SaveSystem.SaveData(this);
    }

    public void LoadData()
    {
        SavedData data = SaveSystem.LoadData();

        if (data != null)
        {
            tutFinishedFreeRoam = data.tutFinishedFreeRoam;
            tutFinishedEx = data.tutFinishedEx;
            tutFinishedFreeRoam = data.tutFinishedFreeRoam;
        }
        
    }
}

public enum GameState
{
    MainMenu,
    FreeRoamMode,
    PrepRoomMode,
    EvacMode,
    ExMode
}