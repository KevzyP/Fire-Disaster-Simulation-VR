using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class TutorialPanelController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private TextMeshProUGUI indexCountText;
    [TextArea (10, 20)]
    [SerializeField] private List<string> textList;

    private GameManager gameManager;

    private int textIndex = 0;


    private void OnEnable()
    {
        textIndex = 0;
        ShowText();
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void ShowText()
    {
        tutorialText.text = textList[textIndex];
        indexCountText.text = textIndex+1 + " / " + textList.Count;
    }

    public void PrevText()
    {
        if (textIndex-1 >= 0 )
        {
            textIndex--;
            ShowText();
        }
    }

    public void NextText()
    {
        if (textIndex + 1 < textList.Count)
        {
            textIndex++;
            ShowText();
        }
    }

    public void CloseTutorial()
    {
        switch (gameManager.State)
        {
            case GameState.FreeRoamMode:
                gameManager.tutFinishedFreeRoam = true;
                break;
            case GameState.ExMode:
                gameManager.tutFinishedEx = true;
                break;
            case GameState.EvacMode:
                gameManager.tutFinishedEvac = true;
                break;
        }

        gameManager.SaveData();
        DisableTutPanel();
    }

    private void DisableTutPanel()
    {
        gameObject.SetActive(false);
    }
}
