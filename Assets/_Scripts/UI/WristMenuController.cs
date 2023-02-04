using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using System;

public class WristMenuController : MonoBehaviour
{
    [SerializeField] private InputActionReference menuButton;
    [SerializeField] private XRRayInteractor leftRayController;
    [SerializeField] private GameObject wristMenuCanvas;
    [SerializeField] private GameObject objectivePanel;
    [SerializeField] private GameObject systemPanel;
    [SerializeField] private GameObject exitPromptPanel;
    
    private GameObject controllerModel;
    private MenuState currentMenuState;
    private bool isInExitPrompt = false;
    private bool isShown = false;

    private void ToggleWristMenu(InputAction.CallbackContext obj)
    {
        Debug.Log("ToggleWristMenu function started");
        if (!isShown)
        {
            ShowWristMenu();
        }
        else if (isShown)
        {
            HideWristMenu();
        }
    }

    void ShowWristMenuCanvas()
    {
        CanvasGroup canvasGroup = wristMenuCanvas.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
    
    void HideWristMenuCanvas()
    {
        CanvasGroup canvasGroup = wristMenuCanvas.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
    }

    private void ShowWristMenu()
    {
        ShowWristMenuCanvas();
        controllerModel.SetActive(false);
        leftRayController.enabled = false;
        isShown = true;
    }

    private void HideWristMenu()
    {
        HideWristMenuCanvas();
        controllerModel.SetActive(true);
        leftRayController.enabled = true;
        isShown = false;
    }

    public void ChangeToObjectiveTab()
    {
        if(currentMenuState != MenuState.Objectives)
        {
            if (!isInExitPrompt)
            {
                systemPanel.SetActive(false);
                objectivePanel.SetActive(true);
                currentMenuState = MenuState.Objectives;
            }            
        }
    }

    public void ChangeToSystemTab()
    {
        if(currentMenuState != MenuState.System)
        {
            if (!isInExitPrompt)
            {
                objectivePanel.SetActive(false);
                systemPanel.SetActive(true);
                currentMenuState = MenuState.System;
            }            
        }
    }

    public void ToggleExitPrompt()
    {
        switch (currentMenuState)
        {
            case MenuState.ExitPrompt:
                exitPromptPanel.SetActive(false);
                currentMenuState = MenuState.System;
                isInExitPrompt = false;
                break;
            case MenuState.System:
                exitPromptPanel.SetActive(true);
                currentMenuState = MenuState.ExitPrompt;
                isInExitPrompt = true;
                break;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        menuButton.action.started += ToggleWristMenu;
    }

    // Update is called once per frame
    void OnDisable()
    {
        menuButton.action.started -= ToggleWristMenu;
    }

    private void Start()
    {
        HideWristMenuCanvas();
        controllerModel = GameObject.Find("[LeftHand Direct Controller] Model Parent");
        currentMenuState = MenuState.Objectives;
    }

    public enum MenuState
    {
        Objectives,
        System,
        ExitPrompt
    }

}

