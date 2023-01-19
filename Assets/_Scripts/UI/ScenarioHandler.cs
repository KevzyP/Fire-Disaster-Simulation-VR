using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioHandler : MonoBehaviour
{
    [SerializeField] private Image scenarioBG;
    [SerializeField] private CanvasGroup scenariosCanvasGroup;
                
    private GameManager gameManager;

    private bool isWindowOpen = false;
    private bool isRunning = false;

    public void Start()
    {
        gameManager = GameManager.Instance;
    }

    public void ChangeScenario(int scenarioNumber)
    {
        gameManager.currentScenario = scenarioNumber;
        gameManager.UpdateGameState(GameState.PrepRoomMode);
        gameManager.tutFinished = false;
        gameManager.ChangeScene(gameManager.s_PrepRoom);
    }

    public void ToggleScenarioWindow()
    {
        Debug.Log("Toggling scenario window");
        if (!isWindowOpen && !isRunning)
        {
            if(gameManager.tutFinished == true)
                StartCoroutine(ShowScenarioWindow());
        }
        else if (isWindowOpen && !isRunning)
        {
            StartCoroutine(HideScenarioWindow());
        }
    }
    public IEnumerator ShowScenarioWindow()
    {
        Debug.Log("Running show scenario window");
        isRunning = true;
        scenarioBG.fillAmount = 0;
        scenariosCanvasGroup.alpha = 0;
        var currFillAmount = scenarioBG.fillAmount;
        var currAlphaAmount = scenariosCanvasGroup.alpha;

        while (currFillAmount <= 1)
        {
            currFillAmount += 1 * Time.deltaTime;
            scenarioBG.fillAmount = currFillAmount;
            yield return new WaitForFixedUpdate();
        }

       
        while (currAlphaAmount <= 1)
        {
            currAlphaAmount += 1 * Time.deltaTime;
            scenariosCanvasGroup.alpha = currAlphaAmount;
            yield return new WaitForEndOfFrame();
        }

        foreach(Image image in scenariosCanvasGroup.GetComponentsInChildren<Image>())
        {
            image.raycastTarget = true;
        }

        isWindowOpen = true;
        isRunning = false;

        yield return null;
    }
    public IEnumerator HideScenarioWindow()
    {
        isRunning = true;
        scenarioBG.fillAmount = 1;
        scenariosCanvasGroup.alpha = 1;
        var currFillAmount = scenarioBG.fillAmount;
        var currAlphaAmount = scenariosCanvasGroup.alpha;

        foreach (Image image in scenariosCanvasGroup.GetComponentsInChildren<Image>())
        {
            image.raycastTarget = false;
        }

        while (currAlphaAmount >= 0)
        {
            currAlphaAmount -= 1 * Time.deltaTime;
            scenariosCanvasGroup.alpha = currAlphaAmount;
            yield return new WaitForEndOfFrame();
        }

        while (currFillAmount >= 0)
        {
            currFillAmount -= 1 * Time.deltaTime;
            scenarioBG.fillAmount = currFillAmount;
            yield return new WaitForEndOfFrame();
        }       

        isWindowOpen = false;
        isRunning = false;

        yield return null;
    }
}
