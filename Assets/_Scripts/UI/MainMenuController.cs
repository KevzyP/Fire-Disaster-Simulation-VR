using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    private GameManager gameManager;
    private PlayerPrefsHandler prefsHandler;

    public void StartGame()
    {
        PlayerPrefsHandler.LoadData(gameManager);
        gameManager.State = GameState.FreeRoamMode;
        gameManager.ChangeScene(gameManager.s_FreeRoam);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        prefsHandler = PlayerPrefsHandler.Instance;
    }
}
