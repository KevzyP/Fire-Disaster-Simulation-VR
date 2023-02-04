using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SimulationConfirmationController : MonoBehaviour
{
    [SerializeField] private Image bgImage;
    [SerializeField] private TextMeshProUGUI simulationTitle;
    [SerializeField] private CanvasGroup contentsCanvasGroup;

    private GameManager gameManager;

    private bool isWindowOpen = false;
    private bool isRunning = false;

    public void Start()
    {
        gameManager = GameManager.Instance;
        ChangeSimulationTitle();
    }

    public void StartSimulation()
    {
        switch (gameManager.currentScenario)
        {
            case 1:
                gameManager.UpdateGameState(GameState.ExMode);
                gameManager.ChangeScene(gameManager.s_FireExScene);
                break;

            case 2:
                gameManager.UpdateGameState(GameState.EvacMode);
                gameManager.ChangeScene(gameManager.s_EvacScene);
                break;

            default:
                break;
        }
    }

    public void ExitRoom()
    {
        gameManager.currentScenario = 0;
        gameManager.UpdateGameState(GameState.FreeRoamMode);
        gameManager.ChangeScene(gameManager.s_FreeRoam);
    }

    private void ChangeSimulationTitle()
    {
        switch (gameManager.currentScenario)
        {
            case 1:
                simulationTitle.text = gameManager.extSimulationTitle;
                break;

            case 2:
                simulationTitle.text = gameManager.evacSimulationTitle;
                break;

            default:
                break;
        }
    }

    public void ToggleScenarioWindow()
    {
        if (!isWindowOpen && !isRunning)
        {
            StartCoroutine(ShowConfirmationWindow());
        }
        else if (isWindowOpen && !isRunning)
        {
            StartCoroutine(HideConfirmationWindow());
        }
    }

    public IEnumerator ShowConfirmationWindow()
    {
        isRunning = true;
        bgImage.fillAmount = 0;
        contentsCanvasGroup.alpha = 0;
        var currFillAmount = bgImage.fillAmount;
        var currAlphaAmount = contentsCanvasGroup.alpha;

        while (currFillAmount <= 1)
        {
            currFillAmount += 1 * Time.deltaTime;
            bgImage.fillAmount = currFillAmount;
            yield return new WaitForEndOfFrame();
        }


        while (currAlphaAmount <= 1)
        {
            currAlphaAmount += 1 * Time.deltaTime;
            contentsCanvasGroup.alpha = currAlphaAmount;
            yield return new WaitForEndOfFrame();
        }

        foreach (Image image in contentsCanvasGroup.GetComponentsInChildren<Image>())
        {
            image.raycastTarget = true;
        }

        contentsCanvasGroup.blocksRaycasts = true;

        isWindowOpen = true;
        isRunning = false;

        yield return null;
    }
    public IEnumerator HideConfirmationWindow()
    {
        isRunning = true;
        contentsCanvasGroup.blocksRaycasts = false;
        bgImage.fillAmount = 1;
        contentsCanvasGroup.alpha = 1;
        var currFillAmount = bgImage.fillAmount;
        var currAlphaAmount = contentsCanvasGroup.alpha;

        foreach (Image image in contentsCanvasGroup.GetComponentsInChildren<Image>())
        {
            image.raycastTarget = false;
        }

        while (currAlphaAmount >= 0)
        {
            currAlphaAmount -= 1 * Time.deltaTime;
            contentsCanvasGroup.alpha = currAlphaAmount;
            yield return new WaitForEndOfFrame();
        }

        while (currFillAmount >= 0)
        {
            currFillAmount -= 1 * Time.deltaTime;
            bgImage.fillAmount = currFillAmount;
            yield return new WaitForEndOfFrame();
        }

        isWindowOpen = false;
        isRunning = false;

        yield return null;
    }
}