using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitSimulationController : MonoBehaviour
{
    [SerializeField] private Canvas canvasPanel;

    private GameManager gameManager;

    private bool isWindowOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    public void ExitToFreeRoam()
    {
        gameManager.State = GameState.FreeRoamMode;
        gameManager.ChangeScene(gameManager.s_FreeRoam);
    }

    public void ToggleExitPanel()
    {
        switch (isWindowOpen)
        {
            case true:
                StartCoroutine(HideExitPanel());
                break;
            case false:
                StartCoroutine(ShowExitPanel());
                break;
        }
    }

    public IEnumerator ShowExitPanel()
    {
        isWindowOpen = true;

        canvasPanel.GetComponent<CanvasGroup>().alpha = 1f;
        canvasPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
        
        yield return null;
    }

    public IEnumerator HideExitPanel()
    {
        canvasPanel.GetComponent<CanvasGroup>().alpha = 0f;
        canvasPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;

        isWindowOpen = false;

        yield return null;
    }
}