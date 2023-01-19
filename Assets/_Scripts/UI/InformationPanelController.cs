using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InformationPanelController : MonoBehaviour
{

    [SerializeField] private Canvas canvas;
    [SerializeField] private Image bgImage;
    [SerializeField] private TextMeshProUGUI infoText;

    [TextArea(3, 10)]
    [SerializeField] private string descriptionText;

    private bool isPanelOpen = false;
    private bool isRunning = false;

    private void Start()
    {
        infoText.text = descriptionText;
    }

    public void ToggleInfoPanel()
    {
        if (!isPanelOpen && !isRunning)
        {
            StartCoroutine(ShowInfoPanel());
        }
        else if (isPanelOpen && !isRunning)
        {
            StartCoroutine(HideInfoPanel());
        }
    }

    public IEnumerator ShowInfoPanel()
    {
        isRunning = true;
        bgImage.fillAmount = 0;
        infoText.GetComponent<CanvasGroup>().alpha = 0;
        float newFillAmount = bgImage.fillAmount;
        float newAlphaAmount = infoText.GetComponent<CanvasGroup>().alpha;


        while (newFillAmount <= 1)
        {
            newFillAmount += 1 * Time.deltaTime;
            bgImage.fillAmount = newFillAmount;
            yield return new WaitForEndOfFrame();
        }

        while (newAlphaAmount <= 1)
        {
            newAlphaAmount += 1 * Time.deltaTime;
            infoText.GetComponent<CanvasGroup>().alpha = newAlphaAmount;
            yield return new WaitForEndOfFrame();
        }

        isPanelOpen = true;
        isRunning = false;

        yield return null;
    }

    public IEnumerator HideInfoPanel()
    {
        isRunning = true;
        bgImage.fillAmount = 1;
        infoText.GetComponent<CanvasGroup>().alpha = 1;
        float newFillAmount = bgImage.fillAmount;
        float newAlphaAmount = infoText.GetComponent<CanvasGroup>().alpha;

        while (newAlphaAmount >= 0)
        {
            newAlphaAmount -= 1 * Time.deltaTime;
            infoText.GetComponent<CanvasGroup>().alpha = newAlphaAmount;
            yield return new WaitForFixedUpdate();
        }

        while (newFillAmount >= 0)
        {
            newFillAmount -= 1 * Time.deltaTime;
            bgImage.fillAmount = newFillAmount;
            yield return new WaitForFixedUpdate();

        }

        isPanelOpen = false;
        isRunning = false;

        yield return null;
    }
}
